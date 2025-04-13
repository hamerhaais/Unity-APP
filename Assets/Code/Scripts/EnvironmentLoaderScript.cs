using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvironmentLoaderScript : MonoBehaviour
{
    public int currentEnvironmentId;
    public Object2DApiClient apiClient;

    public Transform environmentObjectsParent;

    public GameObject deskPrefab;
    public GameObject couchPrefab;
    public GameObject coatRackPrefab;

    private void Start()
    {
        currentEnvironmentId = EnvironmentSession.environmentId;
        Debug.Log("Current Environment ID: " + currentEnvironmentId);
        LoadEnvironment();
    }
    public void Back()
    {
        SceneManager.LoadScene("HomeScene");
        Debug.Log("Back button clicked");
    }

    public async void ResetEnvironment()
    {
        await apiClient.DeleteAllObjectsFromEnvironment(currentEnvironmentId);
        SceneManager.LoadScene("EnvironmentScene");
    }

    public async void LoadEnvironment()
    {
        IWebRequestReponse response = await apiClient.ReadObject2Ds(currentEnvironmentId);

        if (response is WebRequestData<List<Object2D>> data)
        {
            foreach (var obj in data.Data)
            {
                GameObject prefab = GetPrefabFromType(obj.type);
                if (prefab == null) continue;

                GameObject instance = Instantiate(prefab);
                instance.transform.SetParent(environmentObjectsParent, true);
                instance.transform.position = new Vector3(obj.x, obj.y, 0f);
            }
        }
        else
        {
            Debug.LogError("Kon objecten niet laden");
        }
    }

    private GameObject GetPrefabFromType(string type)
    {
        switch (type.ToLower())
        {
            case "1":
                return deskPrefab;
            case "2":
                return couchPrefab;
            case "3":
                return coatRackPrefab;
            default:
                return null;
        }
    }
}
