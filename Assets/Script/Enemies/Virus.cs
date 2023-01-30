using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Enemy
{
    protected override IEnumerator Attack()
    {
        if (GameManager.Instance.player == null) yield break;

        GameObject bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
        bullet.GetComponent<NormalBullet>().SetBullet(gameObject, GameManager.Instance.player.transform.position - transform.position, damage);
    }

    protected override void Movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
    }
}
