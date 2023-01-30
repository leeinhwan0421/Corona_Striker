using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoBot : Item
{
    protected override void Effect()
    {
        SoundInstance.Instance.GetOtherItemSFX();
        return;
    }
}
