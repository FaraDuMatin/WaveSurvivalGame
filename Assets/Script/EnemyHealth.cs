using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hp = 30f;
    [SerializeField] GameObject gemPrefab;

    public void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp > 0f) return;
        Instantiate(gemPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
