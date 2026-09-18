using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float interval = 1f;
    [SerializeField] float radius = 12f;

    Transform player;
    float timer;

    void Start() => player = GameObject.FindWithTag("Player").transform;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < interval) return;
        timer = 0f;
        Vector2 pos = (Vector2)player.position + Random.insideUnitCircle.normalized * radius;
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}
