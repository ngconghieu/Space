using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private int _inventorySize = 7;
    [SerializeField] private AssetLabelReference _itemProfilesLabel;
    private readonly Dictionary<Const, ItemProfiles> _itemProfiles = new();
    [SerializeField] private List<Item> _items = new(); // ItemID, Item

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
            foreach (var itemProfiles in handle.Result)
                _itemProfiles.Add(itemProfiles.PrefabName, itemProfiles);
        };
    }
    #endregion

    #region AddItem
    public bool AddItem(Const itemName, int amount)
    {
        ItemProfiles itemProfiles = GetItemProfiles(itemName);
        if (amount <= 0 || itemProfiles == null) return false;

        List<Item> items = new(_items); // clone _items
        int currentAmount = amount;
        int cntSpace = 0;

        for (int i = 0; i < items.Count; i++)
        {
            if (cntSpace >= _inventorySize) break; // if inventory is full
            ++cntSpace;

            // if item is not same
            if (!items[i].ItemProfiles.PrefabName.Equals(itemProfiles.PrefabName)) continue;

            // if items is full
            if (items[i].Amount == itemProfiles.MaxStack) continue;

            items[i] = StackItem(items[i], ref currentAmount);
        }

        AddItemIntoNewSpaces(items, itemProfiles, ref currentAmount, ref cntSpace);
        if (currentAmount > 0) return false;
        _items = items;
        return true;
    }

    private Item StackItem(Item item, ref int currentAmount)
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
        return item;
    }

    private void AddItemIntoNewSpaces(List<Item> items, ItemProfiles itemProfiles, ref int currentAmount, ref int cntSpace)
    {
        while (currentAmount > 0 && ++cntSpace <= _inventorySize)
        {
            int amount = Math.Min(itemProfiles.MaxStack, currentAmount);
            currentAmount -= amount;

            string itemID = Guid.NewGuid().ToString();
            items.Add(new()
            {
                ItemID = itemID,
                ItemProfiles = itemProfiles,
                Amount = amount
            });
        }
    }
    #endregion

    #region RemoveItem
    public void RemoveItem(Const prefabName, int amount)
    {
        if (_items.Count == 0 || amount <= 0) return;
        ItemProfiles itemProfiles = GetItemProfiles(prefabName);
        if (!EnoughAmountToRemove(itemProfiles, amount)) return;
        for (int i = _items.Count - 1; i >= 0; i--)
        {
            if (!_items[i].ItemProfiles.Equals(itemProfiles)) continue;
            if (_items[i].Amount >= amount)
            {
                _items[i].Amount -= amount;
                break;
            }
            else
            {
                amount -= _items[i].Amount;
                _items[i].Amount = 0;
            }
        }
    }

    private bool EnoughAmountToRemove(ItemProfiles itemProfiles, int amount)
    {
        int totalAmount = 0;
        foreach (var item in _items)
        {
            if (item.ItemProfiles.PrefabName.Equals(itemProfiles.PrefabName))
                totalAmount += item.Amount;
        }
        return totalAmount >= amount;
    }
    #endregion

    public void RemoveEmptyItems() =>
        _items.RemoveAll(item => item.Amount == 0);

    public int InventorySize() => _inventorySize;

    public ItemProfiles GetItemProfiles(Const prefabName) =>
        _itemProfiles.TryGetValue(prefabName, out var value) ? value : null;

    public List<Item> GetItemList() => _items;

}

[Serializable]
public class Item
{
    public string ItemID;
    public ItemProfiles ItemProfiles;
    public int Amount;
}