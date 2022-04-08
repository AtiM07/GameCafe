using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IDropHandler
{
    //MISIING SCRIPT
    [SerializeField] Canvas canvas;
    RectTransform rectTransform;
    public static DragNDrop cardSelected;
    Transform parent;
    Vector2 startPosition;
    public RectTransform rectTransformGrid;
    GameObject[] Cells;

    private void Awake()
    {        
        Cells = GameObject.FindGameObjectsWithTag("Cell");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        cardSelected = this;
        //rectTransform = cardSelected.GetComponent<RectTransform>();
        //if (cardSelected.GetComponent<CardScript>().StartPosition == null)
        //{
        //    cardSelected.GetComponent<CardScript>().StartPosition = GetComponent<RectTransform>();
        //}
        //startPosition = cardSelected.GetComponent<CardScript>().StartPosition.anchoredPosition;
        //parent = transform.parent;

        startPosition = transform.position;
        parent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
      //rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
       // rectTransform.position = eventData.position;
       transform.position = Input.mousePosition;
    }

    bool flag = true;
    public void OnEndDrag(PointerEventData eventData)
    {
        cardSelected = null;
        ////if (rectTransform.anchoredPosition != startPosition)
        ////    rectTransform.anchoredPosition = startPosition;
        //foreach (GameObject cell in Cells)
        //    if (GetDistance(rectTransform, cell.GetComponent<RectTransform>()) < 20)
        //    {
        //        rectTransform.anchoredPosition = cell.GetComponent<RectTransform>().anchoredPosition;
        //        flag = false;
        //        Debug.Log("move");
        //    }

        //if (flag) rectTransform.anchoredPosition = startPosition;
        //else flag = true;
        if (parent == transform.parent)
            transform.position = startPosition;
    }
    float GetDistance(RectTransform card, RectTransform cell)
    {
        return Mathf.Sqrt(Mathf.Pow(card.transform.position.x - cell.transform.position.x, 2) +
                                     Mathf.Pow(card.transform.position.y - cell.transform.position.y, 2) +
                                     Mathf.Pow(card.transform.position.z - cell.transform.position.z, 2));
    }
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (cardSelected != null)
            cardSelected.transform.SetParent(transform);
    }
}
