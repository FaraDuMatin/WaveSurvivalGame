using System;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] float baseXp = 10f;
    [SerializeField] float xpGrowth = 1.3f;

    public int Level { get; private set; } = 1;
    public event Action OnLevelUp;

    float xp, xpToNext;
    public float Progress => xp / xpToNext;
    public string XpText => $"{(int)xp}/{(int)xpToNext}";

    void Awake() => xpToNext = baseXp;

    public void AddXp(float amount)
    {
        xp += amount * GetComponent<PlayerStats>().xpMult;
        while (xp >= xpToNext)
        {
            xp -= xpToNext;
            xpToNext *= xpGrowth;
            Level++;
            OnLevelUp?.Invoke();
        }
    }
}
