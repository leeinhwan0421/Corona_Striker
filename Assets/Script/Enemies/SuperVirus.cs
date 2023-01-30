using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperVirus : Enemy
{
    protected override IEnumerator Attack()
    {
        if (GameManager.Instance.player == null) yield break;

        for (int i = 0; i < 3; i++)
        {
            GameObject bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
            bullet.GetComponent<NormalBullet>().SetBullet(gameObject, GameManager.Instance.player.transform.position - transform.position, damage);
            yield return new WaitForSeconds(0.3f);
        }
    }

    protected override void Movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
    }
}
