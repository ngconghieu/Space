using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryUI : GameMonoBehaviour
{
    [SerializeField] private BtnItem _btnDefault;
    [SerializeField] private bool _showInventory = true;
    private Dictionary<string, BtnItem> _btnItems = new();

    private void Start()
    {
        _btnDefault.gameObject.SetActive(false);
        ToggleInventory();
    }

    private void Update()
    {
        if (InputManager.Instance.OpenInventory)
            ToggleInventory();
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnDefault();
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
        gameObject.SetActive(_showInventory);
    }

    public void OnItemChange()
    {
        
    }
}
