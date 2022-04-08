using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class ChooseUIManager : MonoBehaviour
{
    public static ChooseUIManager Instance;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public GameObject mainPanel;
    public GameObject cardLvLPanel;

    public GameObject btnStart;

    //toggle ��� ������ ������ ����
    public Toggle[] games;

    //toggle ��� ������ ������ �������
    public Toggle[] locations;

    //toggle ��� ������ ������ �������
    public Toggle[] lvls;

    public void Play()
    {
        // �������� ������ ������        
        if (!mainPanel.activeSelf) mainPanel.SetActive(true);

        //�������� ������ ������
        if (btnStart.activeSelf) btnStart.SetActive(false);

        // �������� ������ ������ ��������� ��������� ����        
        //if (!cardLvLPanel.activeSelf) cardLvLPanel.SetActive(true);
    }

    public void MainOK()
    {
        //��� ���������� ����� ������� � ������������� �� ����
       

        //foreach (Toggle toggle in games)
        //    if (toggle.isOn) if (toggle.name == "tgglNovella") SceneManager.LoadScene("NovellaScene");

        foreach (Toggle toggle in locations)
            if (toggle.isOn) Debug.Log(toggle.name);
        foreach (Toggle toggle in games)
            if (toggle.isOn) Debug.Log(toggle.name);

        // ���� ������� ��������� ����, �� ���������� ����� ���������
        mainPanel.SetActive(false);
        cardLvLPanel.SetActive(true);        
    }
    public void LvlOK()
    {
        ////��� ���������� ��������� ����
        foreach (Toggle toggle in lvls)
            if (toggle.isOn) Debug.Log(toggle.name);

    }
}
