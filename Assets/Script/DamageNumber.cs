using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] float life = 0.5f, rise = 1.5f;
    TextMesh tm; float t;

    void Awake() => tm = GetComponent<TextMesh>();

    void OnEnable() { t = 0f; transform.position += (Vector3)(Random.insideUnitCircle * 0.3f); }

    void Update()
    {
        t += Time.deltaTime;
        transform.position += Vector3.up * (rise * Time.deltaTime);
        var c = tm.color; c.a = 1f - t / life; tm.color = c;
        if (t >= life) Pool.Release(gameObject);
    }
}
