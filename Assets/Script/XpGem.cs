using UnityEngine;

public class XpGem : MonoBehaviour
{
    public float value = 5f;
    [SerializeField] float magnetSpeed = 8f;

    Transform player;
    PlayerStats stats;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        stats = player.GetComponent<PlayerStats>();
    }

    void Update()
    {
        Vector3 to = player.position - transform.position;
        if (to.sqrMagnitude > stats.pickupRadius * stats.pickupRadius) return;
        transform.position += to.normalized * magnetSpeed * Time.deltaTime;
        if (to.sqrMagnitude < 0.1f)
        {
            player.GetComponent<PlayerLevel>().AddXp(value);
            Destroy(gameObject);
        }
    }
}
