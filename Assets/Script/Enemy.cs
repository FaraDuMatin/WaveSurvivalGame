using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject gemPrefab;

    public EnemyData Data { get; private set; }

    Rigidbody2D rb;
    Transform player;
    float hp;

    public void Init(EnemyData data)
    {
        Data = data;
        hp = data.hp;
        transform.localScale = Vector3.one * data.scale;
        GetComponent<SpriteRenderer>().color = data.color;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void OnEnable() => EnemySpawner.Alive++;
    void OnDisable() => EnemySpawner.Alive--;

    void FixedUpdate()
    {
        rb.linearVelocity = (player.position - transform.position).normalized * Data.speed;
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp > 0f) return;
        Instantiate(gemPrefab, transform.position, Quaternion.identity).GetComponent<XpGem>().value = Data.xp;
        Destroy(gameObject);
    }
}
