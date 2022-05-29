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
        {
            card.transform.SetParent(transform);
            card.GetComponent<Card>().Position = int.Parse(transform.gameObject.name);
            if (card.GetComponent<Card>().OnDeck)
            {
                GameManager.Instance.numCellDesk -= 1;
                card.GetComponent<Card>().OnDeck = false;
            }
        }
    }
}
