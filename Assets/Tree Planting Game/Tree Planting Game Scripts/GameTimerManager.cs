using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimerManager : MonoBehaviour
{
    public float gameDuration = 60f;
    private float timeRemaining;
    public Text timerText;

    private bool gameRunning = true;

    void Start()
    {
        timeRemaining = gameDuration;
        UpdateTimerUI();
    }

    void Update()
    {
        if (!gameRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            gameRunning = false;
            EndGame();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = "Time Left: " + seconds.ToString();
    }

    void EndGame()
    {
        Debug.Log("Time's up!");

        GameSessionManager.Instance.finalScore = ScoreManager.Instance.currentScore;
        GameSessionManager.Instance.lastMinigameSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene("End Screen");
    }
}
