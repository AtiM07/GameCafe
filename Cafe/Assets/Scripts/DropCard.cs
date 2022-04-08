using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropCard : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var card = DragCard.selectedCard;
        if (card != null && transform.childCount == 0)
            card.transform.SetParent(transform);
    }
}
