using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] UpgradeData[] pool;
    [SerializeField] GameObject panel;
    [SerializeField] Button[] buttons;

    PlayerStats stats;
    UpgradeData[] offered = new UpgradeData[3];

    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        stats = player.GetComponent<PlayerStats>();
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
        for (int i = 0; i < 3; i++)
        {
            offered[i] = pool[Random.Range(0, pool.Length)];
            buttons[i].GetComponentInChildren<Text>().text = offered[i].title;
        }
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    void Pick(int i)
    {
        offered[i].Apply(stats);
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
}
