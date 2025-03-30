using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag" + eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }
}
