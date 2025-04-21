using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.Core;

public class TestSciptForIncreasePoints : MonoBehaviour
{
    
    public async void SaveScore()
    {
        string User = "", LastLog = "";
        int currentScore = 0;
        
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {
            "Username", "Score", "LastLoginTime"
        });
        
        if (playerData.TryGetValue("Username", out var Username)) {
            User = Username.Value.GetAs<string>();
        }
        
        if (playerData.TryGetValue("Score", out var Score)) {
            currentScore = Score.Value.GetAs<int>();
        }

        if (playerData.TryGetValue("LastLoginTime", out var LastLoginTime)) {
            LastLog = LastLoginTime.Value.GetAs<string>();
        }
        
        var newPlayerdata = new Dictionary<string, object>
        {
            { "Username", User },
            { "Score", currentScore + GameSessionManager.Instance.finalScore},
            { "LastLoginTime", LastLog } 
        };

        try
        {
            await CloudSaveService.Instance.Data.ForceSaveAsync(newPlayerdata);
            Debug.Log("User data saved to Cloud Save.");
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError($"Failed to save user data: {ex.Message}");
        }
    }
}
