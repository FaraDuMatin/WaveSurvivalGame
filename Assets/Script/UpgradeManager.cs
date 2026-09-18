using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] UpgradeData[] pool;
    [SerializeField] GameObject panel;
    [SerializeField] Button[] buttons;

    PlayerStats stats;
    WeaponHolder holder;
    List<UpgradeData> offered = new();

    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        stats = player.GetComponent<PlayerStats>();
        holder = player.GetComponent<WeaponHolder>();
        player.GetComponent<PlayerLevel>().OnLevelUp += Show;
        for (int i = 0; i < buttons.Length; i++)
        {
            int idx = i;
            buttons[i].onClick.AddListener(() => Pick(idx));
        }
        panel.SetActive(false);
    }

    void Show()
    {
        var available = new List<UpgradeData>();
        foreach (var u in pool) if (u.Available(holder)) available.Add(u);
        offered.Clear();
        for (int i = 0; i < buttons.Length; i++)
        {
            bool has = available.Count > 0;
            buttons[i].gameObject.SetActive(has);
            if (!has) continue;
            var u = available[Random.Range(0, available.Count)];
            available.Remove(u);
            offered.Add(u);
            buttons[i].GetComponentInChildren<Text>().text = u.Title(holder);
        }
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    void Pick(int i)
    {
        offered[i].Apply(holder, stats);
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
}
