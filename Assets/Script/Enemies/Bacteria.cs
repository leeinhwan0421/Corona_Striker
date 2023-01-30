using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : Enemy
{
    private float radius = 2.0f;
    private float timer = 0.0f;
    private float traceTimer = 0.0f;

    Vector3 direction = Vector3.zero;

    private void Start()
    {
        // traceTime 지정....
        traceTimer = Random.Range(3.5f, 4.5f);
    }

    protected override IEnumerator Attack()
    {
        // 공격패턴이 없는 유일한 몬스터
        throw null;   
    }

    protected override void Movement()
    {
        traceTimer -= Time.deltaTime;

        if (traceTimer <= 0.0f)
        {
            // 일정 시간이 지나면 진행방향으로 쭈욱 간다
            transform.Translate(moveSpeed * Time.deltaTime * direction.normalized);
            return;
        }

        if (GameManager.Instance.player == null) return;

        timer += Time.deltaTime * radius;

        Vector3 playerPosition = GameManager.Instance.player.transform.position;

        direction = (playerPosition - transform.position) + (new Vector3(Mathf.Sin(timer), Mathf.Cos(timer), 0.0f) * radius);

        transform.Translate(moveSpeed * Time.deltaTime * direction.normalized);
    }
}
