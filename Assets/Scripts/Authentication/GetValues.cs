using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.CloudSave;

public class GetValues : MonoBehaviour
{

    public Text UsernameText;
    public Text ScoreText;
    
    public async void LoadData()
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {
            "Username", "Score"
        });
        
        if (playerData.TryGetValue("Username", out var Username)) {
            UsernameText.text = Username.Value.GetAs<string>();
        }

        if (playerData.TryGetValue("Score", out var Score)) {
            ScoreText.text = Score.Value.GetAs<string>();
        }
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called third
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadData();
    }
    
}
