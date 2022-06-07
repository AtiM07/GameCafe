using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultGameScript : MonoBehaviour
{
    public static ResultGameScript Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

       if (resultPanel.activeSelf) resultPanel.SetActive(!resultPanel.activeSelf);
    }
    public GameObject resultPanel;
    [SerializeField]
    private GameObject gameElement;
    [SerializeField]
    private TextMeshProUGUI resultTxt;

    private readonly string _resultGameTextPattern = "Счет игры: ", 
        _badTextPattern = "Ты неплохо справляешься, но необходимо подтянуть свои навыки. Будь более общительным!", 
        _normalTextPattern = "Продолжай в том же духе и ты достигнешь высшего уровня волшебного леса!",
        _goodTextPattern = "Ты большой молодец, горжусь! Ты достиг уровня короля волшебного леса!";
    public void ResultGame(int result)
    {
        if (gameElement.activeSelf) gameElement.SetActive(!gameElement.activeSelf);
        //if (!resultPanel.activeSelf) resultPanel.SetActive(!resultPanel.activeSelf);
        resultPanel.SetActive(true);
        var name = SceneManager.GetActiveScene().name;
        if (name == "NovellaScene")
        {
              resultTxt.text = _resultGameTextPattern + result + "\n";
            if (result < 0) resultTxt.text += _badTextPattern;
            else if (result < 3) resultTxt.text += _normalTextPattern;
            else resultTxt.text += _goodTextPattern;

            Interpreter.LastResultNovella = result;

            //проверка наилучшего результата
            if (Interpreter.LastResultNovella > Interpreter.BestResultNovella)
                Interpreter.BestResultNovella = Interpreter.LastResultNovella;

        }
        else
        if (name == "CardGameScene")
        {
            resultTxt.text = _resultGameTextPattern + result;
            resultTxt.text = _resultGameTextPattern + result + "\n";
            if (result < 6) resultTxt.text += "На этот раз ты проиграл. Но не отчаивайся и попробуй еще раз!";
            else if (result < 10) resultTxt.text += _normalTextPattern;
            else resultTxt.text += _goodTextPattern;

            Interpreter.LastResultCardsGame = result;

            //проверка наилучшего результата
            if (Interpreter.LastResultCardsGame > Interpreter.BestResultCardsGame)
                Interpreter.BestResultCardsGame = Interpreter.LastResultCardsGame;
        }

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    //добавить изменение персонажа взависимости от результата
}
