using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private List<Vector3> shotDirection1 = new List<Vector3>();
    [SerializeField] private List<Vector3> shotDirection2 = new List<Vector3>();
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    private float timer = 0.0f;

    public float maxHp { get { return maxHealthPoint; } private set { } }
    public float currentHp { get { return healthPoint; } private set { } }

    private void Start()
    {
        GameManager.Instance.SetBoss(this);
    }

    private void OnDestroy()
    {
        GameManager.Instance.StartCoroutine(GameManager.Instance.StageClear());
    }

    protected override IEnumerator Attack()
    {
        int rand = Random.Range(0, 4);

        switch (rand)
        {
            case 0:
                Pattern1();
                yield return new WaitForSeconds(0.3f);
                Pattern2();
                yield return new WaitForSeconds(0.3f);
                Pattern1();
                yield return new WaitForSeconds(0.3f);
                Pattern2();
                yield return new WaitForSeconds(0.3f);
                Pattern1();
                yield return new WaitForSeconds(0.3f);
                Pattern2();
                yield return new WaitForSeconds(0.3f);
                Pattern1();
                break;
            case 1:
                Pattern1();
                yield return new WaitForSeconds(0.3f);
                Pattern3();
                yield return new WaitForSeconds(0.3f);
                Pattern1();
                yield return new WaitForSeconds(0.3f);
                Pattern3();
                yield return new WaitForSeconds(0.3f);
                Pattern1();
                yield return new WaitForSeconds(0.3f);
                Pattern3();
                yield return new WaitForSeconds(0.3f);
                Pattern1();
                break;
            case 2:
                Pattern4();
                Pattern4();
                Pattern4();
                break;
            case 3:
                for (int i = 0; i < 8; i++)
                {
                    Pattern3();
                    yield return new WaitForSeconds(0.2f);
                }
                break;
        }
        yield break;
    }

    private void Pattern1()
    {
        for (int i = 0; i < shotDirection1.Count; i++)
        {
            GameObject bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
            bullet.GetComponent<NormalBullet>().SetBullet(gameObject, shotDirection1[i], damage);
        }
    }

    private void Pattern2()
    {
        for (int i = 0; i < shotDirection2.Count; i++)
        {
            GameObject bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
            bullet.GetComponent<NormalBullet>().SetBullet(gameObject, shotDirection2[i], damage);
        }
    }
    private void Pattern3()
    {
        if (GameManager.Instance.player == null) return;

        GameObject bullet1 = Instantiate(bullet, transform.position + new Vector3(0.2f, 0.0f, 0.0f), Quaternion.identity);
        GameObject bullet2 = Instantiate(bullet, transform.position + new Vector3(-0.2f, 0.0f, 0.0f), Quaternion.identity);

        bullet1.GetComponent<NormalBullet>().SetBullet(gameObject, GameManager.Instance.player.transform.position - transform.position, damage);
        bullet2.GetComponent<NormalBullet>().SetBullet(gameObject, GameManager.Instance.player.transform.position - transform.position, damage);
    }

    private void Pattern4()
    {
        Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, Quaternion.identity);
    }

    protected override void Movement()
    {
        timer += Time.deltaTime;

        transform.Translate(moveSpeed * Time.deltaTime * new Vector3(Mathf.Cos(timer), 0.0f, 0.0f));

        if (transform.position.y < 3.0f) return;

        transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);
    }
}