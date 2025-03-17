using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : Singleton<Spawner<T>> where T : GameMonoBehaviour
{
    [SerializeField] private GameObject _prefabHolder;
    [SerializeField] private List<T> _prefabs;
    [SerializeField] private List<T> _pools;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHolder();
    }

    private void LoadHolder()
    {
        if (_prefabHolder != null) return;
        _prefabHolder = transform.Find("Holder").gameObject;
        if (_prefabHolder == null)
        {
            _prefabHolder = new GameObject("Holder");
            _prefabHolder.transform.SetParent(transform);
        }
        Debug.Log("LoadHolder", gameObject);
    }

    private void LoadPrefabs()
    {
        if (_prefabs.Count > 0) return;
        _prefabs.AddRange(_prefabHolder.GetComponentsInChildren<T>());
        Debug.Log("LoadPrefabs", gameObject);
    }

    #endregion

    public virtual T Spawn(T prefab, Vector2 position, Quaternion rotation)
    {
        T newPrefab = GetPrefabFromPool(prefab);
        newPrefab.transform.position = position;
        newPrefab.transform.rotation = rotation;
        return newPrefab;
    }

    private T GetPrefabFromPool(T prefab)
    {
        for(int i = 0; i< _prefabs.Count; i++)
        {
            if (!_prefabs[i].name.Equals(prefab.name))
                return _prefabs[i];
        }
        //create new prefab
        return Spawn(prefab);
    }

    public virtual T Spawn(T prefab)
    {
        T newPrefab = Instantiate(prefab, _prefabHolder.transform);
        newPrefab.gameObject.SetActive(true);
        return newPrefab;
    }

    public virtual void Despawn(T prefab)
    {
        _pools.Add(prefab);
        prefab.gameObject.SetActive(false);
    }
}