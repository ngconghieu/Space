using System;
using UnityEngine;

public class ItemCtrl : GameMonoBehaviour
{
    [SerializeField] private DespawnItem _despawnItem;

    public DespawnItem DespawnItem => _despawnItem;

    private void Start()
    {
        _despawnItem.Initialize(this);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDesawnItem();
    }

    private void LoadDesawnItem()
    {
        if(_despawnItem != null) return;
        _despawnItem = GetComponentInChildren<DespawnItem>();
        Debug.Log("LoadDespawnItem", gameObject);
    }
}