using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RankSceneManager : MonoBehaviour
{
    [SerializeField] private List<Text> initialList = new List<Text>();
    [SerializeField] private List<Text> scoreList = new List<Text>();

    [SerializeField] private GameObject otherPanel;

    [SerializeField] private GameObject InitialPanel;

    [SerializeField] private Text scoreText;

    private void Start()
    {
        UpdateList();
    }

    public void InputInitialPanelOn()
    {
        otherPanel.SetActive(false);
        InitialPanel.SetActive(true);
        StartCoroutine(ScoreText());
        SoundInstance.Instance.InitialSFX();
    }

    public void InputInitialPanelOff(Text text)
    {
        if (text.text.Length == 3)
        {
            otherPanel.SetActive(true);
            InitialPanel.SetActive(false);
            SoundInstance.Instance.ClickSFX();
            ScoreInstance.initialList[ScoreInstance.index] = text.text;
            UpdateList();
        }
    }

    private void UpdateList()
    {
        for (int i = 0; i < scoreList.Count; i++)
        {
            scoreList[i].text = ScoreInstance.scoreList[i].ToString();
            initialList[i].text = ScoreInstance.initialList[i];
        }
    }

    private IEnumerator ScoreText()
    {
        float target = ScoreInstance.scoreList[ScoreInstance.index];
        float nowScore = 0;

        while(nowScore != target)
        {
            nowScore += target * Time.deltaTime;

            if (nowScore > target)
            {
                nowScore = target;
            }

            scoreText.text = "YOUR SCORE : " + ((int)nowScore).ToString("000000");

            yield return new WaitForEndOfFrame();
        }

    }
}
