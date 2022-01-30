using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using TMPro;

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
    GameObject[] answer;
    int numSection, numDialogue;
    Interpreter.Answer[] answers;
    void Start()
    {
        phraseCharText = GameObject.Find("phraseCharText").GetComponent<TextMeshProUGUI>();
        nameCharText = GameObject.Find("nameCharText").GetComponent<TextMeshProUGUI>();
        numSection = 0;
        numDialogue = 0;
    }
    public void Init()
    {
        numSection = Random.Range(0, Interpreter.Instance.sections.Count-1);
        numDialogue = Random.Range(0, Interpreter.Instance.sections[numSection].Dialogues.Count-1);
        answer = GameObject.FindGameObjectsWithTag("Answer");
        NovellaGenerate();
    }
    public void NovellaGenerate()
    {
        Interpreter.SectionDialogue section = Interpreter.Instance.sections[numSection];

        nameCharText.text = section.Character;          

        DialogueGenerate(section.Dialogues[numDialogue]);
        
    }

    public void DialogueGenerate(Interpreter.Dialogue dialogue)
    {
        phraseCharText.text = dialogue.Phrases[0].PhraseText;

        answers = new Interpreter.Answer[dialogue.Phrases[0].Answers.Count];
        int a = 0;
        foreach (Interpreter.Answer ans in dialogue.Phrases[0].Answers)
        {
            answers[a] = ans;
            answer[a].GetComponent<TextMeshProUGUI>().text += answers[a].AnswerText + "\n";
            a++;
        }
    }
}