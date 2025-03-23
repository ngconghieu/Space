using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private int _maxInventorySize = 10;
    [SerializeField] private AssetLabelReference _itemProfilesLabel;
    [SerializeField] private List<ItemProfiles> _itemProfiles = new();
    [SerializeField] private List<Item> _items = new();

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
            _itemProfiles = new List<ItemProfiles>(handle.Result);
        };
    }
    #endregion

    public void AddItem(ItemName itemName, int amount)
    {
        if (amount <= 0) return;
        ItemProfiles itemProfiles = GetItemProfiles(itemName);
        int currentAmount = amount; // backup amount

        // check if there is enough space in inventory
        if (!canAddItem(itemProfiles, currentAmount))
        {
            Debug.Log("Not enough space in inventory");
            return;
        }

        // stack item
        StackItem(itemProfiles, ref currentAmount); 

        // if item is not valid in _items
        AddNewItem(itemProfiles, ref currentAmount);

    }

    private bool canAddItem(ItemProfiles itemProfiles, int currentAmount)
    {
        int stackableSlot = 0;
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].ItemProfiles.Equals(itemProfiles))
            {
                stackableSlot += itemProfiles.MaxStack - _items[i].Amount;
            }
        }return false;
    }

    private void StackItem(ItemProfiles itemProfiles, ref int currentAmount)
    {
        
    }

    private void AddNewItem(ItemProfiles itemProfiles, ref int currentAmount)
    {
        while (currentAmount > 0)
        {
            int amount;
            if (currentAmount > itemProfiles.MaxStack)
            {
                amount = itemProfiles.MaxStack;
                currentAmount -= itemProfiles.MaxStack;
            }
            else
            {
                amount = currentAmount;
                currentAmount = 0;
            }
            _items.Add(new()
            {
                ItemID = Guid.NewGuid().ToString(),
                ItemProfiles = itemProfiles,
                Amount = amount
            });
        }
    }

    private ItemProfiles GetItemProfiles(ItemName itemName)
    {
        return _itemProfiles.Find(item => item.ItemName == itemName.ToString());
    }
}

[Serializable]
public struct Item
{
    public string ItemID;
    public ItemProfiles ItemProfiles;
    public int Amount;
}