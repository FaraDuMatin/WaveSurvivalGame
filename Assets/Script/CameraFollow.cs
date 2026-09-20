using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smooth = 0.08f, shakeDecay = 1.5f;

    static float shake;
    Vector3 basePos, vel;

    public static void Shake(float amount) => shake = Mathf.Max(shake, amount);

    void Start() => basePos = transform.position;

    void LateUpdate()
    {
        Vector3 goal = new(target.position.x, target.position.y, basePos.z);
        basePos = Vector3.SmoothDamp(basePos, goal, ref vel, smooth);
        shake = Mathf.Max(0f, shake - shakeDecay * Time.deltaTime);
        transform.position = basePos + (Vector3)(Random.insideUnitCircle * shake);
    }
}
