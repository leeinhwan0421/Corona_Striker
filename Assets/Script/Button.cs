using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject intro;

    public void LoadGameSceneWithIntro()
    {
        SoundInstance.Instance.ClickSFX();
        canvas.SetActive(false);

        // Instantiate Intro Object
        GameObject intro = Instantiate(this.intro, Vector3.zero , Quaternion.identity);

        // Wait Animator Change To Intro Object
        Invoke(nameof(LoadGameScene), intro.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length / 0.75f);
    }

    // LoadScene Func
    public void LoadTitleScene()
    {
        SoundInstance.Instance.ClickSFX();
        SoundInstance.Instance.TitleBGM();
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadRankScene()
    {
        SoundInstance.Instance.ClickSFX();
        SoundInstance.Instance.RankBGM();
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadHelpScene()
    {
        SoundInstance.Instance.ClickSFX();
        SoundInstance.Instance.TitleBGM();
        SceneManager.LoadSceneAsync(2);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void ExitGame()
    {
        SoundInstance.Instance.ClickSFX();
        Application.Quit();
    }
}
