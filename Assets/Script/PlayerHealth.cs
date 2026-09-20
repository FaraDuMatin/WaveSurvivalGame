using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerStats stats;
    Flash flash;
    float hp;
    public float Hp => hp;

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        flash = GetComponent<Flash>();
        hp = stats.maxHp;
    }

    void Update() => hp = Mathf.Min(hp + stats.regen * Time.deltaTime, stats.maxHp);

    public void Heal(float amount) => hp = Mathf.Min(hp + amount, stats.maxHp);

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Enemy e)) return;
        hp -= Mathf.Max(0f, e.Data.damagePerSecond - stats.armor) * Time.fixedDeltaTime;
        flash.Hit(); CameraFollow.Shake(0.08f); Sfx.Play(Sfx.I.hurt, 1f, 0.3f);
        if (hp <= 0f) Die();
    }

    void Die() => GameManager.I.GameOver();
}
