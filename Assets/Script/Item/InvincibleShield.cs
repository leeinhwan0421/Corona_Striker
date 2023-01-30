using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleShield : Item
{
    [SerializeField] private float invincibleSeconds = 3.0f;
    protected override void Effect()
    {
        if (GameManager.Instance.player == null) return;

        SoundInstance.Instance.GetOtherItemSFX();
        GameManager.Instance.player.SetInvincibleSecond(invincibleSeconds);
    }
}
