using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hp = 30f;

    public void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0f) Destroy(gameObject);
    }
}
