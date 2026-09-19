using UnityEngine;

public class Projectile : MonoBehaviour
{
    float damage;

    public void Init(Vector2 dir, float speed, float dmg)
    {
        damage = dmg;
        GetComponent<Rigidbody2D>().linearVelocity = dir * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Enemy enemy)) return;
        enemy.TakeDamage(damage);
        Pool.Release(gameObject);
    }
}
