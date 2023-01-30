using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private float moveSpeed = 1.5f;

    private bool isHit = false;

    private float time = 0.0f;

    private void FixedUpdate()
    {
        time += Time.deltaTime;

        Movement();
    }

    private void Movement()
    {
        Vector3 dir = new Vector3(Mathf.Cos(time), -1.0f, 0.0f);

        transform.Translate(moveSpeed * Time.deltaTime * dir);
    }

    private void VisibleEffect()
    {
        isHit = true;
        GameObject effect = Instantiate(this.effect, transform.position, Quaternion.identity);
        Destroy(effect, effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        SoundInstance.Instance.EnemyHitSound();
    }

    protected abstract void Effect();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHit == true) return;

        if (collision.CompareTag("Player"))
        {
            Effect();
            VisibleEffect();
            Destroy(gameObject);
        }

        switch (this.tag)
        {
            case "WhiteBloodCell":
                if (collision.CompareTag("PlayerBullet"))
                {
                    Effect();
                    VisibleEffect();
                    Destroy(gameObject);
                }
                break;
            case "BloodCell":
                if (collision.CompareTag("EnemyBullet"))
                {
                    Effect();
                    VisibleEffect();
                    Destroy(gameObject);
                }
                break;
        }
    }
}
