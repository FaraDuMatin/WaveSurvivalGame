using UnityEngine;

public class Fx : MonoBehaviour
{
    [SerializeField] GameObject damageNumber, deathBurst, pickupBurst;
    static Fx I;

    void Awake() => I = this;

    public static void Damage(Vector3 pos, float amount) =>
        Pool.Get(I.damageNumber, pos, Quaternion.identity).GetComponent<TextMesh>().text = Mathf.RoundToInt(amount).ToString();

    public static void Death(Vector3 pos, Color color)
    {
        var main = Pool.Get(I.deathBurst, pos, Quaternion.identity).GetComponent<ParticleSystem>().main;
        main.startColor = color;
    }

    public static void Pickup(Vector3 pos) => Pool.Get(I.pickupBurst, pos, Quaternion.identity);
}
