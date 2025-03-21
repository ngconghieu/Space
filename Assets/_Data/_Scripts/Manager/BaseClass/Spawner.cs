using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Spawner<T> : GameMonoBehaviour where T : GameMonoBehaviour
{
    [SerializeField] private Transform _holder;
    private readonly Dictionary<string, T> _prefabs = new();
    private readonly ObjectPool<T> _objectPool = new();

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHolder();
        LoadPrefabs();
    }

    private void LoadHolder()
    {
        if (_holder != null) return;
        _holder = transform.Find("Holder");
        if (_holder == null)
        {
            _holder = new GameObject("Holder").transform;
            _holder.transform.SetParent(transform);
        }
        //Debug.Log("LoadHolder", gameObject);
    }

    private void LoadPrefabs()
    {
        if (_prefabs.Count != 0) return;
        foreach (var prefab in transform.GetComponentsInChildren<T>())
        {
            _prefabs.Add(prefab.name, prefab);
            prefab.gameObject.SetActive(false);
        }
        //Debug.Log("LoadPrefabs", gameObject);
    }

    #endregion

    public virtual T Spawn(T prefab, Vector2 position, Quaternion rotation)
    {
        T newPrefab = _objectPool.GetFromPool(prefab);
        if (newPrefab != null)
        {
            newPrefab.transform.SetPositionAndRotation(position, rotation);
            return newPrefab;
        }
        newPrefab = Spawn(prefab);
        newPrefab.transform.SetPositionAndRotation(position, rotation);
        SubcribeEvent(newPrefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }

    public virtual T Spawn(T prefab)
    {
        T newPrefab = Instantiate(prefab, _holder.transform);
        newPrefab.gameObject.SetActive(true);
        return newPrefab;
    }

    public virtual void Despawn(T prefab)
    {
        _objectPool.AddToPool(prefab);
    }

    public T GetPrefab(string name)
    {
        if (!_prefabs.TryGetValue(name, out T prefab)) return null;
        return prefab;
    }

    protected abstract void SubcribeEvent(T prefab);
}