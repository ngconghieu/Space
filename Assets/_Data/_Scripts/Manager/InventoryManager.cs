using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private int _maxInventorySize = 10;
    [SerializeField] private AssetLabelReference _itemProfilesLabel;
    [SerializeField] private List<ItemProfiles> _itemProfiles = new();
    [SerializeField] private Dictionary<string, Item> _items = new(); // ItemID, Item

    #region Test
    [SerializeField] private List<Item> _listItems = new();
    private void Test()
    {
        _listItems = new(_items.Values);
    }
    #endregion

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItemProfiles();
    }

    private void LoadItemProfiles()
    {
        Addressables.LoadAssetsAsync<ItemProfiles>(_itemProfilesLabel, null).Completed += handle =>
        {
            _itemProfiles = new(handle.Result);
        };
    }
    #endregion

    #region AddItem
    public void AddItem(ItemName itemName, int amount)
    {
        ItemProfiles itemProfiles = GetItemProfiles(itemName);
        if (amount <= 0 || itemProfiles == null) return;

        Dictionary<string, Item> items = new(_items); // clone _items
        int currentAmount = amount;
        int cntSpace = 0;

        foreach (string key in _items.Keys)
        {
            if (cntSpace >= _maxInventorySize) break; // if inventory is full
            ++cntSpace;

            Item item = _items[key];

            // if item is not same
            if (!item.ItemProfiles.ItemName.Equals(itemProfiles.ItemName)) continue;

            // if items is full
            if (item.Amount == itemProfiles.MaxStack) continue;

            StackItem(ref item, ref currentAmount);
            items[key] = item;
        }

        AddItemIntoNewSpaces(items, itemProfiles, ref currentAmount, ref cntSpace);
        if (currentAmount > 0) return;
        _items = items;
        Test();
    }

    private void StackItem(ref Item item, ref int currentAmount)
    {
        int amount = item.ItemProfiles.MaxStack - item.Amount;
        if (currentAmount >= amount)
        {
            item.Amount += amount;
            currentAmount -= amount;
        }
        else
        {
            item.Amount += currentAmount;
            currentAmount = 0;
        }
    }

    private void AddItemIntoNewSpaces(Dictionary<string, Item> items, ItemProfiles itemProfiles, ref int currentAmount, ref int cntSpace)
    {
        while (currentAmount > 0 && ++cntSpace <= _maxInventorySize)
        {
            int amount = Math.Min(itemProfiles.MaxStack, currentAmount);
            currentAmount -= amount;

            string itemID = Guid.NewGuid().ToString();
            items.Add(itemID, new()
            {
                ItemID = itemID,
                ItemProfiles = itemProfiles,
                Amount = amount
            });
        }
    }
    #endregion

    #region RemoveItem
    
    #endregion
    public ItemProfiles GetItemProfiles(ItemName itemName) => 
        _itemProfiles.Find(item => item.ItemName == itemName);
}

[Serializable]
public struct Item
{
    public string ItemID;
    public ItemProfiles ItemProfiles;
    public int Amount;
}