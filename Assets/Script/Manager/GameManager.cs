using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// ���� �������� �ִ� ���� Singleton pattern
/// ���� �����ϴ� Map, Player, EnemySpawner.... �� ĳĪ�ؼ� ����մϴ�.
/// GameObject.Find ���� �ʰ� GameManager�� �׻� ������ �ٳ� GameManager.Instance.Player.Func �̷������� �δ��� ���̵��� �մϴ�.
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

    // ������Ƽ�� ����Ͽ� �����Ͽ��� ������ 
    // Start()���� Find�� ���� ĳ�����ݴϴ�.
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
        // �̹� �ν��Ͻ��� �ִٸ� ������ �����ݴϴ�.....
        if (instance != null)
        {
            Destroy(instance);
            return;
        }

        instance = this;
    }

    public static GameObject GetTopObject(string name)
    {
        // GameObject.Find�� ���ɿ� ���ϸ� �� �� �ֱ⿡
        // ���Ӱ� Scene���� ��Ʈ ������Ʈ�� �˻��Ͽ� ���� ���ϸ� ���̴� ��� ä��
        // �̺��� �� ���� ����� ��ġ������ �ӽ������� ���

        // ���⼭ �������� ������Ʈ�� ��� ��Ʈ�� �־�� ��

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
            Debug.Log("���ӸŴ����� ĳ���� �ȵƴ�");
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
            // ���� ������ ���� �ö󰣴ٸ�... player�� ü���� 0���� ���� GameOver�� �ϴ� ����� ������ ��.
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
