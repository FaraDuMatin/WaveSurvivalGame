using UnityEngine;

public class AutoRelease : MonoBehaviour
{
    [SerializeField] float life = 3f;
    float t;

    void OnEnable() => t = 0f;

    void Update()
    {
        t += Time.deltaTime;
        if (t >= life) Pool.Release(gameObject);
    }
}
