using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// 게임 씬에서만 있는 간이 Singleton pattern
/// 자주 접근하는 Map, Player, EnemySpawner.... 는 캐칭해서 사용합니다.
/// GameObject.Find 하지 않고 GameManager를 항상 가지고 다녀 GameManager.Instance.Player.Func 이런식으로 부담을 줄이도록 합니다.
/// </summary>
/// 
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                return null;

            return instance;
        }
    }

    [SerializeField] private List<GameObject> stageStart = new List<GameObject>();
    [SerializeField] private GameObject stageClear;
    [SerializeField] private GameObject stageFail;

    // 프로퍼티를 사용하여 선언하였기 때문에 
    // Start()에서 Find로 직접 캐싱해줍니다.
    public Map map { get; private set; }
    public Player player { get; private set; }
    public PlayerSkill playerSkill { get; private set; }
    public UIManager uiManager { get; private set; }
    public SpawnManager spawnManager { get; private set; }
    public Boss boss { get; private set; }

    private const float maxPain = 100;
    private float pain = 10;

    private int score = 0;
    private int stage = 1;

    private void Awake()
    {
        // 이미 인스턴스가 있다면 삭제를 시켜줍니다.....
        if (instance != null)
        {
            Destroy(instance);
            return;
        }

        instance = this;
    }

    public static GameObject GetTopObject(string name)
    {
        // GameObject.Find는 성능에 저하를 줄 수 있기에
        // 새롭게 Scene에서 루트 오브젝트만 검색하여 성능 저하를 줄이는 방법 채택
        // 이보다 더 좋은 방법은 넘치겠지만 임시적으로 사용

        // 여기서 가져오는 오브젝트는 모두 루트에 있어야 함

        Scene scene = SceneManager.GetActiveScene();
        GameObject[] rootObj = scene.GetRootGameObjects();
        foreach (GameObject obj in rootObj)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }

        return null;
    }

    private void Start()
    {
        map = GetTopObject("Map").GetComponent<Map>();
        player = GetTopObject("Player").GetComponent<Player>();
        playerSkill = GetTopObject("Player").GetComponent<PlayerSkill>();
        uiManager = GetTopObject("UIManager").GetComponent<UIManager>();
        spawnManager = GetTopObject("Spawner").GetComponent<SpawnManager>();

        if (map == null || player == null || uiManager == null || spawnManager == null || playerSkill == null)
        {
            Debug.Log("게임매니저에 캐싱이 안됐다");
            return;
        }

        uiManager.gameObject.SetActive(false);
        StartCoroutine(StageStart(1));
    }

    private void FixedUpdate()
    {
        uiManager.SetBossBar(boss);
    }

    public void SetBoss(Boss boss)
    {
        this.boss = boss;
        SoundInstance.Instance.BossBGM();
    }

    public void GetScore(int value)
    {
        score += value;
        uiManager.SetScoreText(score);
    }

    public void GetPain(float value)
    {
        pain += value;
        SoundInstance.Instance.GetPainSFX();
        CheckPain();
    }

    public void SetPain(float value)
    {
        pain = value;
        CheckPain();
    }

    public void LostPain(float value)
    {
        pain -= value;
        CheckPain();
    }

    private void CheckPain()
    {
        if (pain <= 0)
        {
            pain = 0;
        }

        else if (pain >= maxPain)
        {
            // 고통 지수가 많이 올라간다면... player의 체력을 0으로 만들어서 GameOver를 하는 방식을 취해줄 것.
            player.SetHealth(0);
            pain = maxPain;
        }

        uiManager.SetPainBar(pain, maxPain);
    }

    public IEnumerator StageStart(int stage)
    {
        this.stage = stage;
        uiManager.SetStageText(stage);

        GameObject stageStart = Instantiate(this.stageStart[this.stage - 1], Vector3.zero, Quaternion.identity);
        Destroy(stageStart, stageStart.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        map.StartCoroutine(map.ChangeMapToInfection());

        SoundInstance.Instance.GameBGM();

        yield return new WaitForSeconds(stageStart.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        uiManager.gameObject.SetActive(true);

        spawnManager.InitSpawner(stage);
        player.SetHealth(100.0f);

        if (stage == 1)
        {
            SetPain(10);
        }
        else
        {
            SetPain(30);
        }
    }

    public IEnumerator StageClear()
    {
        uiManager.gameObject.SetActive(false);
        map.StartCoroutine(map.ChangeMapToCure(stage));
        GameObject stageClear = Instantiate(this.stageClear, Vector3.zero, Quaternion.identity);
        Destroy(stageClear, stageClear.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        SoundInstance.Instance.GameClearSFX();

        yield return new WaitForSeconds(stageClear.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        player.SetInvincibleSecond(3.0f);
        player.SetHealth(100.0f);

        if (stage == 1)
        {
            StartCoroutine(StageStart(2));
        }
        else
        {
            ScoreInstance.Instance.LoadNewScore(score);
            SoundInstance.Instance.RankBGM();
            SceneManager.LoadSceneAsync(1);
        }

    }

    public IEnumerator StageFail()
    {
        uiManager.gameObject.SetActive(false);
        GameObject stageFail = Instantiate(this.stageFail, Vector3.zero, Quaternion.identity);
        Destroy(stageFail, stageFail.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        SoundInstance.Instance.GameOverSFX();

        yield return new WaitForSeconds(stageFail.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        ScoreInstance.Instance.LoadNewScore(score);
        SoundInstance.Instance.RankBGM();
        SceneManager.LoadSceneAsync(1);
    }
}
