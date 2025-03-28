using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : GameMonoBehaviour
{
    [SerializeField] private Transform _scrollView;
    [SerializeField] private BtnItem _btnDefault;
    [SerializeField] private bool _showInventory = true;
    private readonly Dictionary<string, BtnItem> _btnItems = new();

    private void Start()
    {
        _btnDefault.gameObject.SetActive(false);
        ToggleInventory();
        InventoryManager.Instance.OnItemChange += OnItemChange;
        InputManager.Instance.OnInventoryToggle += ToggleInventory;
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadScrollView();
        LoadBtnDefault();
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
        _btnDefault = GetComponentInChildren<BtnItem>();
        Debug.Log("LoadBtnDefault", gameObject);
    }
    #endregion

    public void ToggleInventory()
    {
        _showInventory = !_showInventory;
        if (_showInventory) OnItemChange();
        _scrollView.gameObject.SetActive(_showInventory);
    }

    public void OnItemChange()
    {
        List<Item> items = new(InventoryManager.Instance.GetItemList());
        foreach (var item in items)
        {
            // update existing btnItem
            if (_btnItems.ContainsKey(item.ItemID))
            {
                if (item.Amount == 0)
                {
                    GameObject.Destroy(_btnItems[item.ItemID].gameObject);
                    _btnItems.Remove(item.ItemID);
                    continue;
                }
                _btnItems[item.ItemID].SetAmount(item.Amount);
                continue;
            }

            // add new btnItem
            else
            {
                BtnItem newBtnItem = Instantiate(_btnDefault, _btnDefault.transform.parent);
                newBtnItem.SetAmount(item.Amount);
                newBtnItem.SetImage(item.ItemProfiles.ItemIcon);
                newBtnItem.SetItemId(item.ItemID);
                newBtnItem.gameObject.SetActive(true);
                _btnItems.Add(item.ItemID, newBtnItem);
            }
        }
    }
}
