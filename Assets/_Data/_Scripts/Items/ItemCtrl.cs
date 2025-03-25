using System;
using UnityEngine;

public class ItemCtrl : GameMonoBehaviour
{
    [SerializeField] private DespawnItem _despawnItem;
    [SerializeField] private ItemMovement _itemMovement;

    public DespawnItem DespawnItem => _despawnItem;
    public ItemMovement ItemMovement => _itemMovement;

    private void Start()
    {
        _despawnItem.Initialize(this);
        _itemMovement.Initialize(this);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDesawnItem();
        LoadItemMovement();
    }

    private void LoadItemMovement()
    {
        if (_itemMovement != null) return;
        _itemMovement = GetComponentInChildren<ItemMovement>();
        Debug.Log("LoadItemMovement", gameObject);
    }

    private void LoadDesawnItem()
    {
        if(_despawnItem != null) return;
        _despawnItem = GetComponentInChildren<DespawnItem>();
        Debug.Log("LoadDespawnItem", gameObject);
    }
}