using UnityEngine;

public class AuraWeapon : Weapon
{
    Transform visual;

    protected override bool Fire()
    {
        if (visual == null) visual = Instantiate(Data.projectilePrefab, transform).transform;
        visual.localScale = Vector3.one * Range * 2f;
        foreach (var c in Physics2D.OverlapCircleAll(transform.position, Range, EnemyMask))
            if (c.TryGetComponent(out Enemy e)) e.TakeDamage(Damage);
        return true;
    }
}
