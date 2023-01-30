using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : Item
{
    protected override void Effect()
    {
        if (GameManager.Instance.player == null) return;

        SoundInstance.Instance.GetOtherItemSFX();

        GameManager.Instance.player.LevelUp();
    }
}
