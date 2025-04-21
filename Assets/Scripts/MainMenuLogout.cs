using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Authentication;

public class MainMenuManager : MonoBehaviour
{
    public void Logout()
    {
        if(AuthenticationService.Instance.IsSignedIn)
        {
            AuthenticationService.Instance.SignOut();
            Debug.Log("User logged out.");
        }

        SceneManager.LoadScene("Login Screen");
    }
}
