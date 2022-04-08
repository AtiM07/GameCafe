using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public List<GameObject> cards; //cardscript

    public void AddCard(GameObject card)
    {
        cards.Add(card);
    }

    public GameObject cardPref;
    private void Start()
    {
        
    }
}
