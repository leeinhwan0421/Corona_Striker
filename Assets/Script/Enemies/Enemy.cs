using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 중복코드의 방지를 위해 가상클래스 지정
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected float maxHealthPoint = 100.0f;
    protected float healthPoint;

    [Header("Attack")]
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float damage = 0.0f;
    [SerializeField] protected float attackCoolTime = 100.0f;
    protected float attackCoolTimer;

    [Header("color")]
    private Color color;

    [Header("movement")]
    [SerializeField] protected float moveSpeed = 1.0f;

    [Header("Score")]
    [SerializeField] protected int score = 100;

    private void Awake()
    {
        color = GetComponent<SpriteRenderer>().color;
        healthPoint = maxHealthPoint;
    }

    private void FixedUpdate()
    {
        Movement();

        attackCoolTimer += Time.deltaTime;

        if (attackCoolTimer >= attackCoolTime)
        {
            StartCoroutine(Attack());
            attackCoolTimer = 0;
        }
    }

    protected abstract IEnumerator Attack();
    protected abstract void Movement();

    public void GetDamage(float value)
    {
        if (healthPoint == 0) return;

        healthPoint -= value;

        if (healthPoint <= 0)
        {
            Dead();
        }

        StartCoroutine(HitEffect());
    }

    private void Dead()
    {
        healthPoint = 0.0f;
        attackCoolTimer = -100.0f;

        GameManager.Instance.GetScore(score);
        GameManager.Instance.playerSkill.GetBombGauge(score / 20);
        GameManager.Instance.playerSkill.GetChargeShot(Random.Range(0, 2));

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("IsDie");

        SoundInstance.Instance.EnemyHitSound(); // 적이 맞을 때 마다 소리를 내는것은 느낌이 안삼.

        // HardCoding
        if (GetComponent<Boss>() != null)
        {
            Destroy(gameObject,1.0f);
        }
        else
        {
            Destroy(gameObject, 0.3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어에게 닿으면 0.5배 데미지가 플레이어에게 전달됨
        switch (collision.tag)
        {
            case "Player":
                collision.GetComponent<Player>().GetDamage(damage / 2);
                break;
            case "EnemyBorder":
                GameManager.Instance.GetPain(damage / 2);
                Destroy(gameObject);
                break;
        }
    }

    private IEnumerator HitEffect()
    {
        // 맞을 때 깜빡이게 하기 위한 Code,
        // 따로 color라는 변수를 선언한 이유는 같은 Sprite를 Color만 바꿔서 사용하기 때문이다.

        float colorValue = 0.5f;

        GetComponent<SpriteRenderer>().color = new Color(color.r * colorValue, color.g * colorValue, color.b * colorValue, color.a);

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = color;
    }
}
