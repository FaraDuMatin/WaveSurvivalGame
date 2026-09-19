using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float damage;
    public float rehitDelay = 0.5f;

    Dictionary<Enemy, float> lastHit = new();

    void OnEnable() => lastHit.Clear();

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Enemy e)) return;
        if (lastHit.TryGetValue(e, out float t) && Time.time - t < rehitDelay) return;
        lastHit[e] = Time.time;
        e.TakeDamage(damage);
    }
}
