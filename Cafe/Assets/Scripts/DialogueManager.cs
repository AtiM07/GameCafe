using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public TextMeshProUGUI phraseCharText ;
    public TextMeshProUGUI nameCharText;
    public GameObject resultPanel;
    public int resultPoint;

    /// <summary>
    /// Хранение кнопок с ответами
    /// </summary>
    GameObject[] answer, background;
    ///
    int numSection =0, numDialogue=0, numPhrase=0;

    /// <summary>
    /// Запуск. Выбор рандомной секции новеллы с дальнейшим отображением диалога
    /// </summary>
    public void Init()
    {
        phraseCharText = GameObject.Find("phraseCharText").GetComponent<TextMeshProUGUI>();
        nameCharText = GameObject.Find("nameCharText").GetComponent<TextMeshProUGUI>();
        answer = GameObject.FindGameObjectsWithTag("Answer");
        GameObject.Find("background").GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("Novella/background");

        numSection = Random.Range(0, Interpreter.Instance.GetSection().Count);
        numDialogue = Random.Range(0, Interpreter.Instance.GetDialogue(numSection).Count);

        nameCharText.text = Interpreter.Instance.GetSection(numSection).character.name;
        numPhrase = 0; resultPoint = 0;
        DialogueGenerate();
    }

    /// <summary>
    /// Отображение диалога (фразы персонажа и вариантов ответа пользователя)
    /// </summary>
    public void DialogueGenerate()
    {        
        phraseCharText.text = Interpreter.Instance.GetPhrase(numSection, numDialogue, numPhrase).phrase;

        for (int i = 0; i < Interpreter.Instance.GetAnswer(numSection, numDialogue, numPhrase).Count; i++)
        {
            answer[i].GetComponentInChildren<TextMeshProUGUI>().text = Interpreter.Instance.GetAnswer(numSection, numDialogue, numPhrase, i).answer; //answer[a].GetComponent<TextMeshProUGUI>().text += answers[a].AnswerText + "\n";
        }
    }

    /// <summary>
    /// Проверка выбранного пользователем варианта ответа и соответствующие ответу дальнейшие действия
    /// </summary>
    public void CheckAnswer()
    {
        TextMeshProUGUI currAnswer = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>();

        for (int i = 0; i < answer.Length; i++)
        {
            DataManager.Answer answer = Interpreter.Instance.GetAnswer(numSection, numDialogue, numPhrase, i);
            if (currAnswer.text == answer.answer)
            {

                Sum(answer.value);
                if (answer.nextphrase != 0)
                {
                    AnswerResult(answer.nextphrase - 1);
                    return;
                }
                else //вывод результата игры
                {
                    UIManager.Instance.ResultPanel(resultPanel);
                    return;
                }

            }
        }
    }
    private void Sum(int value)
    {
        resultPoint += value;
    }

    /// <summary>
    /// Реакция на правильный ответ пользователя
    /// </summary>
    /// <param name="numPhrases">Следующая фраза</param>
    void AnswerResult(int numPhrases)
    {
        StartCoroutine(animBtnAnswer());
        numPhrase = numPhrases;
        DialogueGenerate();
        //смена иконки персонажа + результат
    }

    IEnumerator animBtnAnswer()
    {
        foreach(GameObject a in answer)
        {           
            a.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        yield return null;
    }

}