using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : GameMonoBehaviour
{
    private Dictionary<string, T> _pools = new();

    public T GetFromPool(T prefab)
    {
        if(!_pools.TryGetValue(prefab.name, out T pooledPrefab)) return null;
        pooledPrefab.gameObject.SetActive(true);
        _pools.Remove(prefab.name);
        return pooledPrefab;
    }

    public void AddToPool(T prefab)
    {
        if (_pools.ContainsKey(prefab.name)) return;
        _pools.Add(prefab.name, prefab);
        prefab.gameObject.SetActive(false);
    }

}