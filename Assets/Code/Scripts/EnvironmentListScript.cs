using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnvironmentListScript : MonoBehaviour
{
    public Transform contentParent;
    public GameObject environmentItemPrefab;
    public Object2DApiClient object2DApiClient;
    public Environment2DApiClient apiClient;

    private async void Start()
    {
        IWebRequestReponse response = await apiClient.ReadEnvironment2Ds();

        if (response is WebRequestData<List<Environment2D>> data)
        {
            foreach (var env in data.Data)
            {
                GameObject item = Instantiate(environmentItemPrefab, contentParent);

                item.transform.Find("NameText").GetComponent<TMP_Text>().text = env.name;

                item.transform.Find("OpenButton").GetComponent<Button>().onClick.AddListener(() =>
                {
                    EnvironmentSession.environmentId = env.id;
                    SceneManager.LoadScene("EnvironmentScene");
                });


                item.transform.Find("DeleteButton").GetComponent<Button>().onClick.AddListener(async () =>
                {
                    Debug.Log("Delete: " + env.id);
                    IWebRequestReponse deleteObjectsResponse = await object2DApiClient.DeleteAllObjectsFromEnvironment(env.id);
                    if (deleteObjectsResponse is WebRequestData<string> deleteObjectsData)
                    {
                        Debug.Log("Deleted all objects: " + deleteObjectsData.Data);
                    }
                    else
                    {
                        Debug.LogError("Failed to delete objects");
                    }
                    IWebRequestReponse deleteResponse = await apiClient.DeleteEnvironment(env.id);
                    if (deleteResponse is WebRequestData<string> deleteData)
                    {
                        Debug.Log("Deleted: " + deleteData.Data);
                        SceneManager.LoadScene("HomeScene");

                    }
                    else
                    {
                        Debug.LogError("Failed to delete environment");
                    }
                });
            }
        }
        else
        {
            Debug.LogError("Environments ophalen mislukt");
        }
    }
}
