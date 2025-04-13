using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RegisterScript : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_InputField passwordConfirm;

    public UserApiClient UserApiClient;

    public TMP_Text errorText;

    public async void Register()
    {
        if (password.text != passwordConfirm.text)
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Passwords do not match";
            return;
        }
        if (!IsCorrectPassword(password.text))
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Password must contain at least 10 characters, a digit, a lowercase letter, an uppercase letter, and a non-alphanumeric character.";
            return;
        }
        if (email.text == "")
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Email cannot be empty";
            return;
        }
        if (IsValidEmail(email.text) == false)
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Email is not valid";
            return;
        }

        IWebRequestReponse response = await UserApiClient.Register(new User()
        {
            email = email.text,
            password = password.text
        });

        if (response is WebRequestData<string> webRequestResponse)
        {
            SceneManager.LoadScene("LoginScene");
        }
        else if (response is WebRequestError errorResponse)
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Register Failed: User already exists or something else failed.";
        }
        else
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "An unknown error occurred";
        }
    }

    private bool IsCorrectPassword(string password)
    {
        if (password.Length < 10)
            return false;

        bool hasUpperCase = false;
        bool hasLowerCase = false;
        bool hasDigit = false;
        bool hasNonAlphanumeric = false;

        foreach (char c in password)
        {
            if (char.IsUpper(c)) hasUpperCase = true;
            if (char.IsLower(c)) hasLowerCase = true;
            if (char.IsDigit(c)) hasDigit = true;
            if (!char.IsLetterOrDigit(c)) hasNonAlphanumeric = true;
        }

        return hasUpperCase && hasLowerCase && hasDigit && hasNonAlphanumeric;
    }

    public void Login()
    {
        SceneManager.LoadScene("LoginScene");
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addresObject = new System.Net.Mail.MailAddress(email);
            return addresObject.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
