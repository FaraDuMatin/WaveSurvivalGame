using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] Color color = Color.white;
    [SerializeField] float duration = 0.06f;

    SpriteRenderer sr; Color baseColor; float t;

    void Awake() => sr = GetComponent<SpriteRenderer>();

    public void Hit()
    {
        if (t <= 0f) baseColor = sr.color;
        sr.color = color; t = duration;
    }

    void Update()
    {
        if (t <= 0f) return;
        t -= Time.deltaTime;
        if (t <= 0f) sr.color = baseColor;
    }

    void OnDisable() { if (t > 0f) sr.color = baseColor; t = 0f; }
}
