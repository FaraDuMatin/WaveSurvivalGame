using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] Image hpFill, xpFill;
    [SerializeField] Text timer, kills, weapons;

    PlayerHealth health; PlayerStats stats; PlayerLevel level; WeaponHolder holder;
    readonly StringBuilder sb = new();

    void Start()
    {
        var p = GameObject.FindWithTag("Player");
        health = p.GetComponent<PlayerHealth>(); stats = p.GetComponent<PlayerStats>();
        level = p.GetComponent<PlayerLevel>(); holder = p.GetComponent<WeaponHolder>();
    }

    void Update()
    {
        hpFill.fillAmount = health.Hp / stats.maxHp;
        xpFill.fillAmount = level.Progress;
        timer.text = GameManager.Fmt(GameManager.I.Elapsed);
        kills.text = $"kills {GameManager.I.Kills}   lv {level.Level}";
        sb.Clear();
        foreach (var w in holder.Weapons) sb.Append(w.Data.name.Replace("Weapon_", "")).Append(' ').Append(w.Level).Append('\n');
        weapons.text = sb.ToString();
    }
}
