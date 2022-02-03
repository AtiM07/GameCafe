using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.IO;



[CreateAssetMenu(fileName = "Data", menuName = "Data")]
public class DataManager : ScriptableObject
{
	[SerializeField]
	private List<Section> sections = new();

	/// <summary>
	/// Все секции, которые находились в xml-файле сценария
	/// </summary>
	public List<Section> Sections { get { return sections; } }

	/// <summary>
	/// Загрузка сценария игры
	/// </summary>
	public void Init()
	{
		XmlDocument xDoc = new();
		TextAsset textAsset = (TextAsset)Resources.Load("Novella/scenario");
		xDoc.LoadXml(textAsset.text);

		XmlElement xRoot = xDoc.DocumentElement;
		if (xRoot != null)
			Set(xRoot);
	}
	/// <summary>
	/// Заполнение данными из xml-документа
	/// </summary>
	/// <param name="xmlElement">Корневой элемент</param>
	private void Set(XmlElement xmlElement)
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
					Section section = new();
					section.part = int.Parse(attr?.Value);
					sections.Add(section);
				}
				// у диалога создаем экземпляр и записываем к последнему добавленному элементу секции
				if (xmlElement.Name == "dialogue")
				{
					Dialogue dialogue = new();
					dialogue.number = (int.Parse(attr?.Value));
					sections[^1].dialogues.Add(dialogue);
				}
			}
			//и спускаемся ниже по дереву
			foreach (XmlElement xeNode in xmlElement.ChildNodes) Set(xeNode);
		}
		else
		{
			if (attr?.Value != null)
			{
				//у персонажа создаем экземпляр и записываем к последней добавленной секции
				if (xmlElement.Name == "character")
				{
					Character character = new();
					character.name = xmlElement.InnerText;
					character.sprite = (Sprite[])Resources.LoadAll<Sprite>("Novella/Characters/" + xmlElement.Attributes["name"].InnerText + "/");
					sections[^1].character = character;
				}
				//у фразы создаем экземпляр и записываем к последнему добавленному диалогу
				if (xmlElement.Name == "phrase")
				{
					Phrase phrase = new();
					phrase.phrase = (xmlElement.InnerText);
					sections[^1].dialogues[^1].phrases.Add(phrase);
				}
				//у ответа создаем экземпляр и записываем к последней добавленной фразе
				if (xmlElement.Name == "answer")
				{
					Answer answer = new();
					answer.nextphrase = int.Parse(xmlElement.Attributes["nextphrase"].InnerText);
					answer.value = int.Parse(xmlElement.Attributes["value"].InnerText);
					answer.answer = xmlElement.InnerText;
					sections[^1].dialogues[^1].phrases[^1].answers.Add(answer);
				}
			}
			else //если нет детей и атрибутов, то просто проверяем, нужный ли нам это тэг и записываем его в соответствующее значение к секции
			{				
				//тут был персонаж
			}
		}
	}

	#region classXML
	public class Section
	{
		//[XmlAttribute("part")]
		public int part;

		//[XmlElement("character")]
		public Character character;

		//[XmlArray("dialogue")]
		public List<Dialogue> dialogues = new();
	}
	public class Dialogue
	{
		//[XmlAttribute("num")]
		public int number;

		//[XmlArray("phrase")]
		public List<Phrase> phrases = new();
	}
	public class Phrase
	{
		//[XmlAttribute("num")]
		public int number;

		//[XmlElement("phrase")]
		public string phrase;

		//[XmlArray("answer")]
		public List<Answer> answers = new();
	}
	public class Answer
	{
		//[XmlElement("answer")]
		public string answer;

		//[XmlAttribute("value")]
		public int value;

		//[XmlAttribute("nextphrase")]
		public int nextphrase;
	}
	public class Character
	{
		//[XmlElement("character")]
		public string name;

		public Sprite[] sprite;
	}
	#endregion

}