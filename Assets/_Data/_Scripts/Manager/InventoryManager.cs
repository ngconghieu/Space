using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private int _maxInventorySize = 10;
    [SerializeField] private AssetLabelReference _itemProfilesLabel;
    private Dictionary<PrefabName, ItemProfiles> _itemProfiles = new();
    [SerializeField] private Dictionary<string, Item> _items = new(); // ItemID, Item
    public event Action OnItemChange;

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
            foreach (var itemProfiles in handle.Result)
                _itemProfiles.Add(itemProfiles.PrefabName, itemProfiles);
        };
    }
    #endregion

    #region AddItem
    public void AddItem(PrefabName itemName, int amount)
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
            if (!item.ItemProfiles.PrefabName.Equals(itemProfiles.PrefabName)) continue;

            // if items is full
            if (item.Amount == itemProfiles.MaxStack) continue;

            StackItem(ref item, ref currentAmount);
            items[key] = item;
        }

        AddItemIntoNewSpaces(items, itemProfiles, ref currentAmount, ref cntSpace);
        if (currentAmount > 0) return;
        _items = items;
        OnItemChangeInvoke();
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
    public void RemoveItem(PrefabName prefabName, int amount)
    {

    }
    #endregion

    public ItemProfiles GetItemProfiles(PrefabName prefabName) =>
        _itemProfiles.TryGetValue(prefabName, out var value)? value : null;

    public void OnItemChangeInvoke() => OnItemChange?.Invoke();
}

[Serializable]
public struct Item
{
    public string ItemID;
    public ItemProfiles ItemProfiles;
    public int Amount;
}