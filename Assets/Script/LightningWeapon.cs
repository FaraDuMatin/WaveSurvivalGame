using System.Collections.Generic;
using UnityEngine;

public class LightningWeapon : Weapon
{
    protected override bool Fire()
    {
        var targets = new List<Collider2D>(Physics2D.OverlapCircleAll(transform.position, Range, EnemyMask));
        if (targets.Count == 0) return false;
        for (int i = 0; i < Count && targets.Count > 0; i++)
        {
            var c = targets[Random.Range(0, targets.Count)];
            targets.Remove(c);
            c.GetComponent<Enemy>().TakeDamage(Damage);
            Destroy(Instantiate(Data.projectilePrefab, c.transform.position, Quaternion.identity), 0.15f);
        }
        return true;
    }
}
