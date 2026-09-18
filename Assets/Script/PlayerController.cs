using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionReference move;

    Rigidbody2D rb;
    PlayerStats stats;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
    }
    void OnEnable() => move.action.Enable();
    void OnDisable() => move.action.Disable();

    void FixedUpdate()
    {
        rb.linearVelocity = move.action.ReadValue<Vector2>() * stats.moveSpeed;
    }
}
