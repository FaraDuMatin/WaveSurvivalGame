using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerStats stats;
    float hp;

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        hp = stats.maxHp;
    }

    void Update() => hp = Mathf.Min(hp + stats.regen * Time.deltaTime, stats.maxHp);

    public void Heal(float amount) => hp = Mathf.Min(hp + amount, stats.maxHp);

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Enemy e)) return;
        hp -= Mathf.Max(0f, e.Data.damagePerSecond - stats.armor) * Time.fixedDeltaTime;
        if (hp <= 0f) Die();
    }

    void Die()
    {
        Debug.Log("Player died");
        Time.timeScale = 0f;
    }
}
