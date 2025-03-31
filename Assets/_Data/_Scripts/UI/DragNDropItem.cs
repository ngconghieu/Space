using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private ItemSlot _itemSlot;

    public void OnBeginDrag(PointerEventData eventData)
    {
        foreach (var item in InventoryUI.Instance.GetItemSlotList.Values)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(item.GetComponentInChildren<RectTransform>(), eventData.position))
            {
                if (item.ItemUI.CheckEmptyItem()) return; // if the item slot is empty, return
                _itemSlot = item;
                _itemSlot.ItemUI.transform.SetParent(InventoryUI.Instance.Holder);
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (_itemSlot == null) return;
        _itemSlot.ItemUI.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_itemSlot == null) return;
        if (!HandleDroppedAtASlot(eventData))
        {
            //HandleDroppedAtNotASlot(eventData);
        }


    }

    private bool HandleDroppedAtASlot(PointerEventData eventData)
    {
        foreach (var item in InventoryUI.Instance.GetItemSlotList.Values)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(item.GetComponentInChildren<RectTransform>(), eventData.position))
            {
                Debug.Log(item.ItemUI, item.ItemUI.gameObject);
                item.ItemUI.transform.SetParent(_itemSlot.transform);
                item.ItemUI.transform.position = Vector2.zero;
                return true;
            }
        }
        return false;
    }

    private void HandleDroppedAtNotASlot(PointerEventData eventData)
    {
        foreach (var item in InventoryUI.Instance.GetItemSlotList.Values)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(item.GetComponentInChildren<RectTransform>(), eventData.position))
            {
                _itemSlot.ItemUI.transform.position = item.ItemUI.transform.position;
                item.SetItemUI(_itemSlot.ItemUI);
                _itemSlot.ItemUI.SetDefault();
                return;
            }
        }
    }
}
