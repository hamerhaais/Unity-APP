using UnityEngine;

public class Logic : MonoBehaviour
{
    public GameObject spritePrefab1;
    public GameObject spritePrefab2;
    public GameObject spritePrefab3;
    public Transform environmentObjectsParent;

    public Vector3 position;
    public void Button1Pressed()
    {
        Debug.Log("1");
        GetNewObject("1");
    }

    public void Button2Pressed()
    {
        Debug.Log("2");
        GetNewObject("2");
    }

    public void Button3Pressed()
    {
        GetNewObject("3");
    }


    private void GetNewObject(string prefabObject)
    {
        GameObject spawned = null;

        switch (prefabObject)
        {
            case "1":
                spawned = Instantiate(spritePrefab1, position, Quaternion.identity);
                break;
            case "2":
                spawned = Instantiate(spritePrefab2, position, Quaternion.identity);
                break;
            case "3":
                spawned = Instantiate(spritePrefab3, position, Quaternion.identity);
                break;
        }

        if (spawned != null)
        {
            spawned.transform.SetParent(environmentObjectsParent, true);
        }

    }
}