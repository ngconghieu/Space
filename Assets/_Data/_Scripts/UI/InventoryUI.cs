using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUI : GameMonoBehaviour
{
    [SerializeField] private ItemSlot _itemSlotDefault;
    [SerializeField] private Transform _scrollView;
    [SerializeField] private Transform _holder;
    [SerializeField] private bool _showInventory = true;
    private readonly List<ItemSlot> _itemSlotList = new();

    public Transform Holder => _holder;

    private void Start()
    {
        Initialize();
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHolder();
        LoadScrollView();
        LoadItemSlotDefault();
        ServiceLocator.Register<InventoryUI>(this);
    }

    private void LoadHolder()
    {
        if (_holder != null) return;
        _holder = transform.Find("Holder");
        Debug.Log("LoadHolder", gameObject);
    }

    private void LoadScrollView()
    {
        if (_scrollView != null) return;
        _scrollView = transform.Find("Scroll View");
        Debug.Log("LoadScrollView", gameObject);
    }

    private void LoadItemSlotDefault()
    {
        if (_itemSlotDefault != null) return;
        _itemSlotDefault = GetComponentInChildren<ItemSlot>();
        _itemSlotDefault.SetIndex(0);
        Debug.Log("LoadBtnDefault", gameObject);
    }
    #endregion

    #region Initialize
    private void Initialize()
    {
        ToggleInventory();
        InputManager.Instance.HandleInventoryToggle += ToggleInventory;
        _itemSlotList.Add(_itemSlotDefault);
        LoadItemSlots();
    }

    private void LoadItemSlots()
    {
        int inventorySize = InventoryManager.Instance.InventorySize();

        for (int i = _itemSlotList.Count; i < inventorySize; i++)
        {
            ItemSlot newBtnItem = Instantiate(_itemSlotDefault, _itemSlotDefault.transform.parent);
            newBtnItem.name = $"ItemSlot_{i}";
            newBtnItem.SetIndex(i);
            _itemSlotList.Add(newBtnItem);
        }
    }
    #endregion

    public void ToggleInventory()
    {
        _showInventory = !_showInventory;
        if (_showInventory)
            HandleItemChange();
        _scrollView.gameObject.SetActive(_showInventory);
    }

    #region HandleItemChange
    public void HandleItemChange()
    {
        List<Item> items = InventoryManager.Instance.GetItemList();
        UpdateExistingItems(items);
        AddNewItems(items);
        InventoryManager.Instance.RemoveEmptyItems();
    }

    private void UpdateExistingItems(List<Item> items)
    {
        foreach (var item in items)
        {
            ItemSlot itemSlot = FindItemSlotById(item.ItemID);
            if (itemSlot != null)
            {
                if (item.Amount == 0)
                    itemSlot.ItemUI.SetDefault();
                else
                    itemSlot.ItemUI.SetAmount(item.Amount);
            }
        }
    }

    private void AddNewItems(List<Item> items)
    {
        foreach (var item in items)
        {
            if (FindItemSlotById(item.ItemID) != null) continue;
            ItemSlot emptySlot = FindEmptyItemSlot();
            if (emptySlot == null) continue;

            ItemUI itemUI = emptySlot.ItemUI;
            itemUI.SetItemId(item.ItemID);
            itemUI.SetImage(item.ItemProfiles.ItemIcon);
            itemUI.SetAmount(item.Amount);
            emptySlot.SetItemUI(itemUI);
        }
    }


    private ItemSlot FindItemSlotById(string itemId)
    {
        foreach (var slot in _itemSlotList)
        {
            if (slot.ItemUI.ItemId == itemId)
                return slot;
        }
        return null;
    }

    private ItemSlot FindEmptyItemSlot() =>
        _itemSlotList.Find(slot => slot.ItemUI.CheckEmptyItem());
    #endregion

    public List<ItemSlot> GetItemSlotList => _itemSlotList;
}
