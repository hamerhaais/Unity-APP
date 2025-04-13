using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateEnvironmentScript : MonoBehaviour
{
    public TMP_InputField nameText;

    public TMP_Text errorText;
    public Image errorBackground;

    public GameObject createPopUp;

    public Environment2DApiClient environment2DApiClient;

    public Button createButton;
    public Button cancelButton;

    public async void CreateEnvironment()
    {
        createPopUp.SetActive(false);
        Environment2D environment = new Environment2D
        {
            name = nameText.text,
            maxX = 50,
            maxY = 50
        };

        IWebRequestReponse response = await environment2DApiClient.CreateEnvironment(environment);
        if (response is WebRequestData<Environment2D> webRequestResponse)
        {
            SceneManager.LoadScene("HomeScene");
        }
        else if (response is WebRequestError errorResponse)
        {
            errorText.text = "Create Environment Failed: Name taken/ not 1-25 characters or limit reached.";
            errorBackground.gameObject.SetActive(true);
            errorText.gameObject.SetActive(true);
        }
        else
        {
            
            errorText.text = "An unknown error occurred";
            errorBackground.gameObject.SetActive(true);
            errorText.gameObject.SetActive(true);
        }        
    }

    public void Cancel()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
