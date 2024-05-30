using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static T GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<T>();
        }
        else if (instance != FindObjectOfType<T>())
        {
            Destroy(FindObjectOfType<T>());
        }
        return instance;
    }
    public static T Instance => GetInstance();

}
