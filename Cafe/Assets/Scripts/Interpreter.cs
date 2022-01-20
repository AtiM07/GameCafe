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

        // �������� �������
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
            //���� � ���� ���� �������, �� ������� �������� ��������
            if (attr?.Value != null)               
                str.text += attr?.Value + "\n";               
        }
        else
        {            
            if (attr?.Value != null)
            {
                //� ����� ������ ������� �������� ����
                if (xmlElement.Name == "phrase")
                    
                    str.text += "�����: " + xmlElement.InnerText + "; \n";
                //� ������ ������� �������� ���� � ��� ��������
                if (xmlElement.Name == "answer")
                {                   
                    str.text += "�����: " + xmlElement.InnerText + "; ";
                    foreach (XmlAttribute x in xmlElement.Attributes)
                    {
                        if (x.Name == "nextphrase")
                           
                            str.text += "��������� �����: " + x.InnerText + "; ";
                        if (x.Name == "value")
                            
                            str.text += "��������� ������: " + x.InnerText + "; ";
                    }
                }

                str.text += "\n";
            }
            else //���� ��� ����� � ���������, �� ������ ������� �������� ����
                str.text += xmlElement.InnerText + "\n";
        }
        //���������� ���� �� ������
        if (hasChild)
            foreach (XmlElement xeNode in xmlElement.ChildNodes) showFullScenario(xeNode);

    }

}
