using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : GameMonoBehaviour
{
    private readonly Dictionary<string, Queue<T>> _pools = new();

    public T GetFromPool(T prefab)
    {
        if (!_pools.TryGetValue(prefab.name, out var pooledPrefab)) return null;
        if (pooledPrefab.Count == 0) return null;
        var _prefab = pooledPrefab.Dequeue();
        _prefab.gameObject.SetActive(true);
        return _prefab;
    }

    public void AddToPool(T prefab)
    {
        if (prefab == null) return;
        if (_pools.TryGetValue(prefab.name, out var values))
            values.Enqueue(prefab);
        else
        {
            Queue<T> queue = new();
            queue.Enqueue(prefab);
            _pools.Add(prefab.name, queue);
        }
        prefab.gameObject.SetActive(false);
    }

}