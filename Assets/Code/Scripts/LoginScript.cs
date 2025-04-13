using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;

    public UserApiClient userApiClient;

    public TMP_Text errorText;

    public async void Login()
    {
        IWebRequestReponse response = await userApiClient.Login(new User()
        {
            email = email.text,
            password = password.text
        });
        Debug.Log(response);

        if (response is WebRequestData<string> webRequestResponse)
        {
            SceneManager.LoadScene("HomeScene");
        }
        else if (response is WebRequestError errorResponse)
        {
            errorText.gameObject.SetActive(true);
            errorText.text = $"Login failed: User doesn't exist, password is incorrect or something else failed";
        }
        else
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "An unknown error occurred";
        }
    }

    public void Register()
    {
        SceneManager.LoadScene("RegisterScene");
    }
}
