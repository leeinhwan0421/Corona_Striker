using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInstance : MonoBehaviour
{
    private static SoundInstance instance;

    public static SoundInstance Instance
    {
        get
        {
            if (instance == null)
                return null;

            return instance;
        }
    }

    [SerializeField] private List<AudioClip> bgmList = new List<AudioClip>();
    [SerializeField] private List<AudioClip> sfxList = new List<AudioClip>();

    private AudioSource audioSource;

    public float bgmVolume { get; private set; } = 1.0f;
    public float sfxVolume { get; private set; } = 1.0f;

    private void Awake()
    {
        if( instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void SetBGMVolume(float value)
    {
        GetComponent<AudioSource>().volume = value / 2.0f;

        bgmVolume = value;
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
    }

    private void InstantiateSoundObject(AudioClip clip, float volume)
    {
        GameObject soundObject = new GameObject("SoundObject");
        soundObject.AddComponent<AudioSource>();

        soundObject.GetComponent<AudioSource>().volume = volume;
        soundObject.GetComponent<AudioSource>().clip = clip;
        soundObject.GetComponent<AudioSource>().loop = false;
        soundObject.GetComponent<AudioSource>().Play();

        // Scene이 바뀌더라도 효과음은 유지되어야만 한다
        DontDestroyOnLoad(soundObject);

        // SoundObject가 할 일을 다하면 Destroy 해야만 한다
        Destroy(soundObject, soundObject.GetComponent<AudioSource>().clip.length);
    }

    public void BossBGM()
    {
        if (audioSource.clip == bgmList[0]) return;

        audioSource.clip = bgmList[0];
        audioSource.Play();
    }

    public void GameBGM()
    {
        if (audioSource.clip == bgmList[1]) return;

        audioSource.clip = bgmList[1];
        audioSource.Play();
    }

    public void RankBGM()
    {
        if (audioSource.clip == bgmList[2]) return;

        audioSource.clip = bgmList[2];
        audioSource.Play();
    }

    public void TitleBGM()
    {
        if (audioSource.clip == bgmList[3]) return;

        audioSource.clip = bgmList[3];
        audioSource.Play();
    }

    public void GameClearSFX()
    {
        InstantiateSoundObject(sfxList[0], sfxVolume);
    }

    public void GameOverSFX()
    {
        InstantiateSoundObject(sfxList[1], sfxVolume);
    }

    public void GetOtherItemSFX()
    {
        InstantiateSoundObject(sfxList[2], sfxVolume);
    }

    public void HealItemSFX()
    {
        InstantiateSoundObject(sfxList[3], sfxVolume);
    }

    public void InitialSFX()
    {
        InstantiateSoundObject(sfxList[4], sfxVolume);
    }

    public void ClickSFX()
    {
        InstantiateSoundObject(sfxList[5], sfxVolume);
    }

    public void GetPainSFX()
    {
        InstantiateSoundObject(sfxList[6], sfxVolume);
    }

    public void PlayerHitSFX()
    {
        InstantiateSoundObject(sfxList[7], sfxVolume);
    }

    public void EnemyHitSound()
    {
        int rand = Random.Range(8, 11);
        InstantiateSoundObject(sfxList[rand], sfxVolume);
    }

    public void FireBulletSFX()
    {
        InstantiateSoundObject(sfxList[11], sfxVolume);
    }
}
