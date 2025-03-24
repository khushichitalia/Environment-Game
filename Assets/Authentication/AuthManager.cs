using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.CloudSave;

public class AuthManager : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Button registerButton;
    public Button resetPasswordButton;
    public Text messageText;

    async void Start()
    {
        await InitializeAuthentication();
    }

    // initialize unity authentication
    async Task InitializeAuthentication()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services Initialized");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to initialize Unity Services: {ex.Message}");
        }
    }

    // register user with constraints
    public async void RegisterUser()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        // constraints on username and password
        if (username.Length < 3 || password.Length < 8)
        {
            messageText.text = "Invalid Username or Password!";
            return;
        }

        try
        {
            // check if registration works
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            messageText.text = "Registration Successful!";
            Debug.Log("SignUp is successful.");

            // store username after user is sucessfully registered
            await SaveUserData(username);

            // successful registration moves to main menu
            SceneManager.LoadScene("Main Menu");
        }
        catch (AuthenticationException ex)
        {
            messageText.text = "Registration Failed!";
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            messageText.text = "Request Failed!";
            Debug.LogException(ex);
        }
    }

    // allow user to log in  
    public async void LoginUser()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        try
        {
            // check if login works
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            messageText.text = "Login Successful!";
            Debug.Log("SignIn is successful.");

            // tentatively just storing arbitrary data
            await SaveUserData(username);

            // successful login moves to main menu
            SceneManager.LoadScene("Main Menu");
        }
        catch (AuthenticationException ex)
        {
            messageText.text = "Login Failed!";
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            messageText.text = "Request Failed!";
            Debug.LogException(ex);
        }
    }

    // reset password
    public async void ResetPassword()
    {
        string newPassword = passwordInput.text;

        // ensures new password is 8 or more chars
        if (newPassword.Length < 8)
        {
            messageText.text = "New Password is too short!";
            return;
        }

        try
        {
            // checks if update password works and resets it
            // will not move to main menu until user logs in with new password
            await AuthenticationService.Instance.UpdatePasswordAsync(passwordInput.text, newPassword);
            messageText.text = "Password Updated!";
            Debug.Log("Password updated.");
        }
        catch (AuthenticationException ex)
        {
            messageText.text = "Password Reset Failed!";
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            messageText.text = "Request Failed!";
            Debug.LogException(ex);
        }
    }

    // save user data in cloud save after login/registration
    async Task SaveUserData(string username)
    {
        // arbitrarily saving username and last login time
        var data = new Dictionary<string, object>
        {
            { "Username", username },
            { "Score", 0 },
            { "LastLoginTime", System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") }
        };

        try
        {
            await CloudSaveService.Instance.Data.ForceSaveAsync(data);
            Debug.Log("User data saved to Cloud Save.");
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError($"Failed to save user data: {ex.Message}");
        }
    }
}
