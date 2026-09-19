using UnityEngine;

public class PerfLog : MonoBehaviour
{
    float sum, worst; int frames;

    void Update()
    {
        float dt = Time.unscaledDeltaTime;
        sum += dt; frames++; worst = Mathf.Max(worst, dt);
        if (sum < 3f) return;
        Debug.Log($"[perf] avg {sum / frames * 1000f:F1}ms ({frames / sum:F0}fps) worst {worst * 1000f:F0}ms alive {EnemySpawner.Alive}");
        sum = worst = 0f; frames = 0;
    }
}
