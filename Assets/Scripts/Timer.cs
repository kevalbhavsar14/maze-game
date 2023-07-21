using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Tooltip("Time limit to complete the maze in seconds")]
    [SerializeField] float TimeLimit = 150;
    public float TimeRemaining { get; private set; } = 0;

    [Header("References")]
    [SerializeField] TMP_Text TimerTextUI;

    Color EndTimerColor = new(0.8f, 0, 0);

    
    void Start()
    {
        TimeRemaining = TimeLimit;
    }

    void Update()
    {
        if (GameManager.Instance.GameState != GameState.Running)
            return;
        TimeRemaining -= Time.deltaTime;
        TimerTextUI.text = ((int) TimeRemaining).ToString();
        if (TimeRemaining < 10)
            TimerTextUI.color = EndTimerColor;
        if (TimeRemaining <= 0)
            GameManager.Instance.GameState = GameState.Lost;
    }
}
