using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    static public CardScript Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    Image image;
    string person;
    string condition;
    bool selected;
    int position;
    RectTransform startPosition;

    public Image Image { get { return Instance.image; } }
    public string Person { get { return Instance.person; } }
    public string Condition { get { return Instance.condition; } }
    public bool Selected { get { return Instance.selected; } }
    public int Position { get { return Instance.position; } }
    public RectTransform StartPosition { set { Instance.startPosition = value; } get { return Instance.startPosition; } }

    private void Start()
    {
        startPosition = GetComponent<RectTransform>();
    }

}
