using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    [SerializeField]
    private TextMeshProUGUI phraseTxt;

    [SerializeField]
    private TextMeshProUGUI nameTxt;

    [SerializeField]
    private GameObject resultPanel;

    private int _resultPoint;

    [SerializeField]
    private Image characterImg;
    public string _character;

    /// <summary>
    /// Хранение кнопок с ответами
    /// </summary>/ 
    [SerializeField]
    private GameObject[] answer;

    private int _numSection =0, _numDialogue=0, _numPhrase=0;

    /// <summary>
    /// Запуск. Выбор рандомной секции новеллы с дальнейшим отображением диалога
    /// </summary>
    public void Init()
    {
        _numSection = Random.Range(0, Interpreter.Instance.GetSection().Count);
        _numDialogue = Random.Range(0, Interpreter.Instance.GetDialogue(_numSection).Count);

        nameTxt.text = Interpreter.Instance.GetSection(_numSection).character.name;
        _character = Interpreter.Instance.GetSection(_numSection).character.character;
        SetImageCharacter();      
        _numPhrase = 0; _resultPoint = 0;
        DialogueGenerate();
    }
    public Sprite[] sprites;
    private void SetImageCharacter()
    {
        foreach (Sprite sprite in sprites)
            if (sprite.name == _character)
                characterImg.sprite = sprite;
    }
    private void Start()
    {       
        Init();
    }
    
    private void DialogueGenerate()// Отображение диалога (фразы персонажа и вариантов ответа пользователя)
    {        
        phraseTxt.text = Interpreter.Instance.GetPhrase(_numSection, _numDialogue, _numPhrase).phrase;

        for (int i = 0; i < Interpreter.Instance.GetAnswer(_numSection, _numDialogue, _numPhrase).Count; i++)
        {
            answer[i].GetComponentInChildren<TextMeshProUGUI>().text = Interpreter.Instance.GetAnswer(_numSection, _numDialogue, _numPhrase, i).answer;
        }
    }

    public void CheckAnswer()// Проверка выбранного пользователем варианта ответа и соответствующие ответу дальнейшие действия
    {
        TextMeshProUGUI currAnswer = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>();

        for (int i = 0; i < answer.Length; i++)
        {
            DMData.DataManager.Answer answer = Interpreter.Instance.GetAnswer(_numSection, _numDialogue, _numPhrase, i);
            if (currAnswer.text == answer.answer)
            {

                SumResult(answer.value);
                if (answer.nextphrase != 0)
                {
                    AnswerResult(answer.nextphrase - 1);
                    return;
                }
                else //вывод результата игры
                {
                    if(!resultPanel.activeSelf) resultPanel.SetActive(!resultPanel.activeSelf);

                    ResultGameScript.Instance.resultPanel = resultPanel;
                    ResultGameScript.Instance.ResultGame(_resultPoint);
                    return;
                }

            }
        }
    }

    private void SumResult(int value)// Счетчик баллов после выбора ответа на фразу посетителя кафе
    {
        _resultPoint += value;
    }
    private void AnswerResult(int numNextPhrases)// Реакция на правильный ответ пользователя
    {
        StartCoroutine(AnimClearBtnAnswer());
        _numPhrase = numNextPhrases;
        DialogueGenerate();
        //смена иконки персонажа + результат
    }
    private IEnumerator AnimClearBtnAnswer()// Обнуление текста ответов
    {
        foreach(GameObject a in answer)
        {           
            a.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        yield return null;
    }

}