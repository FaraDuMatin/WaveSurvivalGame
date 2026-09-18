using UnityEngine;

public enum Stat { Damage, Cooldown, MoveSpeed, MaxHp, PickupRadius }

[CreateAssetMenu(menuName = "Data/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string title;
    public Stat stat;
    public float amount;

    public void Apply(PlayerStats s)
    {
        switch (stat)
        {
            case Stat.Damage: s.damageMult += amount; break;
            case Stat.Cooldown: s.cooldownMult -= amount; break;
            case Stat.MoveSpeed: s.moveSpeed += amount; break;
            case Stat.MaxHp: s.maxHp += amount; break;
            case Stat.PickupRadius: s.pickupRadius += amount; break;
        }
    }
}
