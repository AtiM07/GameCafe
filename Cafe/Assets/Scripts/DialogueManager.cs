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

    TextMeshProUGUI phraseCharText;
    TextMeshProUGUI nameCharText;

    /// <summary>
    /// Хранение кнопок с ответами
    /// </summary>
    GameObject[] answer;

    int numSection, numDialogue;

    /// <summary>
    /// Хранение текущего списка ответов
    /// </summary>
    Interpreter.Answer[] answers;

    void Start()
    {
        phraseCharText = GameObject.Find("phraseCharText").GetComponent<TextMeshProUGUI>();
        nameCharText = GameObject.Find("nameCharText").GetComponent<TextMeshProUGUI>();
        numSection = 0;
        numDialogue = 0;
    }

    /// <summary>
    /// Запуск. Выбор рандомной секции новеллы с дальнейшим отображением диалога
    /// </summary>
    public void Init()
    {
        numSection = Random.Range(0, Interpreter.Instance.sections.Count-1);
        numDialogue = Random.Range(0, Interpreter.Instance.sections[numSection].Dialogues.Count-1);
        answer = GameObject.FindGameObjectsWithTag("Answer");
        NovellaGenerate();
    }

    /// <summary>
    /// Вспомогательный метод для начальной генерации новеллы 
    /// </summary>
    public void NovellaGenerate()
    {
        Interpreter.SectionDialogue section = Interpreter.Instance.sections[numSection];

        nameCharText.text = section.Character;          

        DialogueGenerate(section.Dialogues[numDialogue].Phrases[0]);
        
    }

    /// <summary>
    /// Отображение диалога (фразы персонажа и вариантов ответа пользователя)
    /// </summary>
    /// <param name="phrases"> Номер диалога, который неоходимо отобразить </param>
    public void DialogueGenerate(Interpreter.Phrase phrases)
    {
        phraseCharText.text = phrases.PhraseText;

        answers = new Interpreter.Answer[phrases.Answers.Count];
        int a = 0;
        foreach (Interpreter.Answer ans in phrases.Answers)
        {
            answers[a] = ans;
            answer[a].GetComponentInChildren<TextMeshProUGUI>().text = answers[a].AnswerText; //answer[a].GetComponent<TextMeshProUGUI>().text += answers[a].AnswerText + "\n";
            a++;
        }
    }

    /// <summary>
    /// Проверка выбранного пользователем варианта ответа и соответствующие ответу дальнейшие действия
    /// </summary>
    public void CheckAnswer()
    {
        TextMeshProUGUI currAnswer = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>();        
        
        for (int i=0; i< answers.Length;i++)
        {
            if (currAnswer.text == answers[i].AnswerText)
            {                
                if (answers[i].NumNextPhrase != 0)
                {
                    AnswerResult(answers[i].NumNextPhrase-1);
                    return;
                }
                else //вывод результата игры
                {
                    ResultGame();
                    return;
                }
               
            }
        }
    }

    void AnswerResult(int numPhrases)
    {
        StartCoroutine(animBtnAnswer());
        DialogueGenerate(Interpreter.Instance.sections[numSection].Dialogues[numDialogue].Phrases[numPhrases]);
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

    void ResultGame()
    {
        phraseCharText.text = "Игра закончена";
        foreach (GameObject a in answer)
            a.GetComponentInChildren<TextMeshProUGUI>().text = "";

        //вывод результата прохождения новеллы
    }
}