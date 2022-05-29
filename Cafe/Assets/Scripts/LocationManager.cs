using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LocationManager : MonoBehaviour
{
    [System.Serializable]
    public class Location
    {
        public Sprite background;
        public Sprite deskCard;
        public Sprite borderCard;
        public Sprite cardDeskBack;
        public Sprite timerBack;
        public Sprite addFood;
        public Sprite addPerson;
        public Sprite checkBack;

        public Sprite resultBack;
        public Sprite btnBack;

    }

    [SerializeField]
    private List<Location> Locations = new ();

    [SerializeField]
    private Image background;
    [SerializeField]
    private Image deskCard;
    [SerializeField]
    private Image borderCard;
    [SerializeField]
    private List<Image> cardDeskBack;
    [SerializeField]
    private Image timerBack;
    [SerializeField]
    private Image addFood;
    [SerializeField]
    private Image addPerson;
    [SerializeField]
    private Image checkBack;

    [SerializeField]
    private Image resultBack;
    [SerializeField]
    private Image exitBack;
    [SerializeField]
    private Image restartBack;

    [SerializeField]
    private TextMeshProUGUI timerTxt;

    [SerializeField]
    private List<GameObject> LocationsNovella = new ();
    private void Start()
    {
        int num = Interpreter.Location - 1;
        if (SceneManager.GetActiveScene().name == "CardGameScene")
        {            
            GameObject[] cell = GameObject.FindGameObjectsWithTag("Cell");
            foreach (GameObject card in cell)
                cardDeskBack.Add(card.GetComponent<Image>());

            ChangeCardGame(num);
        }
        else if (SceneManager.GetActiveScene().name == "NovellaScene")
        {
            LocationsNovella[num].SetActive(true);
        }

    }

    private void ChangeCardGame(int i)
    {
        background.sprite = Locations[i].background;
        deskCard.sprite = Locations[i].deskCard;
        borderCard.sprite = Locations[i].borderCard;

        foreach (Image card in cardDeskBack)
            card.sprite = Locations[i].cardDeskBack;

        timerBack.sprite = Locations[i].timerBack;
        addFood.sprite = Locations[i].addFood;
        addPerson.sprite = Locations[i].addPerson;
        checkBack.sprite = Locations[i].checkBack;

        resultBack.sprite = Locations[i].resultBack;
        exitBack.sprite = Locations[i].btnBack;
        restartBack.sprite = Locations[i].btnBack;

        if (i == 1) timerTxt.color = new Color32(82, 125, 113, 255);
        else timerTxt.color = Color.white;
    }
}
