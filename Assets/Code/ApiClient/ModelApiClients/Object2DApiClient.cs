using System.Collections.Generic;
using UnityEngine;

public class Object2DApiClient : MonoBehaviour
{
    public async Awaitable<IWebRequestReponse> ReadObject2Ds(int environmentId)
    {
        string route = "/environments/" + environmentId + "/objects";
        IWebRequestReponse response = await WebClient.instance.SendGetRequest(route);
        return ParseObject2DListResponse(response);
    }

    public async Awaitable<IWebRequestReponse> CreateObject2D(Object2D object2D)
    {
        string route = "/environments/" + object2D.environmentId + "/objects";
        string data = JsonUtility.ToJson(object2D);
        IWebRequestReponse response = await WebClient.instance.SendPostRequest(route, data);
        return ParseObject2DResponse(response);
    }

    public async Awaitable<IWebRequestReponse> UpdateObject2D(Object2D object2D)
    {
        string route = "/environments/" + object2D.environmentId + "/objects/" + object2D.id;
        string data = JsonUtility.ToJson(object2D);
        return await WebClient.instance.SendPutRequest(route, data);
    }

    public async Awaitable<IWebRequestReponse> DeleteObject2D(int environmentId, int objectId)
    {
        string route = "/environments/" + environmentId + "/objects/" + objectId;
        return await WebClient.instance.SendDeleteRequest(route);
    }

    // Added this function to delete all objects from an environment so resetting the environment works and so saving is easier (by first deleting all objects and then creating new ones)
    public async Awaitable<IWebRequestReponse> DeleteAllObjectsFromEnvironment(int environmentId)
    {
        string route = "/environments/" + environmentId + "/objects";
        return await WebClient.instance.SendDeleteRequest(route);
    }

    private IWebRequestReponse ParseObject2DResponse(IWebRequestReponse response)
    {
        switch (response)
        {
            case WebRequestData<string> data:
                Debug.Log("Response data raw: " + data.Data);
                Object2D parsed = JsonUtility.FromJson<Object2D>(data.Data);
                return new WebRequestData<Object2D>(parsed);
            default:
                return response;
        }
    }

    private IWebRequestReponse ParseObject2DListResponse(IWebRequestReponse response)
    {
        switch (response)
        {
            case WebRequestData<string> data:
                Debug.Log("Response data raw: " + data.Data);
                List<Object2D> list = JsonHelper.ParseJsonArray<Object2D>(data.Data);
                return new WebRequestData<List<Object2D>>(list);
            default:
                return response;
        }
    }
}
