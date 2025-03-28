using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new();

    public static void Register<T>(object service)
    {
        _services[typeof(T)] = service;
    }

    public static T Get<T>()
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
            return (T)_services[type];
        Debug.LogError($"Service of type {type} not found");
        return default;
    }
}