using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon")]
public class WeaponData : ScriptableObject
{
    public float damage = 10f;
    public float cooldown = 1f;
    public float projectileSpeed = 10f;
    public float range = 8f;
    public GameObject projectilePrefab;
}
