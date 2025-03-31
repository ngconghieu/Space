using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private ItemSlot _itemSlot;

    public void OnBeginDrag(PointerEventData eventData)
    {
        foreach (var item in ServiceLocator.Get<InventoryUI>().GetItemSlotList)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(item.GetComponentInChildren<RectTransform>(), eventData.position))
            {
                if (item.ItemUI.CheckEmptyItem()) return; // if the item slot is empty, return
                _itemSlot = item;
                _itemSlot.ItemUI.transform.SetParent(ServiceLocator.Get<InventoryUI>().Holder);
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
            HandleDroppedAtNotASlot(eventData);
        }


    }

    private bool HandleDroppedAtASlot(PointerEventData eventData)
    {
        foreach (var item in ServiceLocator.Get<InventoryUI>().GetItemSlotList)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(item.GetComponentInChildren<RectTransform>(), eventData.position))
            {
                Debug.Log(item.ItemUI, item.ItemUI.gameObject);
                ItemUI transitionItemUI = item.ItemUI;
                SetItemUIToPreviousSlot(item);

                SetDraggedItemToNewSlot(item, transitionItemUI);
                _itemSlot = null;
                return true;
            }
        }
        return false;
    }

    private void HandleDroppedAtNotASlot(PointerEventData eventData)
    {
        _itemSlot.ItemUI.transform.SetParent(_itemSlot.transform);
        _itemSlot.ItemUI.SetRectTransform(Vector2.zero);
    }

    private void SetItemUIToPreviousSlot(ItemSlot item)
    {
        item.ItemUI.transform.SetParent(_itemSlot.transform);
        item.ItemUI.SetRectTransform(Vector2.zero);
        item.SetItemUI(_itemSlot.ItemUI);
    }

    private void SetDraggedItemToNewSlot(ItemSlot item, ItemUI transitionItemUI)
    {
        _itemSlot.ItemUI.transform.SetParent(item.transform);
        _itemSlot.ItemUI.SetRectTransform(Vector2.zero);
        _itemSlot.SetItemUI(transitionItemUI);
    }
}
