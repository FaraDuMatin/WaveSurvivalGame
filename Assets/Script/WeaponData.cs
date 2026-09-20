using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon")]
public class WeaponData : ScriptableObject
{
    public string title;
    public GameObject weaponPrefab;
    public GameObject projectilePrefab;
    public float damage = 10f;
    public float cooldown = 1f;
    public float projectileSpeed = 10f;
    public float range = 8f;
    public AudioClip fireSfx;
    public int maxLevel = 5;
    public float damagePerLevel = 0.25f;
    public int[] extraProjectileAtLevels = { 2, 4 };
    public float spreadAngle = 15f;
    public float rangePerLevel;
}
