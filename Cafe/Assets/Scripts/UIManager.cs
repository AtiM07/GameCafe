using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VectorGraphics;
class UIManager : MonoBehaviour
{
    public Animator contentPanel;
    public SVGImage menuImg;
    public SVGImage hideImg;

    void Start()
    {
        // скрывает выезжающую панель меню
        contentPanel.enabled = false;
        RectTransform transform = contentPanel.gameObject.transform as RectTransform;
        Vector2 position = transform.anchoredPosition;
        position.y += transform.rect.height;
        transform.anchoredPosition = position;
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
    /// Выход из игры
    /// </summary>
    public void ExitMenu()
    {
        Application.Quit();
    }
}