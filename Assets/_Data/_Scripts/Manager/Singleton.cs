using System;
using UnityEngine;

public class Singleton<T> : GameMonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance => _instance;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadInstance();
    }

    private void LoadInstance()
    {
        if(_instance != null)
        {
            Debug.LogError("Singleton already exists.", gameObject);
            return;
        }
        _instance = this as T;
        DontDestroyOnLoad(gameObject);
    }
}