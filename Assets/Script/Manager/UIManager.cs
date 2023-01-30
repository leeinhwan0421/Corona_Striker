using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Image healthImage;
    [SerializeField] private Text healthText;

    [Header("Pain")]
    [SerializeField] private Image painImage;
    [SerializeField] private Text painText;

    [Header("SkillInformation")]
    [SerializeField] private Text chargeText;
    [SerializeField] private Text bombText;

    [Header("Boss")]
    [SerializeField] private GameObject bossInterfaceObject;
    [SerializeField] private Image bossImage;
    [SerializeField] private Text bossText;
    [SerializeField] private Text bossName;

    [Header("Text")]
    [SerializeField] private Text stage;
    [SerializeField] private Text timer;
    [SerializeField] private Text score;

    [Header("EscapeMenu")]
    [SerializeField] private GameObject escapeMenu;
    private bool isOpenEscapeMenu = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpenEscapeMenu = !isOpenEscapeMenu;
            escapeMenu.SetActive(isOpenEscapeMenu);
        }
    }

    public void SetHealthBar(float curHealth, float maxHealth)
    {
        healthImage.fillAmount = curHealth / maxHealth;
        healthText.text = "HP : " + curHealth.ToString() + " / " + maxHealth.ToString();
    }

    public void SetPainBar(float curPain, float maxPain)
    {
        painImage.fillAmount = curPain / maxPain;
        painText.text = "PAIN : " + curPain.ToString() + " / " + maxPain.ToString();
    }

    public void SetBossBar(Boss boss)
    {
        if (boss == null)
        {
            bossInterfaceObject.SetActive(false);
            return;
        }

        bossInterfaceObject.SetActive(true);

        bossImage.fillAmount = boss.currentHp / boss.maxHp;
        bossName.text = stage.text == "STAGE 1" ? "SARS-COV-2" : "SARS-COV-2 [DELTACRON]";
        bossText.text = boss.currentHp.ToString();
    }

    public void SetStageText(int stage)
    {
        this.stage.text = "STAGE " + stage.ToString();
    }

    public void SetTimerText(float timer)
    {
        if (timer <= 0)
        {
            this.timer.text = "BOSS";
            return;
        }
        else
        {
            int min = (int)timer / 60;
            int sec = (int)timer % 60;

            this.timer.text = min.ToString("00") + " : " + sec.ToString("00"); 
        }
    }

    public void SetSkillInfoText(PlayerSkill skill)
    {
        chargeText.text = "Charge : " + skill.chargeShotCount.ToString("0");
        bombText.text = "Bomb Level : " + ((int)(skill.bombCount / 500.0f)).ToString("0");
    }

    public void SetScoreText(float score)
    {
        this.score.text = "SCORE : " + score.ToString("000000");
    }
}
