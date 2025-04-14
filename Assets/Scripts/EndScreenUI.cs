using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenUI : MonoBehaviour
{
    public Text finalScoreText;

    void Start()
    {
        finalScoreText.text = "Final Score: " + GameSessionManager.Instance.finalScore.ToString();
    }

    public void OnPlayAgain()
    {
        string lastScene = GameSessionManager.Instance.lastMinigameSceneName;
        if (!string.IsNullOrEmpty(lastScene))
        {
            SceneManager.LoadScene(lastScene);
        }
    }

    public void OnBackHome()
    {
        SceneManager.LoadScene("Main Menu"); // or whatever your home scene is called
    }
}
