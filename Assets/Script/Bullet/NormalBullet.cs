using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    [SerializeField] private float damage = 8.0f;
    [SerializeField] private float moveSpeed = 8.0f;
    [SerializeField] private float rotateSpeed = 0.0f;

    private string onwer;
    private Vector3 direction = Vector3.zero;

    [SerializeField] private GameObject effect;

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        transform.localPosition = transform.position + (moveSpeed * Time.deltaTime * direction.normalized);

        transform.Rotate(Time.deltaTime * rotateSpeed * Vector3.forward);
    }

    public void SetBullet(GameObject onwer, Vector3 direction, float damage)
    {
        this.onwer = onwer.tag;
        this.direction = direction;
        this.damage = damage;
    }

    private void Effect()
    {
        if (this.effect == null) return;

        GameObject effect = Instantiate(this.effect, transform.position, Quaternion.identity);
        Destroy(effect, effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletBorder"))
        {
            Destroy(gameObject);
        }

        // 코드의 중복을 막기위한 방안.... ex) playerBullet.cs, EnemyBullet.cs를 줄이고자 NormalBullet으로 통합...
        switch(onwer)
        {
            // 생성자가 적일때
            case "Enemy":
                switch (collision.tag)
                {
                    case "Player":
                        collision.GetComponent<Player>().GetDamage(damage);
                        Destroy(gameObject);
                        break;
                    case "BloodCell":
                        Destroy(gameObject);
                        break;
                }
                break;
            // 생성자가 플레이어일때
            case "Player":
                switch(collision.tag)
                {
                    case "Enemy":
                        collision.GetComponent<Enemy>().GetDamage(damage);
                        Effect();
                        Destroy(gameObject);
                        break;
                    case "WhiteBloodCell":
                        Destroy(gameObject);
                        break;
                }
                break;
        }
    }
}
