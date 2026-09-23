using UnityEngine;

// Cycles sprite frames (idle when still) and flips to face movement.
public class SpriteAnim : MonoBehaviour
{
    public Sprite[] frames, idle;
    public float fps = 10f;

    SpriteRenderer sr; Vector3 last; float phase;

    void Awake() { sr = GetComponent<SpriteRenderer>(); phase = Random.value * 10f; }

    void Update()
    {
        float dx = transform.position.x - last.x;
        bool moving = (transform.position - last).sqrMagnitude > 0.00001f;
        last = transform.position;
        if (Mathf.Abs(dx) > 0.001f) sr.flipX = dx < 0f;
        var set = !moving && idle != null && idle.Length > 0 ? idle : frames;
        if (set != null && set.Length > 0) sr.sprite = set[(int)((Time.time + phase) * fps) % set.Length];
    }
}
