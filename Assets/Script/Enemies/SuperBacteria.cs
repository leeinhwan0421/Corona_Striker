using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBacteria : Enemy
{
    [SerializeField] private List<Vector3> shotDirection = new List<Vector3>();
    protected override IEnumerator Attack()
    {
        GameObject bulletFirst = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletFirst.GetComponent<NormalBullet>().SetBullet(gameObject, Vector3.down, damage);

        yield return new WaitForSeconds(0.8f);

        for (int  i = 0; i < shotDirection.Count; i++)
        {
            GameObject bulletmiddle = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletmiddle.GetComponent<NormalBullet>().SetBullet(gameObject, Vector3.down + shotDirection[i], damage);
        }

        yield return new WaitForSeconds(0.8f);

        GameObject bulletLast = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletLast.GetComponent<NormalBullet>().SetBullet(gameObject, Vector3.down, damage);
    }

    protected override void Movement()
    {
        if (GameManager.Instance.player == null) return;

        Vector3 direction = GameManager.Instance.player.transform.position - transform.position + new Vector3(0.0f, 3.0f, 0.0f);

        transform.Translate(moveSpeed * Time.deltaTime * direction.normalized);
    }
}
