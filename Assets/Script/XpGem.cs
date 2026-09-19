using UnityEngine;

public class XpGem : MonoBehaviour
{
    public float value = 5f;
    [SerializeField] float magnetSpeed = 8f;

    static Transform player;
    static PlayerStats stats;
    static PlayerLevel level;

    void Awake()
    {
        if (player != null) return;
        player = GameObject.FindWithTag("Player").transform;
        stats = player.GetComponent<PlayerStats>();
        level = player.GetComponent<PlayerLevel>();
    }

    void Update()
    {
        Vector3 to = player.position - transform.position;
        if (to.sqrMagnitude > stats.pickupRadius * stats.pickupRadius) return;
        transform.position += to.normalized * magnetSpeed * Time.deltaTime;
        if (to.sqrMagnitude < 0.1f)
        {
            level.AddXp(value);
            Pool.Release(gameObject);
        }
    }
}
