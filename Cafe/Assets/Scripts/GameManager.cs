using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    [SerializeField]
    private List<Card> cards;
    [SerializeField]
    private GameObject cardPref;
    [SerializeField]
    private Transform cardParent;

    public int numCellDesk = 0; //карт может быть всего 8 на доске

    [SerializeField]
    private float TimeGame = 10;
    private float _time;

    [SerializeField]
    private TextMeshProUGUI timerTxt;

    [SerializeField]
    private GameObject resultPanel;

    private void Start()
    {
        CreateCard(6);
        if (Interpreter.LevelCardsGame == Card.Levels.Normal.ToString())
            CreateLvlCard(Card.Levels.Normal);

        if (Interpreter.LevelCardsGame == Card.Levels.Hard.ToString())
        {
            CreateLvlCard(Card.Levels.Normal);
            CreateLvlCard(Card.Levels.Hard);
        }

        _time = TimeGame * 60;
        StartCoroutine(Timer());
    }

    /// <summary>
    /// Создает определенное количество карточек для игры для удобства начала
    /// </summary>
    /// <param name="num">6</param>
    private void CreateCard(int num)
    {
        for (int i = 0; i < num; i++)
        {
            if (i < num / 2) CreateCard(true);
            else CreateCard(false);
        }
    }

    /// <summary>
    /// Создает карточку
    /// </summary>
    /// <param name="isPersonCard">передавать true, если хотите создать карту персонажа</param>
    private void CreateCard(bool isPersonCard)
    {
        if (cards.Count < 16)
        {
            var card = Instantiate(cardPref);
            var cardi = card.GetComponent<Card>();
            if (isPersonCard) CardsManager.Instance.SetCardPerson(cardi);
            else CardsManager.Instance.SetCardFood(cardi);
            cardi.transform.SetParent(cardParent);
            cardi.transform.localScale = Vector3.one;

            cards.Add(cardi);
            numCellDesk++;
        }
    }
    /// <summary>
    /// Создает карточку повышенного уровня сложности
    /// </summary>
    /// <param name="level"></param>
    private void CreateLvlCard(Card.Levels level)
    {
        if (cards.Count < 16)
        {
            var card = Instantiate(cardPref);
            var cardi = card.GetComponent<Card>();
            CardsManager.Instance.SetRandomLevelCard(cardi, level);
            cardi.transform.SetParent(cardParent);
            cardi.transform.localScale = Vector3.one;
            cards.Add(cardi);
            numCellDesk++;
        }
    }

    #region TIMER
    private IEnumerator Timer()
    {
        while (_time > 0)
        {
            _time -= Time.deltaTime;
            UpdateTimer();
            yield return null;
        }
    }

    private void UpdateTimer()
    {
        if (_time < 0) _time = 0;
        float minutes = Mathf.FloorToInt(_time / 60);
        float seconds = Mathf.FloorToInt(_time % 60);
        timerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);

        if (_time == 0) CheckField();
    }

    #endregion

    //проверка результата игры
    public void CheckField()
    {
        int result = 0;
        foreach (Card card in cards)
        {
            if (card.Level != Card.Levels.Easy) { 
            result += Check(card);
            }
                 
            else

            //сверху, слева, снизу, справа
            if (CheckPosition(card, card.Position - 4) || CheckPosition(card, card.Position - 1) ||
                 CheckPosition(card, card.Position + 4) || CheckPosition(card, card.Position + 1))
                result += 1;
            else result -= 1;

        }


        ResultGameScript.Instance.resultPanel = resultPanel;
        ResultGameScript.Instance.ResultGame(result);
    }

    private int Check(Card card)
    {
        int num = 0; // для среднего уровня = 2, для сложного - =3

        if (CheckPosition(card, card.Position - 4)) num++;
        if (CheckPosition(card, card.Position - 1)) num++;
        if (CheckPosition(card, card.Position + 4)) num++;
        if (CheckPosition(card, card.Position + 1)) num++;

        if (card.Level == Card.Levels.Normal && num >= 2) num = 2;
        if (card.Level == Card.Levels.Hard && num >= 3) num = 3;

        if (num != 2 && num != 3) num = -1;

        return num;
    }

    /// <summary>
    /// Проверка карты находящейся на определенном расстоянии от заданной
    /// </summary>
    /// <param name="card">объект-карта</param>
    /// <param name="numCell">позиция карты для проверки</param>
    /// <returns></returns>
    private bool CheckPosition(Card card, int numCell)
    {
        if (numCell > 0 && numCell < 16)
            if (CardbyPosition(numCell) != null)
                if (card.Condition == CardbyPosition(numCell).Object) return true;
        return false;
    }

    ////проверка, все ли карты находятся на поле
    //bool CheckCardsOnField()
    //{
    //    foreach (Card card in cards)
    //        if (card.Position == 0)
    //            return false;
    //    return true;
    //}

    //возвращение карты на определенной позиции
    private Card CardbyPosition(int pos)
    {
        foreach (Card card in cards)
            if (card.Position == pos) return card;
        return null;
    }

    public void AddCardPerson()
    {
        if (numCellDesk < 8)
        {
            CreateCard(true);
        }
    }
    public void AddCardFood()
    {
        if (numCellDesk < 8)
        {
            CreateCard(false);
        }
    }
}
