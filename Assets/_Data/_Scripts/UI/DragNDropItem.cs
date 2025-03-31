using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag" + eventData);
        foreach (var item in InventoryUI.Instance.GetItemSlotList.Values)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(item.GetComponentInChildren<RectTransform>(), eventData.position))
            {
                Debug.Log("Item: " + item.name);
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }
}
