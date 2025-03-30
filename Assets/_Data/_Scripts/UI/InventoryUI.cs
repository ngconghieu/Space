using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : GameMonoBehaviour
{
    [SerializeField] private ItemUI _btnDefault;
    [SerializeField] private Transform _scrollView;
    [SerializeField] private Transform _holder;
    [SerializeField] private bool _showInventory = true;
    [SerializeField] private List<ItemUI> _btnItemList = new();

    public Transform Holder => _holder;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _btnDefault.SetDefaultBtn();
        ToggleInventory();
        LoadItemSlots();
        InputManager.Instance.HandleInventoryToggle += ToggleInventory;
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadScrollView();
        LoadBtnDefault();
        _holder = _scrollView.Find("Holder");
    }

    private void LoadScrollView()
    {
        if (_scrollView != null) return;
        _scrollView = transform.Find("Scroll View");
        Debug.Log("LoadScrollView", gameObject);
    }

    private void LoadBtnDefault()
    {
        if (_btnDefault != null) return;
        _btnDefault = GetComponentInChildren<ItemUI>();
        _btnItemList.Add(_btnDefault);
        Debug.Log("LoadBtnDefault", gameObject);
    }

    private void LoadItemSlots()
    {
        int inventorySize = InventoryManager.Instance.InventorySize();

        for (int i = _btnItemList.Count; i < inventorySize; i++)
        {
            ItemUI newBtnItem = Instantiate(_btnDefault, _btnDefault.transform.parent);
            newBtnItem.name = $"Item_{i}";
            newBtnItem.SetIndex(i);
            _btnItemList.Add(newBtnItem);
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

    public void HandleItemChange()
    {
        List<Item> items = InventoryManager.Instance.GetItemList();
        foreach (var item in items)
        {
            // update existing btnItem
            ItemUI btnItem = _btnItemList.Find(_item => _item.ItemId.Equals(item.ItemID));
            if (btnItem != null)
            {
                if (item.Amount == 0)
                    btnItem.SetDefaultBtn();
                else
                    btnItem.SetAmount(item.Amount);
            }

            // add new btnItem
            else
            {
                btnItem = _btnItemList.Find(_item => _item.IsEmptyBtn());
                btnItem.SetItemId(item.ItemID);
                btnItem.SetImage(item.ItemProfiles.ItemIcon);
                btnItem.SetAmount(item.Amount);
            }
        }
        InventoryManager.Instance.RemoveEmptyItems();
    }
}
