using System;
using UnityEngine;

/**
 * Let op:
 * - kleine letters i.v.m. JsonUtility
 * - int voor id + environmentId
 */
[Serializable]
public class Object2D
{
    public int id;
    public string type;
    public float x;
    public float y;
    public int environmentId;
}
