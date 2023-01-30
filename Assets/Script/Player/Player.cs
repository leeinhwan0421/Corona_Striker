using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("health")]
    private const float maxhealthPoint = 100.0f;
    private float healthPoint = maxhealthPoint;

    [Header("Level")]
    private const int maxLevel = 5;
    private int level = 1;

    [Header("Attack")]
    [SerializeField] private List<GameObject> bullets = new List<GameObject>();
    [SerializeField] private List<float> bulletDamage = new List<float>();
    private const float attackCoolTime = 0.15f;
    private float attackCoolTimer = attackCoolTime;

    [Header("Movement")]
    private float moveSpeed = 5.5f;

    [Header("Invincible")]
    private float invincibleTimeSec = 0.0f;

    private void Update()
    {
        attackCoolTimer += Time.deltaTime;
        invincibleTimeSec -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Movement();
        Attack();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.Translate(moveSpeed * Time.deltaTime * new Vector3(x, y, 0.0f));
    }

    private void Attack()
    {
        if (attackCoolTimer < attackCoolTime) return;
        if (!Input.GetKey(KeyCode.Space)) return;

        // 구현할 방법은 많음.. class를 나누어서 별도로 처리하던지.....
        // 거대한 프로젝트가 아니기 때문에 대회장에서 했던 방식으로 처리....

        switch (level)
        {
            case 1:
                GameObject bullet = Instantiate(bullets[0], transform.position, Quaternion.identity);
                bullet.GetComponent<NormalBullet>().SetBullet(gameObject, Vector3.up, bulletDamage[0]);
                break;
            case 2:
                GameObject bullet2_left = Instantiate(bullets[0], transform.position  + new Vector3(-0.25f,0.0f,0.0f), Quaternion.identity);
                GameObject bullet2_right = Instantiate(bullets[0], transform.position + new Vector3(0.25f,0.0f,0.0f), Quaternion.identity);
                bullet2_left.GetComponent<NormalBullet>().SetBullet(gameObject, Vector3.up, bulletDamage[0]);
                bullet2_right.GetComponent<NormalBullet>().SetBullet(gameObject, Vector3.up, bulletDamage[0]);
                break;
            case 3:
                GameObject bullet3_left = Instantiate(bullets[1], transform.position + new Vector3(-0.25f, 0.0f, 0.0f), Quaternion.identity);
                GameObject bullet3_right = Instantiate(bullets[1], transform.position + new Vector3(0.25f, 0.0f, 0.0f), Quaternion.identity);
                bullet3_left.GetComponent<NormalBullet>().SetBullet(gameObject, Vector3.up, bulletDamage[1]);
                bullet3_right.GetComponent<NormalBullet>().SetBullet(gameObject, Vector3.up, bulletDamage[1]);
                break;
            case 4:
                GameObject bullet4_left = Instantiate(bullets[2], transform.position + new Vector3(-0.25f, 0.0f, 0.0f), Quaternion.identity);
                GameObject bullet4_right = Instantiate(bullets[2], transform.position + new Vector3(0.25f, 0.0f, 0.0f), Quaternion.identity);
                bullet4_left.GetComponent<NormalBullet>().SetBullet(gameObject, Vector3.up, bulletDamage[2]);
                bullet4_right.GetComponent<NormalBullet>().SetBullet(gameObject, Vector3.up, bulletDamage[2]);
                break;
            case 5:
                GameObject bullet5_left = Instantiate(bullets[1], transform.position + new Vector3(-0.25f, 0.0f, 0.0f), Quaternion.identity);
                GameObject bullet5_middle = Instantiate(bullets[3], transform.position + new Vector3(0.0f, 0.25f, 0.0f), Quaternion.identity);
                GameObject bullet5_right = Instantiate(bullets[1], transform.position + new Vector3(0.25f, 0.0f, 0.0f), Quaternion.identity);
                bullet5_left.GetComponent<NormalBullet>().SetBullet(gameObject, Vector3.up, bulletDamage[1]);
                bullet5_middle.GetComponent<NormalBullet>().SetBullet(gameObject, Vector3.up, bulletDamage[3]);
                bullet5_right.GetComponent<NormalBullet>().SetBullet(gameObject, Vector3.up, bulletDamage[1]);
                break;
        }

        SoundInstance.Instance.FireBulletSFX();

        attackCoolTimer = 0.0f;
    }

    public void SetInvincibleSecond(float seconds)
    {
        invincibleTimeSec = seconds;
    }

    public void LevelUp()
    {
        level++;

        if (level >= maxLevel)
        {
            level = maxLevel;
        }
    }

    public void SetHealth(float value)
    {
        if (healthPoint == 0) return;
        healthPoint = value;
        CheckHealthPoint();
    }

    public void GetHealth(float value)
    {
        if (healthPoint == 0) return;
        healthPoint += value;
        CheckHealthPoint();
    }

    public void GetDamage(float value)
    {
        if (healthPoint == 0) return;
        if (invincibleTimeSec > 0.0f) return;

        healthPoint -= value;

        SoundInstance.Instance.PlayerHitSFX();

        // 무적 시간
        SetInvincibleSecond(1.5f);
        CheckHealthPoint();
        StartCoroutine(nameof(HitEffect));
    }

    private void CheckHealthPoint()
    {
        // healthPoint가 올바른 상태인지 체크
        // 두번 죽는걸 방지하기 위해 health가 0이면 리턴해준다 // 호출 하는 함수에서. 여기서 하면 피가 0인 상태에서 체크하면 안죽잖아요.

        if (healthPoint <= 0.0f)
        {
            Dead();
        }
        else if (healthPoint >= maxhealthPoint)
        {
            healthPoint =  maxhealthPoint;
        }

        GameManager.Instance.uiManager.SetHealthBar(healthPoint, maxhealthPoint);
        GetComponent<Animator>().SetFloat("HealthPoint", healthPoint);
    }

    private void Dead()
    {
        healthPoint = 0.0f;
        moveSpeed = 0.0f;

        transform.position = Vector3.zero;
        transform.localScale = Vector3.one * 3.0f;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("IsDie");

        GameManager.Instance.StartCoroutine(GameManager.Instance.StageFail());

        // 삭제해주지 않아도 게임 오버 후 씬만 전환해 주면 된다!
    }

    private IEnumerator HitEffect()
    {
        // 피격시 깜빡이는 시각적 효과를 준다~~

        float colorValue = 0.5f;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        while(invincibleTimeSec > 0.0f)
        {
            spriteRenderer.color = Color.white * colorValue;

            colorValue = colorValue == 0.5f ? 0.8f : 0.5f;

            // 업데이트문에서 무적시간을 deltaTime만큼 빼주기 때문에 굳이 복잡한 식은 쓰지 않아도 됨
            yield return new WaitForSeconds(0.15f);
        }

        spriteRenderer.color = Color.white;
    }
}
