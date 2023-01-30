using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float damage = 30.0f;

    private void FixedUpdate()
    {
        moveSpeed += Time.deltaTime;

        Movement();
    }

    private void Movement()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.up);
    }

    public void SetBullet(int level)
    {
        damage *= level;
        transform.localScale = Vector3.one * level;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "EnemyBullet":
                Destroy(collision.gameObject);
                break;
            case "Enemy":
                collision.GetComponent<Enemy>().GetDamage(damage);
                moveSpeed = 0.5f;
                break;
        }
    }
}
