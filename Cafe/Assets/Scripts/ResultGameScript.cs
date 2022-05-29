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

    private readonly string _resultGameTextPattern = "—чет игры: ";
    public void ResultGame(int result)
    {
        if (gameElement.activeSelf) gameElement.SetActive(!gameElement.activeSelf);
        if (!resultPanel.activeSelf) resultPanel.SetActive(!resultPanel.activeSelf);
        var name = SceneManager.GetActiveScene().name;
        if (name == "NovellaScene")
        {
              resultTxt.text = _resultGameTextPattern + result;
            Interpreter.LastResultNovella = result;

            //проверка наилучшего результата
            if (Interpreter.LastResultNovella > Interpreter.BestResultNovella)
                Interpreter.BestResultNovella = Interpreter.LastResultNovella;

        }
        else
        if (name == "CardGameScene")
        {
            resultTxt.text = _resultGameTextPattern + result;
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
