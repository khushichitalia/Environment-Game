using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimerManager : MonoBehaviour
{
    public float gameDuration = 60f; // total seconds
    private float timeRemaining;
    public Text timerText; // assign in inspector

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
        timerText.text = "Time: " + seconds.ToString();
    }

    void EndGame()
    {
        Debug.Log("Time's up!");

        // Set up the session for the end screen
        GameSessionManager.Instance.finalScore = ScoreManager.Instance.currentScore;
        GameSessionManager.Instance.lastMinigameSceneName = "Tree Planting Game"; // name of your tree game scene

        // Load the modular end screen
        SceneManager.LoadScene("End Screen");
    }
}
