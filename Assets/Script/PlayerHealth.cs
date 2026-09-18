using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float damagePerSecond = 20f;

    float hp;

    void Awake() => hp = GetComponent<PlayerStats>().maxHp;

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        hp -= damagePerSecond * Time.fixedDeltaTime;
        if (hp <= 0f) Die();
    }

    void Die()
    {
        Debug.Log("Player died");
        Time.timeScale = 0f;
    }
}
