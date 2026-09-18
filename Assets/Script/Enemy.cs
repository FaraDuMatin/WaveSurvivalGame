using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    Rigidbody2D rb;
    Transform player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = (player.position - transform.position).normalized * speed;
    }
}
