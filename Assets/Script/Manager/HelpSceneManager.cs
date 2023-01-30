using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject placeHolderPanel;

    [SerializeField] private GameObject virusHolderPanel;
    [SerializeField] private GameObject commandHolderPanel;
    [SerializeField] private GameObject itemHolderPanel;

    public void BoolPlaceHolderPanel(bool isTrue)
    {
        placeHolderPanel.SetActive(isTrue);
        SoundInstance.Instance.ClickSFX();
    }

    public void BoolVirusHolderPanel(bool isTrue)
    {
        virusHolderPanel.SetActive(isTrue);
        SoundInstance.Instance.ClickSFX();
    }

    public void BoolCommandHolderPanel(bool isTrue)
    {
        commandHolderPanel.SetActive(isTrue);
        SoundInstance.Instance.ClickSFX();
    }

    public void BoolItemHolderPanel(bool isTrue)
    {
        itemHolderPanel.SetActive(isTrue);
        SoundInstance.Instance.ClickSFX();
    }
}
