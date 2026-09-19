using UnityEngine;
using UnityEngine.InputSystem;

public class DebugHud : MonoBehaviour
{
    [SerializeField] EnemySpawner spawner;
    float fps;

    void Update()
    {
        fps = Mathf.Lerp(fps, 1f / Time.unscaledDeltaTime, 0.05f);
        if (Keyboard.current.fKey.wasPressedThisFrame) spawner.SpawnMany(300);
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 20;
        GUI.Label(new Rect(Screen.width - 260, 10, 250, 90), $"alive {EnemySpawner.Alive}\ninstances {Pool.Total}\n{fps:F0} fps\n[F] spawn 300");
    }
}
