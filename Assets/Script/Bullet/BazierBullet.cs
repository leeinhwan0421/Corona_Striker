using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazierBullet : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;

    [Header("value")]
    [SerializeField] private float posA = 0.45f;
    [SerializeField] private float posB = 0.45f;

    private Vector2[] points = new Vector2[4];

    private float time = 0.0f;

    private void FixedUpdate()
    {
        if (time > 1.0f)
        {
            Effect();
            Destroy(gameObject);
            return;
        }

        time += Time.deltaTime * moveSpeed;

        Movement();
    }

    public void SetBullet(Transform target)
    {
        points[0] = transform.position;
        points[1] = SetPoint(transform.position);
        points[2] = SetPoint(target.position);
        points[3] = target.position;
    }

    private Vector2 SetPoint(Vector2 origin)
    {
        float x, y;

        x = posA * Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad) + origin.x;
        y = posB * Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad) + origin.y;
        return new Vector2(x, y);
    }

    private void Movement()
    {
        Vector2 previousPosition = transform.position;
        Vector2 newPosition = new Vector2(
            BazierMovement(points[0].x, points[1].x, points[2].x, points[3].x),
            BazierMovement(points[0].y, points[1].y, points[2].y, points[3].y)
        );

        transform.position = newPosition;

        Vector2 direction = newPosition - previousPosition;
        transform.up = direction;
    }

    private float BazierMovement(float a, float b, float c, float d)
    {
        float ab = Mathf.Lerp(a, b, time);
        float bc = Mathf.Lerp(b, c, time);
        float cd = Mathf.Lerp(c, d, time);

        float abbc = Mathf.Lerp(ab, bc, time);
        float bccd = Mathf.Lerp(bc, cd, time);

        return Mathf.Lerp(abbc, bccd, time);
    }

    private void Effect()
    {
        GameObject effect = Instantiate(this.effect, transform.position, Quaternion.identity);
        Destroy(effect, 0.25f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletBorder"))
        {
            Destroy(gameObject);
        }

        switch (collision.tag)
        {
            case "Cell":
                Effect();
                Destroy(gameObject);
                break;
            case "Enemy":
                collision.GetComponent<Enemy>().GetDamage(damage);
                Effect();
                Destroy(gameObject);
                break;
        }
    }
}
