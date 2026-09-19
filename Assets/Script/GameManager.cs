using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum State { Menu, Playing, Paused, GameOver }

    [SerializeField] GameObject menuPanel, pausePanel, gameOverPanel;
    [SerializeField] Text bestText, resultText;

    public static GameManager I { get; private set; }
    public State Current { get; private set; }
    public float Elapsed { get; private set; }
    public int Kills;

    SaveSystem.Data save;
    static bool skipMenu;

    void Awake() { I = this; save = SaveSystem.Load(); }

    void Start()
    {
        bestText.text = $"best  {Fmt(save.bestTime)}  ·  {save.bestKills} kills";
        Set(skipMenu ? State.Playing : State.Menu);
        skipMenu = false;
    }

    void Update()
    {
        if (Current == State.Playing) Elapsed += Time.deltaTime;
        if (!Keyboard.current.escapeKey.wasPressedThisFrame) return;
        if (Current == State.Playing && Time.timeScale > 0f) Set(State.Paused);
        else if (Current == State.Paused) Set(State.Playing);
    }

    void Set(State s)
    {
        Current = s;
        menuPanel.SetActive(s == State.Menu);
        pausePanel.SetActive(s == State.Paused);
        gameOverPanel.SetActive(s == State.GameOver);
        Time.timeScale = s == State.Playing ? 1f : 0f;
    }

    public void Play() => Set(State.Playing);
    public void Resume() => Set(State.Playing);
    public void Retry() { skipMenu = true; Restart(); }
    public void Restart() { Time.timeScale = 1f; SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

    public void GameOver()
    {
        if (Current != State.Playing) return;
        bool newBest = Elapsed > save.bestTime || Kills > save.bestKills;
        save.bestTime = Mathf.Max(save.bestTime, Elapsed);
        save.bestKills = Mathf.Max(save.bestKills, Kills);
        SaveSystem.Save(save);
        resultText.text = $"survived {Fmt(Elapsed)}  ·  {Kills} kills" + (newBest ? "\nNEW BEST" : "");
        Set(State.GameOver);
    }

    public static string Fmt(float t) => $"{(int)t / 60}:{(int)t % 60:00}";
}
