using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using TMPro;

public class Interpreter : MonoBehaviour
{
    public static Interpreter Instance;
    public List<SectionDialogue> sections { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public Interpreter()
    {
        sections = new();
    }
    
    [SerializeField]
    private TextMeshProUGUI str;

    public TextAsset xmlScenario;   

    void Start()
    {
        XmlDocument xDoc = new();
        xDoc.LoadXml(xmlScenario.text);    //xDoc.Load("scenario.xml");

        //корневой элемент
        XmlElement xRoot = xDoc.DocumentElement;
        if (xRoot != null)
        {
            StartNovella(xRoot);
            // ShowFullScenario(xRoot);
            DialogueManager.Instance.Init(); 
        }
    }

    //метод вывода полного сценария
     void ShowFullScenario(XmlElement xmlElement)
    {
        XmlNode attr = null;

        if (xmlElement.Attributes != null)
            attr = xmlElement.Attributes.Item(0);

        //вспомогательная переменная, сообщающая есть ли у тэга дочерние тэги
        bool hasChild = (xmlElement.ChildNodes.Count != 1); 

        if (hasChild)
        {
            //если у тэга есть атрибут, то выводим значение атрибута
            if (attr?.Value != null)               
                str.text += attr?.Value + "\n";               
        }
        else
        {            
            if (attr?.Value != null)
            {
                //у фразы просто выводим значение тэга
                if (xmlElement.Name == "phrase")                    
                    str.text += "Фраза: " + xmlElement.InnerText + "; \n";
                //у ответа выводим значение тэга и все атрибуты
                if (xmlElement.Name == "answer")
                {                   
                    str.text += "Ответ: " + xmlElement.InnerText + "; ";
                    foreach (XmlAttribute x in xmlElement.Attributes)
                    {
                        if (x.Name == "nextphrase")
                           
                            str.text += "Следующая фраза: " + x.InnerText + "; ";
                        if (x.Name == "value")
                            
                            str.text += "Стоимость ответа: " + x.InnerText + "; ";
                    }
                }

                str.text += "\n";
            }
            else //если нет детей и атрибутов, то просто выводим значение тэга
                str.text += xmlElement.InnerText + "\n";
        }
        //спускаемся ниже по дереву
        if (hasChild)
            foreach (XmlElement xeNode in xmlElement.ChildNodes) ShowFullScenario(xeNode);

    }

    /// <summary>
    /// Заполняет всю информацию по Xml-файлу в список "sections"
    /// </summary>
    /// <param name="xmlElement">Коренневой элемент xml-файла</param>
    void StartNovella(XmlElement xmlElement)
    {
        XmlNode attr = null;

        if (xmlElement.Attributes != null) 
            attr = xmlElement.Attributes.Item(0);

        //вспомогательная переменная, сообщающая есть ли у тэга дочерние тэги
        bool hasChild = (xmlElement.ChildNodes.Count != 1);

        if (hasChild)
        {
            //если у тэга есть атрибут, то выводим значение атрибута
            if (attr?.Value != null)
            {
                //если название элемента соответствует шаблону, то создаем его экземпляр и записываем его в лист
                if (xmlElement.Name == "section")
                {
                    SectionDialogue section = new();
                    section.SetNumber(int.Parse(attr?.Value));
                    sections.Add(section);
                }
                // у диалога создаем экземпляр и записываем к последнему добавленному элементу секции
                if (xmlElement.Name == "dialogue")
                {
                    Dialogue dialogue = new();
                    dialogue.SetNumber(int.Parse(attr?.Value));
                    sections[^1].Dialogues.Add(dialogue);
                }
            }
            //и спускаемся ниже по дереву
            foreach (XmlElement xeNode in xmlElement.ChildNodes) StartNovella(xeNode);
        }
        else
        {
            if (attr?.Value != null)
            {
                //у фразы создаем экземпляр и записываем к последнему добавленному диалогу
                if (xmlElement.Name == "phrase")
                {
                    Phrase phrase = new();
                    phrase.SetPhrase(xmlElement.InnerText);
                    sections[^1].Dialogues[^1].Phrases.Add(phrase);
                }
                //у ответа создаем экземпляр и записываем к последней добавленной фразе
                if (xmlElement.Name == "answer")
                {
                    Answer answer = new();
                    answer.SetAnswer(xmlElement.InnerText, int.Parse(xmlElement.Attributes["value"].InnerText), int.Parse(xmlElement.Attributes["nextphrase"].InnerText));
                    sections[^1].Dialogues[^1].Phrases[^1].Answers.Add(answer);
                }
            }
            else //если нет детей и атрибутов, то просто проверяем, нужный ли нам это тэг и записываем его в соответствующее значение к секции
            {
                if (xmlElement.Name == "character")
                {
                   sections[^1].SetCharacter(xmlElement.InnerText);
                }
            }
        }
       
    }
   
    public class SectionDialogue
    {
        public int Number { get; private set; }
        public string Character { get; private set; }
        public List<Dialogue> Dialogues { get; private set; }
        public SectionDialogue()
        {
            Dialogues = new List<Dialogue>();
        }
        public void SetNumber(int number)
        {
            Number = number;
        }
        public void SetCharacter(string character)
        {
            Character = character;
        }
        public void SetDialogues(List<Dialogue> dialogues)
        {
            Dialogues = dialogues;
        }
    }
    public class Dialogue
    {        
        public int Number { get; private set; }
        public List<Phrase> Phrases { get; private set; }
        public Dialogue()
        {
            Phrases = new List<Phrase>();
        }
        public void SetNumber(int number)
        {
            Number = number;
        }       
        public void SetPhrases(List<Phrase> phrases)
        {
            Phrases = phrases;
        }
    }
    public class Phrase
    {       
        public string PhraseText { get; private set; }
        public List<Answer> Answers { get; private set; }
        public Phrase()
        {
            Answers = new List<Answer>();
        }
        public void SetPhrase(string prase)
        {
             PhraseText = prase;
        }
        public void SetAnsweres(List<Answer> answers)
        {
            Answers = answers;
        }
    }
    public class Answer
    {       
        public string AnswerText { get; private set; }
        public int Value { get; private set; }
        public int NumNextPhrase { get; private set; }
        public void SetAnswer(string answer, int value, int numNextPhrase)
        {
            AnswerText = answer;
            Value = value;
            NumNextPhrase = numNextPhrase;
        }
    }
    public class Character
    {
       
        public string Name { get; private set; }
        public Image[] Reaction { get; private set; }
    }

}
