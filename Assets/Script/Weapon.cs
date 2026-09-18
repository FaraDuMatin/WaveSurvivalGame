using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponData Data { get; private set; }
    public int Level = 1;

    protected PlayerStats stats;
    protected static readonly int EnemyMask = 1 << 9;
    float timer;

    public void Init(WeaponData data, PlayerStats s) { Data = data; stats = s; }

    protected float Damage => Data.damage * stats.damageMult * (1f + Data.damagePerLevel * (Level - 1));
    protected float Cooldown => Data.cooldown * stats.cooldownMult;
    protected float Range => Data.range * (1f + Data.rangePerLevel * (Level - 1));
    protected int Count
    {
        get { int n = 1; foreach (var l in Data.extraProjectileAtLevels) if (Level >= l) n++; return n; }
    }

    void Update()
    {
        Tick();
        timer += Time.deltaTime;
        if (timer < Cooldown) return;
        if (Fire()) timer = 0f;
    }

    protected abstract bool Fire();
    protected virtual void Tick() { }

    protected Transform Nearest()
    {
        Transform best = null;
        float bestDist = float.MaxValue;
        foreach (var c in Physics2D.OverlapCircleAll(transform.position, Range, EnemyMask))
        {
            float d = (c.transform.position - transform.position).sqrMagnitude;
            if (d < bestDist) { bestDist = d; best = c.transform; }
        }
        return best;
    }
}
