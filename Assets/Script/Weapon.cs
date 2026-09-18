using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData data;
    [SerializeField] LayerMask enemyMask;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < data.cooldown) return;
        var target = Nearest();
        if (target == null) return;
        timer = 0f;
        Vector2 dir = (target.position - transform.position).normalized;
        var p = Instantiate(data.projectilePrefab, transform.position, Quaternion.identity);
        p.GetComponent<Projectile>().Init(dir, data.projectileSpeed, data.damage);
    }

    Transform Nearest()
    {
        Transform best = null;
        float bestDist = float.MaxValue;
        foreach (var c in Physics2D.OverlapCircleAll(transform.position, data.range, enemyMask))
        {
            float d = (c.transform.position - transform.position).sqrMagnitude;
            if (d < bestDist) { bestDist = d; best = c.transform; }
        }
        return best;
    }
}
