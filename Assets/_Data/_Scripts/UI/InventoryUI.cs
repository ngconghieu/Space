using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryUI : GameMonoBehaviour
{
    [SerializeField] private Transform _scrollView;
    [SerializeField] private BtnItem _btnDefault;
    [SerializeField] private bool _showInventory = true;
    private Dictionary<string, BtnItem> _btnItems = new();

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
        if(_btnDefault != null) return;
        _btnDefault = GetComponentInChildren<BtnItem>();
        Debug.Log("LoadBtnDefault", gameObject);
    }
    #endregion

    public void ToggleInventory()
    {
        _showInventory = !_showInventory;
        Debug.Log(_showInventory);
        _scrollView.gameObject.SetActive(_showInventory);
    }

    public void OnItemChange()
    {
        Debug.Log("OnItemChange");
    }
}
