using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainKiller : Item
{
    [SerializeField] private float lostPainValue = 20.0f;
    protected override void Effect()
    {
        SoundInstance.Instance.HealItemSFX();
        GameManager.Instance.LostPain(lostPainValue);
    }
}
