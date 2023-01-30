using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancerCell : Enemy
{
    [SerializeField] private List<Vector3> shotDirectionPattern1 = new List<Vector3>();
    [SerializeField] private List<Vector3> shotDirectionPattern2 = new List<Vector3>();

    protected override IEnumerator Attack()
    {
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < shotDirectionPattern1.Count; i++)
            {
                GameObject bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
                bullet.GetComponent<NormalBullet>().SetBullet(gameObject, shotDirectionPattern1[i], damage);
            }

            yield return new WaitForSeconds(0.75f);

            for (int i = 0; i < shotDirectionPattern2.Count; i++)
            {
                GameObject bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
                bullet.GetComponent<NormalBullet>().SetBullet(gameObject, shotDirectionPattern2[i], damage);
            }

            yield return new WaitForSeconds(0.75f);
        }
    }

    protected override void Movement()
    {
        if (transform.position.y <= 3.5f) return;

        transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);
    }
}
