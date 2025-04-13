using System.Collections.Generic;
using System;
using UnityEngine;

public static class JsonHelper
{
    public static List<T> ParseJsonArray<T>(string jsonArray)
    {
        string extendedJson = "{\"list\":" + jsonArray + "}";
        JsonList<T> parsedList = JsonUtility.FromJson<JsonList<T>>(extendedJson);
        return parsedList.list;
    }

    // Changed the return because the response is token not accesToken
    public static string ExtractToken(string data)
    {
        Token token = JsonUtility.FromJson<Token>(data);
        return token.token;
    }
}

[Serializable]
public class JsonList<T>
{
    public List<T> list;
}