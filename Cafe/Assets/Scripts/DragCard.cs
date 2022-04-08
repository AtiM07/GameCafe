using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static DragCard selectedCard;
    Vector3 startPosition;
    Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        selectedCard = this;
        startPosition = transform.position;
        startParent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
      // transform.position = Input.mousePosition;
      transform.localPosition += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (startParent == transform.parent)
            transform.position = startPosition;
        else transform.localPosition = Vector3.zero;
        selectedCard = null;
    }
}
