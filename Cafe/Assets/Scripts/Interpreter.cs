using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using TMPro;


public class Interpreter : MonoBehaviour
{
    public static Interpreter Instance;
    void Awake()
    {
        if (Instance == null)
            Instance = this;

    }


    [SerializeField]
    private Image img;

    [SerializeField]
    private DataManager data;
    private static DataManager isData
    {
        get { return Instance.data; }
    }
    void Start()
    {
        //new DataManager().Init();
        data = ScriptableObject.CreateInstance<DataManager>();
        data.Init();
        DialogueManager.Instance.Init();
        //img.sprite = data.Sections[0].character.sprite[0];
    }

    public List<DataManager.Section> GetSection()
    {
        return  isData.Sections;
    }
    public DataManager.Section GetSection(int numSection)
    {
        return isData.Sections[numSection];
    }
    public List<DataManager.Dialogue> GetDialogue(int numSection)
    {
        return isData.Sections[numSection].dialogues;
    }
    public DataManager.Dialogue GetDialogue(int numSection, int numDialogue)
    {
        return isData.Sections[numSection].dialogues[numDialogue];
    }

    public List<DataManager.Phrase> GetPhrase(int numSection, int numDialogue)
    {
        return isData.Sections[numSection].dialogues[numDialogue].phrases;
    }
    public DataManager.Phrase GetPhrase(int numSection, int numDialogue, int numPhrase)
    {
        return isData.Sections[numSection].dialogues[numDialogue].phrases[numPhrase];
    }

    public List<DataManager.Answer> GetAnswer(int numSection, int numDialogue, int numPhrase)
    {
        return isData.Sections[numSection].dialogues[numDialogue].phrases[numPhrase].answers;
    }
    public DataManager.Answer GetAnswer(int numSection, int numDialogue, int numPhrase, int numAnswer)
    {
        return isData.Sections[numSection].dialogues[numDialogue].phrases[numPhrase].answers[numAnswer];
    }

}
