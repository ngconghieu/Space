using UnityEngine;

public class ItemSlot : GameMonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField] private ItemUI _itemUI;

    public ItemUI ItemUI => _itemUI;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItemUI();
    }

    private void LoadItemUI()
    {
        if (_itemUI != null) return;
        _itemUI = GetComponentInChildren<ItemUI>();
        _itemUI.SetDefault();
        Debug.Log("LoadItemUI", gameObject);
    }
    #endregion

    public void SetIndex(int index) =>
        _index = index;

    public ItemUI SetItemUI(ItemUI itemUI) =>
        _itemUI = itemUI;

}
