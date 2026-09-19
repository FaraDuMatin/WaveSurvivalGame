using UnityEngine;

public class BoomerangWeapon : Weapon
{
    protected override bool Fire()
    {
        var target = Nearest();
        if (target == null) return false;
        Vector2 dir = (target.position - transform.position).normalized;
        int n = Count;
        for (int i = 0; i < n; i++)
        {
            float angle = (i - (n - 1) / 2f) * Data.spreadAngle;
            var b = Pool.Get(Data.projectilePrefab, transform.position, Quaternion.identity);
            b.GetComponent<Boomerang>().Init(Quaternion.Euler(0, 0, angle) * dir, Data.projectileSpeed, Damage, transform);
        }
        return true;
    }
}
