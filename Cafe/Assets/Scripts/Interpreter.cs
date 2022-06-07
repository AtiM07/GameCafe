using System.Collections.Generic;
using UnityEngine;
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
    public static int CheckData
    {
        get { return IsData.Sections.Count;  }
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
        data = JsonController.PopulateJSON(DataPath, data);
        Save();
    }
    
    public static bool Volume
    {
        set { IsData.MainM.volume = value; Instance.Save(); }
        get { return IsData.MainM.volume; }
    }
    public static bool Sound
    {
        set { IsData.MainM.sound = value; Instance.Save(); }
        get { return IsData.MainM.sound; }
    }
    public static int Location
    {
        set { IsData.MainM.location = value; Instance.Save(); }
        get { return IsData.MainM.location; }
    }
    public static string LevelCardsGame
    {
        set { IsData.MainM.lvlCardsGame = value; Instance.Save(); }
        get { return IsData.MainM.lvlCardsGame; }
    }
    public static int LastResultCardsGame
    {
        set { IsData.MainM.lastResultCardsGame = value; Instance.Save(); }
        get { return IsData.MainM.lastResultCardsGame; }
    }
    public static int LastResultNovella
    {
        set { IsData.MainM.lastResultNovella = value; Instance.Save(); }
        get { return IsData.MainM.lastResultNovella; }
    }
    public static int BestResultCardsGame
    {
        set { IsData.MainM.bestResultCardsGame = value; Instance.Save(); }
        get { return IsData.MainM.bestResultCardsGame; }
    }
    public static int BestResultNovella
    {
        set { IsData.MainM.bestResultNovella = value; Instance.Save(); }
        get { return IsData.MainM.bestResultNovella; }
    }

    public List<DataManager.Section> GetSection()
    {
        return IsData.Sections;
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
