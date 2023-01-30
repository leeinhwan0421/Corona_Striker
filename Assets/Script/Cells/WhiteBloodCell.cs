using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBloodCell : Cell
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();

    protected override void Effect()
    {
        int rand = Random.Range(0, 100);

        if (rand >= 80)
        {
            Instantiate(items[0], transform.position, Quaternion.identity);
        }
        else if (rand >= 60)
        {
            Instantiate(items[5], transform.position, Quaternion.identity);
        }
        else if (rand >= 40)
        {
            Instantiate(items[3], transform.position, Quaternion.identity);
        }
        else if (rand >= 20)
        {
            Instantiate(items[2], transform.position, Quaternion.identity);
        }
        else if (rand >= 10)
        {
            Instantiate(items[4], transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(items[1], transform.position, Quaternion.identity);
        }
    }
}
