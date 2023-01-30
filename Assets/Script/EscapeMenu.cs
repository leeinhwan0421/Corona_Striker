using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    [Header("BGM")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Text bgmValueText;
    [Header("SFX")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Text sfxValueText;


    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        SoundInstance.Instance.ClickSFX();
        SoundValueUpdate();
    }

    public void SetBGMValue(Slider slider)
    {
        SoundInstance.Instance.SetBGMVolume(slider.value);
        SoundValueUpdate();
    }

    public void SetSFXValue(Slider slider)
    {
        SoundInstance.Instance.SetSFXVolume(slider.value);
        SoundValueUpdate();
    }

    private void SoundValueUpdate()
    {
        float bgm = SoundInstance.Instance.bgmVolume;
        float sfx = SoundInstance.Instance.sfxVolume;

        bgmSlider.value = bgm;
        sfxSlider.value = sfx;

        bgmValueText.text = ((int)(bgm * 100.0f)).ToString();
        sfxValueText.text = ((int)(sfx * 100.0f)).ToString();
    }

    private void OnDisable()
    {
        SoundInstance.Instance.ClickSFX();
        Time.timeScale = 1.0f;
    }
}
