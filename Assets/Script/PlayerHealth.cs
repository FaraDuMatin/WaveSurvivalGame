using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    float hp;

    void Awake() => hp = GetComponent<PlayerStats>().maxHp;

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Enemy e)) return;
        hp -= e.Data.damagePerSecond * Time.fixedDeltaTime;
        if (hp <= 0f) Die();
    }

    void Die()
    {
        Debug.Log("Player died");
        Time.timeScale = 0f;
    }
}
