using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void LoadPlantingMinigame()
    {
        SceneManager.LoadScene("Tree Planting Game");
    }

    public void LoadRecyclingMinigame()
    {
        SceneManager.LoadScene("Recycling Game");
    }

    public void LoadTriviaMinigame()
    {
        SceneManager.LoadScene("");
    }
}
