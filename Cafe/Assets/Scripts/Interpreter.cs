using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using TMPro;
using DMData;
public class Interpreter : Singleton<Interpreter>
{
    [SerializeField]
    private DataManager data;
    private static DataManager IsData
    {
        get { return Instance.data; }
    }
    void Start()
    {
        //new DataManager().Init();
        //data = ScriptableObject.CreateInstance<DataManager>();
        //data.Init();
        
        DialogueManager.Instance.Init();
        //img.sprite = data.Sections[0].character.sprite[0];
    }
    private void Awake()
    {
        Load();
    }

    public void Save()
    {
        JsonController.InternalSaveJSON(DataPath, data);
    }

    private const string DataPath = "DMData.bin";
    public void Load()
    {
        data = Instantiate(data);
        //data.Init();
        data = JsonController.PopulateJSON(DataPath, data);
        Save();
    }
    public List<DataManager.Section> GetSection()
    {
        return  IsData.Sections;
    }
    public DataManager.Section GetSection(int numSection)
    {
        return IsData.Sections[numSection];
    }
    public List<DataManager.Dialogue> GetDialogue(int numSection)
    {
        return IsData.Sections[numSection].dialogues;
    }
    public DataManager.Dialogue GetDialogue(int numSection, int numDialogue)
    {
        return IsData.Sections[numSection].dialogues[numDialogue];
    }

    public List<DataManager.Phrase> GetPhrase(int numSection, int numDialogue)
    {
        return IsData.Sections[numSection].dialogues[numDialogue].phrases;
    }
    public DataManager.Phrase GetPhrase(int numSection, int numDialogue, int numPhrase)
    {
        return IsData.Sections[numSection].dialogues[numDialogue].phrases[numPhrase];
    }

    public List<DataManager.Answer> GetAnswer(int numSection, int numDialogue, int numPhrase)
    {
        return IsData.Sections[numSection].dialogues[numDialogue].phrases[numPhrase].answers;
    }
    public DataManager.Answer GetAnswer(int numSection, int numDialogue, int numPhrase, int numAnswer)
    {
        return IsData.Sections[numSection].dialogues[numDialogue].phrases[numPhrase].answers[numAnswer];
    }

}
