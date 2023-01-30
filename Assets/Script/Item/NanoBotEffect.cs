using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoBotEffect : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float damage;

    private const float attackCoolTime = 0.5f;
    private float attackCoolTimer = attackCoolTime;

    private void FixedUpdate()
    {
        attackCoolTimer += Time.deltaTime;

        Attack();
    }

    private void Attack()
    {
        if (GameManager.Instance.player == null) return;
        if (attackCoolTimer < attackCoolTime) return;
        if (!Input.GetKey(KeyCode.Space)) return;

        GameObject bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
        bullet.GetComponent<NormalBullet>().SetBullet(GameManager.Instance.player.gameObject, Vector3.up, damage);

        attackCoolTimer = 0.0f;
    }
}
