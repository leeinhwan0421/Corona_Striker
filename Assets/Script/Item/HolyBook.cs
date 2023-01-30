using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyBook : Item
{
    [SerializeField] private float damage = 100.0f;
    protected override void Effect()
    {
        SoundInstance.Instance.GetOtherItemSFX();
        // CircleCast2D로 적개체에게 데미지를 주는 아이템 옵션
        List<RaycastHit2D> hit = new List<RaycastHit2D>();

        hit.AddRange(Physics2D.CircleCastAll(Vector2.zero, 15.0f, Vector2.zero));

        foreach(RaycastHit2D item in hit)
        {
            if (item.collider == null) continue;
            if (item.collider.CompareTag("Enemy"))
            {
                item.collider.GetComponent<Enemy>().GetDamage(damage);
            }
        }
    }
}
