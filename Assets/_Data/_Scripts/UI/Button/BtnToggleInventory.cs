using UnityEngine;

public class BtnToggleInventory : BtnAbstract
{
    [SerializeField] private InventoryUI _inventoryUI;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadInventoryUI();
    }

    private void LoadInventoryUI()
    {
        if (_inventoryUI != null) return;
        _inventoryUI = transform.parent.GetComponentInParent<InventoryUI>();
        Debug.Log("LoadInventoryUI", gameObject);
    }

    protected override void OnClick() =>
        _inventoryUI.ToggleInventory();


}
