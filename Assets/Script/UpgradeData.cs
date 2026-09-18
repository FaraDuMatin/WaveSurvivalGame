using UnityEngine;

public enum Stat { Damage, Cooldown, MoveSpeed, MaxHp, PickupRadius, Armor, Regen, XpGain, Projectiles, Area }

[CreateAssetMenu(menuName = "Data/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string title;
    public Stat stat;
    public float amount;
    public WeaponData weapon;

    public string Title(WeaponHolder h)
    {
        if (weapon == null) return title;
        var w = h.Get(weapon);
        return w == null ? "New: " + weapon.title : weapon.title + " Lv" + (w.Level + 1);
    }

    public bool Available(WeaponHolder h)
    {
        if (weapon == null) return true;
        var w = h.Get(weapon);
        return w == null || w.Level < weapon.maxLevel;
    }

    public void Apply(WeaponHolder h, PlayerStats s)
    {
        if (weapon != null) { h.Upgrade(weapon); return; }
        switch (stat)
        {
            case Stat.Damage: s.damageMult += amount; break;
            case Stat.Cooldown: s.cooldownMult -= amount; break;
            case Stat.MoveSpeed: s.moveSpeed += amount; break;
            case Stat.MaxHp: s.maxHp += amount; s.GetComponent<PlayerHealth>().Heal(amount); break;
            case Stat.PickupRadius: s.pickupRadius += amount; break;
            case Stat.Armor: s.armor += amount; break;
            case Stat.Regen: s.regen += amount; break;
            case Stat.XpGain: s.xpMult += amount; break;
            case Stat.Projectiles: s.extraProjectiles += (int)amount; break;
            case Stat.Area: s.areaMult += amount; break;
        }
    }
}
