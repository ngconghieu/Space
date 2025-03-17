using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : Singleton<Spawner<T>> where T : GameMonoBehaviour
{
    [SerializeField] private Transform _holder;
    [SerializeField] protected List<T> _prefabs = new();
    [SerializeField] private List<T> _pools = new();

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
        Debug.Log("LoadHolder", gameObject);
    }

    private void LoadPrefabs()
    {
        if (_prefabs.Count != 0) return;
        foreach (var prefab in transform.GetComponentsInChildren<T>())
        {
            _prefabs.Add(prefab);
            prefab.gameObject.SetActive(false);
        }
        Debug.Log("LoadPrefabs", gameObject);
    }

    #endregion

    public virtual T Spawn(T prefab, Vector2 position, Quaternion rotation)
    {
        T newPrefab = GetPrefabFromPool(prefab);
        newPrefab.transform.SetPositionAndRotation(position, rotation);
        newPrefab.gameObject.SetActive(true);
        newPrefab.name = prefab.name;
        return newPrefab;
    }

    private T GetPrefabFromPool(T prefab)
    {
        for (int i = 0; i < _prefabs.Count; i++)
        {
            if (_prefabs[i].name.Equals(prefab.name))
                return _prefabs[i];
        }
        //create new prefab
        return Spawn(prefab);
    }

    public virtual T Spawn(T prefab)
    {
        T newPrefab = Instantiate(prefab, _holder.transform);
        newPrefab.gameObject.SetActive(true);
        return newPrefab;
    }

    public virtual void Despawn(T prefab)
    {
        _pools.Add(prefab);
        prefab.gameObject.SetActive(false);
    }

    public abstract T GetPrefab(int prefab);
}