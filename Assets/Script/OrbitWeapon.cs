using System.Collections.Generic;
using UnityEngine;

public class OrbitWeapon : Weapon
{
    [SerializeField] float rotateSpeed = 180f;

    List<Hitbox> blades = new();

    protected override bool Fire()
    {
        if (blades.Count != Count) Rebuild();
        foreach (var b in blades) b.damage = Damage;
        return true;
    }

    void Rebuild()
    {
        foreach (var b in blades) Destroy(b.gameObject);
        blades.Clear();
        for (int i = 0; i < Count; i++)
        {
            var b = Instantiate(Data.projectilePrefab, transform).GetComponent<Hitbox>();
            b.transform.localPosition = Quaternion.Euler(0, 0, 360f * i / Count) * Vector3.right * Range;
            blades.Add(b);
        }
    }

    protected override void Tick() => transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
}
