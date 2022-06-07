using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static DragCard selectedCard;
    private Vector3 startPosition;
    private Transform startParent;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        selectedCard = this;
        startPosition = transform.position;
        startParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
      transform.position = (Vector2)Camera.main.ScreenToWorldPoint( Input.mousePosition);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        selectedCard = null;
        canvasGroup.blocksRaycasts = true;
        if (startParent == transform.parent)
            transform.position = startPosition;
        else transform.localPosition = Vector3.zero;
    }
}


