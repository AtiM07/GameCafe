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

    //toggle для кнопки выбора игры
    public Toggle[] games;

    //toggle для кнопки выбора локации
    public Toggle[] locations;

    //toggle для кнопки выбора локации
    public Toggle[] lvls;

    public void Play()
    {
        // скрывает панель выбора        
        if (!mainPanel.activeSelf) mainPanel.SetActive(true);

        //скрывает кнопку старта
        if (btnStart.activeSelf) btnStart.SetActive(false);

        // скрывает панель выбора сложности карточной игры        
        //if (!cardLvLPanel.activeSelf) cardLvLPanel.SetActive(true);
    }

    public void MainOK()
    {
        //тут передается стиль локации и переключается на игру
       

        //foreach (Toggle toggle in games)
        //    if (toggle.isOn) if (toggle.name == "tgglNovella") SceneManager.LoadScene("NovellaScene");

        foreach (Toggle toggle in locations)
            if (toggle.isOn) Debug.Log(toggle.name);
        foreach (Toggle toggle in games)
            if (toggle.isOn) Debug.Log(toggle.name);

        // если быбрана карточная игра, то предложить выбор сложности
        mainPanel.SetActive(false);
        cardLvLPanel.SetActive(true);        
    }
    public void LvlOK()
    {
        ////тут передается сложность игры
        foreach (Toggle toggle in lvls)
            if (toggle.isOn) Debug.Log(toggle.name);

    }
}
