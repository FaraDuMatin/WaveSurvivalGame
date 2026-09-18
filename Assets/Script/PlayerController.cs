using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] InputActionReference move;

    Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();
    void OnEnable() => move.action.Enable();
    void OnDisable() => move.action.Disable();

    void FixedUpdate()
    {
        rb.linearVelocity = move.action.ReadValue<Vector2>() * speed;
    }
}
