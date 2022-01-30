using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VectorGraphics;
using UnityEngine.SceneManagement;

class UIManager : MonoBehaviour
{
    public Animator contentPanel;
    public Toggle soundToggle;

    //изображения для меню
    public SVGImage menuImg;
    public SVGImage hideImg;

    //изображения для звука
    public SVGImage muteImg;
    public SVGImage volImg;

    //изображения для иконки текущей игры
    public SVGImage gameImg;
    public SVGImage novImg;

    void Start()
    {
        // скрывает выезжающую панель меню
        contentPanel.enabled = false;
        RectTransform transform = contentPanel.gameObject.transform as RectTransform;
        Vector2 position = transform.anchoredPosition;
        position.y += transform.rect.height;
        transform.anchoredPosition = position;

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

            hideImg.gameObject.SetActive(isHidden);
            menuImg.gameObject.SetActive(!isHidden);

            yield return new WaitForSeconds(1.2f);
        }
        else
        {
            yield return new WaitForSeconds(1.2f);
            hideImg.gameObject.SetActive(isHidden);
            menuImg.gameObject.SetActive(!isHidden);
        }
       
    }

    /// <summary>
    /// Включение/выключение звукового сопровождения 
    /// </summary>
    public void ToggleSound()
    { 
        muteImg.gameObject.SetActive(!soundToggle.isOn);
        volImg.gameObject.SetActive(soundToggle.isOn);

        //добавить отключение/включение звука
    }

    /// <summary>
    /// Переход между играми 
    /// </summary>
    public void ToggleGame()
    {
        if (SceneManager.GetActiveScene().name == "NovellaScene")
        {
            gameImg.gameObject.SetActive(false);
            novImg.gameObject.SetActive(true);
        }
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            novImg.gameObject.SetActive(false);
            gameImg.gameObject.SetActive(true);
        }

        //добавить переход на другую игру
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void ExitMenu()
    {
        Application.Quit();
    }
}