using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCell : Cell
{
    [SerializeField] private float getPainValue = 10.0f;
    protected override void Effect()
    {
        GameManager.Instance.GetPain(getPainValue);
    }
}
