using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState GameState = GameState.None;
    public delegate void GameStateChange();
    public static event GameStateChange OnGameStateChanged;

    private GameState prevGameState = GameState.None;

    [Header("References")]
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject pauseScreen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        OnGameStateChanged += OnGameStateChange;
    }

    private void Update()
    {
        //DontDestroyOnLoad(this);
        if (GameState != prevGameState && OnGameStateChanged != null)
        {
            OnGameStateChanged();
        }
        prevGameState = GameState;
    }

    void OnGameStateChange()
    {
        if (GameState == GameState.None)
        { }
        if (GameState == GameState.Starting)
        {
            GameObject.Find("Walls").GetComponent<MazeGenerator>().Generate();
            GameManager.Instance.GameState = GameState.Running;
        }
        if (GameState == GameState.Running)
        {
            if (pauseScreen != null) pauseScreen.SetActive(false);
            EnableGame();
        }
        if (GameState == GameState.Paused)
        {
            if (pauseScreen != null) pauseScreen.SetActive(true);
            DisableGame();
        }
        if (GameState == GameState.Won)
        {
            DisableGame();
            ShowEndScreen("Won");
        }
        if (GameState == GameState.Lost)
        {
            DisableGame();
            ShowEndScreen("Lost");
        }
    }

    void DisableGame()
    {
        Time.timeScale = 0f;
        GameObject.Find("Maze").GetComponent<Controller>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    void EnableGame()
    {
        Time.timeScale = 1f;
        GameObject.Find("Maze").GetComponent<Controller>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void ShowEndScreen(string gameStatus)
    {
        Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
        if (endScreen != null)
        {
            endScreen.SetActive(true);
            endScreen.transform.Find("GameStatus").GetComponent<TMP_Text>().text = gameStatus;
            endScreen.transform.Find("TimeRemaining").GetComponent<TMP_Text>().text = $"Time Remaining: {System.Math.Round(timer.TimeRemaining, 2)} Seconds";
        }
    }
}

public enum GameState
{
    None,
    Starting,
    Running,
    Paused,
    Won,
    Lost
}

