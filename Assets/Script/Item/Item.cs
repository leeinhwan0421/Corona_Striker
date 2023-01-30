using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private GameObject visibleEffectObject;
    [SerializeField] private float visibleEffectTime;
    [SerializeField] private float moveSpeed = 1.5f;

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);
    }

    // 보이지 않는 Effect
    protected abstract void Effect();

    // 보이는 Effect
    private void VisibleEffect()
    {
        if (GameManager.Instance.player == null) return;

        GameObject effect = Instantiate(this.visibleEffectObject, GameManager.Instance.player.transform.position, Quaternion.identity, GameManager.Instance.player.transform);
        Destroy(effect, visibleEffectTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                Effect();
                VisibleEffect();
                Destroy(gameObject);
                break;
        }
    }
}
