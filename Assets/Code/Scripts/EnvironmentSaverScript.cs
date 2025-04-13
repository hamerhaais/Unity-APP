using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSaverScript : MonoBehaviour
{
    public Transform environmentParent; // Dit is EnvironmentObjects, hierin zitten alle clones van de prefabs
    public Object2DApiClient apiClient;

    private int currentEnvironmentId;

    private void Start()
    {
        currentEnvironmentId = EnvironmentSession.environmentId;
        Debug.Log("Current Environment ID: " + currentEnvironmentId);
    }

    public async void SaveEnvironment()
    {
        // 1. Verwijder eerst alles in DB
        await apiClient.DeleteAllObjectsFromEnvironment(currentEnvironmentId);

        // 2. Verzamel nieuwe objecten
        List<Object2D> newObjects = new List<Object2D>();

        // 3. Loop door alle clones die in de EnvironmentObjects zitten heen
        foreach (Transform child in environmentParent)
        {
            Object2D obj = new Object2D
            {
                id = 0, // wordt gegenereerd door backend
                type = child.name.Replace("(Clone)", "").Trim(),
                x = child.position.x,
                y = child.position.y,
                environmentId = currentEnvironmentId
            };

            // 4. Maak deze objecten aan in de DB
            await apiClient.CreateObject2D(obj);
        }
    }
}
