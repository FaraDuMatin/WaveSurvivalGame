using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] EnemyData[] types;
    [SerializeField] float startInterval = 1f;
    [SerializeField] float minInterval = 0.2f;
    [SerializeField] float rampSeconds = 300f;
    [SerializeField] int maxAlive = 300;
    [SerializeField] float radius = 12f;

    Transform player;
    float timer;
    public static int Alive;

    void Start() => player = GameObject.FindWithTag("Player").transform;

    void Update()
    {
        float interval = Mathf.Lerp(startInterval, minInterval, Time.timeSinceLevelLoad / rampSeconds);
        timer += Time.deltaTime;
        if (timer < interval || Alive >= maxAlive) return;
        timer = 0f;
        Spawn();
    }

    public void SpawnMany(int n) { for (int i = 0; i < n; i++) Spawn(); }

    void Spawn()
    {
        var data = Pick();
        if (data == null) return;
        Vector2 pos = (Vector2)player.position + Random.insideUnitCircle.normalized * radius;
        Pool.Get(enemyPrefab, pos, Quaternion.identity).GetComponent<Enemy>().Init(data);
    }

    EnemyData Pick()
    {
        int n = 0;
        foreach (var t in types) if (Time.timeSinceLevelLoad >= t.unlockTime) n++;
        return n == 0 ? null : types[Random.Range(0, n)];
    }
}
