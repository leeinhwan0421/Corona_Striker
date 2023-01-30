using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumorCell : Enemy
{
    [SerializeField] private List<Vector3> shotDirection = new List<Vector3>();

    protected override IEnumerator Attack()
    {
        Vector3 pos = GameManager.Instance.player.transform.position - transform.position;
        float distance = Vector2.Distance(pos, transform.position);

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < shotDirection.Count; j++)
            {
                GameObject bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
                bullet.GetComponent<NormalBullet>().SetBullet(gameObject, (pos * distance).normalized + shotDirection[j], damage);
            }

            yield return new WaitForSeconds(0.28f);
        }
    }

    protected override void Movement()
    {
        if (transform.position.y <= 3.5f) return;

        transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);
    }
}
