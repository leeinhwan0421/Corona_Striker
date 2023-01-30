using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScoreInstance : MonoBehaviour
{
    private static ScoreInstance instance;

    public static ScoreInstance Instance
    {
        get
        {
            if (instance == null)
                return null;

            return instance;
        }
    }

    public static List<string> initialList = new List<string>();
    public static List<int> scoreList = new List<int>();
    public static int index = -1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        for (int i = 5; i > 0; i--)
        {
            initialList.Add("SAM");
            scoreList.Add(i * 1000);
        }
    }

    public void LoadNewScore(int score)
    {
        if (score < scoreList[4]) return;

        for (int i = 0; i < scoreList.Count; i++)
        {
            if (score > scoreList[i])
            {
                for (int j = scoreList.Count - 1; j > i; j--)
                {
                    scoreList[j] = scoreList[j - 1];
                    initialList[j] = initialList[j - 1];
                }

                scoreList[i] = score;
                initialList[i] = "YOU";

                index = i;

                StartCoroutine(OpenInitialPanel());

                break;
            }
        }
    }

    private IEnumerator OpenInitialPanel()
    {
        Scene scene;

        scene = SceneManager.GetActiveScene();

        while(scene.name == "GameScene")
        {
            scene = SceneManager.GetActiveScene();

            yield return new WaitForEndOfFrame();
        }

        // 성능을 저하시키는 구조...
        GameObject.Find("RankSceneManager").GetComponent<RankSceneManager>().InputInitialPanelOn();
    }
}
