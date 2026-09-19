using UnityEngine;

public class LightningWeapon : Weapon
{
    protected override bool Fire()
    {
        var targets = InRange();
        if (targets.Count == 0) return false;
        for (int i = 0; i < Count && targets.Count > 0; i++)
        {
            int k = Random.Range(0, targets.Count);
            var c = targets[k];
            targets.RemoveAt(k);
            c.GetComponent<Enemy>().TakeDamage(Damage);
            Pool.Get(Data.projectilePrefab, c.transform.position, Quaternion.identity);
        }
        return true;
    }
}
