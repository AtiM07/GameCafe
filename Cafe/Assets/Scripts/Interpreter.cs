using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEditor.UI;
using UnityEngine.UI;
using TMPro;

public class Interpreter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        XmlDocument xDoc = new XmlDocument();

        xDoc.Load("scenario.xml");

        // корневой элемент
        XmlElement xRoot = xDoc.DocumentElement;

        if (xRoot != null)
        {
            showFullScenario(xRoot);    
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] 
     private TextMeshProUGUI str;

     void showFullScenario(XmlElement xmlElement)
    {
        XmlNode attr = null;

        if (xmlElement.Attributes != null)
            attr = xmlElement.Attributes.Item(0);

        bool hasChild = (xmlElement.ChildNodes.Count != 1) ? true : false;

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
            foreach (XmlElement xeNode in xmlElement.ChildNodes) showFullScenario(xeNode);

    }

}
