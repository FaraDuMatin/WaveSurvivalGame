using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] float outTime = 0.6f;

    Vector2 dir;
    float speed, t;
    Transform owner;

    public void Init(Vector2 d, float spd, float dmg, Transform o)
    {
        dir = d; speed = spd; owner = o;
        GetComponent<Hitbox>().damage = dmg;
    }

    void Update()
    {
        t += Time.deltaTime;
        transform.Rotate(0, 0, 720f * Time.deltaTime);
        if (t < outTime) { transform.position += (Vector3)dir * speed * Time.deltaTime; return; }
        Vector3 to = owner.position - transform.position;
        if (to.sqrMagnitude < 0.25f) { Destroy(gameObject); return; }
        transform.position += to.normalized * speed * Time.deltaTime;
    }
}
