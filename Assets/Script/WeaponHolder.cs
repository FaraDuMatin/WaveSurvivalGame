using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] WeaponData starting;

    public List<Weapon> Weapons = new();

    void Start() => Add(starting);

    public Weapon Get(WeaponData data) => Weapons.Find(w => w.Data == data);

    public void Add(WeaponData data)
    {
        var w = Instantiate(data.weaponPrefab, transform).GetComponent<Weapon>();
        w.Init(data, GetComponent<PlayerStats>());
        Weapons.Add(w);
    }

    public void Upgrade(WeaponData data)
    {
        var w = Get(data);
        if (w == null) Add(data); else w.Level++;
    }
}
