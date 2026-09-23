using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject gemPrefab;

    public EnemyData Data { get; private set; }

    Rigidbody2D rb;
    Flash flash;
    Transform player;
    float hp;

    public void Init(EnemyData data)
    {
        Data = data;
        hp = data.hp;
        transform.localScale = Vector3.one * data.scale;
        GetComponent<SpriteRenderer>().color = data.frames.Length > 0 ? Color.white : data.color;
        GetComponent<SpriteAnim>().frames = data.frames;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        flash = GetComponent<Flash>();
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
        flash.Hit(); Sfx.Play(Sfx.I.hit, 0.4f);
        Fx.Damage(transform.position, amount);
        if (hp > 0f) return;
        Fx.Death(transform.position, Data.color); Sfx.Play(Sfx.I.death, 0.6f);
        GameManager.I.Kills++;
        Pool.Get(gemPrefab, transform.position, Quaternion.identity).GetComponent<XpGem>().value = Data.xp;
        Pool.Release(gameObject);
    }
}
