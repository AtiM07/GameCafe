using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

class UIManager : MonoBehaviour
{
    public Animator contentPanel;
    public Toggle soundToggle;
    public GameObject rulesPanel;

    //изображения для меню
    public Image menu;
    public Sprite menuImg;
    public Sprite hideImg;

    //изображения для звука
    public Sprite volImg;
    public Sprite muteImg;

    //изображения для иконки текущей игры
    public Image game;
    public Sprite gameImg;
    public Sprite novImg;

    void Start()
    {
        // скрывает выезжающую панель меню
        contentPanel.enabled = false;
        RectTransform transform = contentPanel.gameObject.transform as RectTransform;
        Vector2 position = transform.anchoredPosition;
        position.y += transform.rect.height;
        transform.anchoredPosition = position;
        rulesPanel.SetActive(false);

        ToggleGame();
    }

    /// <summary>
    /// Запускает анимацию выезжающей панели и смену изображения кнопки меню
    /// </summary>
    public void ToggleMenu()
    {
        StartCoroutine(AnimMenu());
    }
    
    public IEnumerator AnimMenu()
    {
        contentPanel.enabled = true;

        bool isHidden = contentPanel.GetBool("isHidden");
        contentPanel.SetBool("isHidden", !isHidden);

        yield return StartCoroutine(ChangeImgMenu(isHidden));
    }
    public IEnumerator ChangeImgMenu(bool isHidden)
    {        
        if (isHidden)
        {
            yield return new WaitForSeconds(1f);

            menu.sprite = hideImg;
            //hideImg.gameObject.SetActive(isHidden);
            //menuImg.gameObject.SetActive(!isHidden);

            yield return new WaitForSeconds(1.2f);
        }
        else
        {
            yield return new WaitForSeconds(1.2f);
            menu.sprite = menuImg;
            //hideImg.gameObject.SetActive(isHidden);
            //menuImg.gameObject.SetActive(!isHidden);
        }
       
    }

    /// <summary>
    /// Включение/выключение звукового сопровождения 
    /// </summary>
    public void ToggleSound()
    { 
        if (soundToggle.isOn)
        {
            soundToggle.gameObject.GetComponent<Image>().sprite = volImg;
        }
        else
        {
            soundToggle.gameObject.GetComponent<Image>().sprite = muteImg;
        }

        //добавить отключение/включение звука
    }

    /// <summary>
    /// Переход между играми 
    /// </summary>
    public void ToggleGame()
    {
        if (SceneManager.GetActiveScene().name == "NovellaScene")
        {
            game.sprite = novImg;
        }
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            game.sprite = gameImg;
        }

        //добавить переход на другую игру
    }

    /// <summary>
    /// Активирует панель с правилами игры
    /// </summary>
    public void ToggleHelp()
    {
        rulesPanel.SetActive(true);
    }
    /// <summary>
    /// Активирует панель с правилами игры
    /// </summary>
    public void CloseRulesPanel()
    {
        rulesPanel.SetActive(false);
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void ExitMenu()
    {
        Application.Quit();
    }
}