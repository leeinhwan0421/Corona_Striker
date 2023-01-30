using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private GameObject chargeBullet;
    [SerializeField] private GameObject bomb;

    public const int maxChargeShotCount = 15;
    public int chargeShotCount { get; private set; } = 5;

    public const float maxBombCount = 1500.0f;
    public float bombCount { get; private set; } = 0.0f;

    public void GetChargeShot(int value)
    {
        chargeShotCount += value;

        if (chargeShotCount >= maxChargeShotCount)
        {
            chargeShotCount = maxChargeShotCount;
        }
    }

    public void GetBombGauge(float value)
    {
        bombCount += value;

        if (bombCount >= maxBombCount)
        {
            bombCount = maxBombCount;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) StartCoroutine(nameof(UseChargeShot));
        if (Input.GetKeyDown(KeyCode.C)) UseBomb();

        GameManager.Instance.uiManager.SetSkillInfoText(this);
    }

    private void UseBomb()
    {
        if (bombCount < 500.0f) return;

        int level = (int)bombCount / 500;

        GameObject bullet = Instantiate(bomb, transform.position, Quaternion.identity);
        bullet.GetComponent<Bomb>().SetBullet(level);

        bombCount -= level * 500.0f;
    }

    private IEnumerator UseChargeShot()
    {
        if (chargeShotCount <= 0) yield break;

        RaycastHit2D[] hit;
        bool isEnemy = true;

        hit = Physics2D.CircleCastAll(transform.position, 15.0f, Vector2.zero);

        while (isEnemy)
        {
            isEnemy = false;

            for (int i = 0; i < hit.Length; i++)
            {
                if (chargeShotCount <= 0) yield break;
                if (hit[i].collider == null) continue;
                if (hit[i].collider.CompareTag("Enemy"))
                {
                    GameObject bullet = Instantiate(chargeBullet, transform.position, Quaternion.identity);
                    bullet.GetComponent<BazierBullet>().SetBullet(hit[i].transform);
                    chargeShotCount--;
                    isEnemy = true;
                    SoundInstance.Instance.FireBulletSFX();
                    yield return new WaitForSeconds(0.08f);
                }
            }
        }
    }
}
