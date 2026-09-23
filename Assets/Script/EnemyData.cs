using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    public float hp = 30f;
    public float speed = 2f;
    public float damagePerSecond = 20f;
    public float xp = 5f;
    public float scale = 1f;
    public Color color = Color.red;
    public Sprite[] frames;
    public float unlockTime;
}
