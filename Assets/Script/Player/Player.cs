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

        // ������ ����� ����.. class�� ����� ������ ó���ϴ���.....
        // �Ŵ��� ������Ʈ�� �ƴϱ� ������ ��ȸ�忡�� �ߴ� ������� ó��....

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

        // ���� �ð�
        SetInvincibleSecond(1.5f);
        CheckHealthPoint();
        StartCoroutine(nameof(HitEffect));
    }

    private void CheckHealthPoint()
    {
        // healthPoint�� �ùٸ� �������� üũ
        // �ι� �״°� �����ϱ� ���� health�� 0�̸� �������ش� // ȣ�� �ϴ� �Լ�����. ���⼭ �ϸ� �ǰ� 0�� ���¿��� üũ�ϸ� �����ݾƿ�.

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

        // ���������� �ʾƵ� ���� ���� �� ���� ��ȯ�� �ָ� �ȴ�!
    }

    private IEnumerator HitEffect()
    {
        // �ǰݽ� �����̴� �ð��� ȿ���� �ش�~~

        float colorValue = 0.5f;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        while(invincibleTimeSec > 0.0f)
        {
            spriteRenderer.color = Color.white * colorValue;

            colorValue = colorValue == 0.5f ? 0.8f : 0.5f;

            // ������Ʈ������ �����ð��� deltaTime��ŭ ���ֱ� ������ ���� ������ ���� ���� �ʾƵ� ��
            yield return new WaitForSeconds(0.15f);
        }

        spriteRenderer.color = Color.white;
    }
}
