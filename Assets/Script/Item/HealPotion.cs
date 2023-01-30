using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : Item
{
    [SerializeField] private float healValue = 20.0f;
    protected override void Effect()
    {
        if (GameManager.Instance.player == null) return;

        SoundInstance.Instance.HealItemSFX();
        GameManager.Instance.player.GetHealth(healValue);
    }
}
