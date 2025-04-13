using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

public class UserApiClient : MonoBehaviour
{
    // Changed the route from account to auth   

    public async Awaitable<IWebRequestReponse> Register(User user)
    {
        string route = "/auth/register";
        string data = JsonUtility.ToJson(user);

        return await WebClient.instance.SendPostRequest(route, data);
    }

    public async Awaitable<IWebRequestReponse> Login(User user)
    {
        string route = "/auth/login";
        string data = JsonUtility.ToJson(user);

        IWebRequestReponse response = await WebClient.instance.SendPostRequest(route, data);
        return ProcessLoginResponse(response);
    }

    private IWebRequestReponse ProcessLoginResponse(IWebRequestReponse webRequestResponse)
    {
        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Response data raw: " + data.Data);
                string token = JsonHelper.ExtractToken(data.Data);
                WebClient.instance.SetToken(token);
                return new WebRequestData<string>("Succes");
            default:
                return webRequestResponse;
        }
    }
}

