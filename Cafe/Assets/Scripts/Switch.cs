using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject[] rools;
    [SerializeField] private int num;
    [SerializeField] private GameObject rightBtn, leftBtn;
    [SerializeField] private TextMeshProUGUI rightTxt, leftTxt;

    private void Start()
    {
        num = 1;
        Change();
    }
    public void Next()
    {
        num++;
        Change();
    }
    public void Prev()
    {
        num--;
        Change();
    }
    private void Change() //смена правил игры
    {
        num = CheckNum();
        foreach (GameObject rool in rools)
            if (rool == rools[num])
            {
                Debug.Log(rool.name);
                rool.SetActive(true);
            }
            else
            {
                rool.SetActive(false);
            }
            //if (rool == rools[num]) rool.SetActive(true);
            //else rool.SetActive(false);
        ChangeBtn();
    }
    private int CheckNum()
    {
        if (num < 0) num = 0;
        else if (num > rools.Length-1) num = rools.Length-1;
        return num;
    }
    private void ChangeBtn() //работа с кнопками
    {
        if (num == 0)
        {
            Debug.Log("0");
            if (!rightBtn.activeSelf) rightBtn.SetActive(true);
            if (leftBtn.activeSelf) leftBtn.SetActive(false);
            rightTxt.text = "Общие правила >>";
        }
        else if (num == rools.Length-1)
        {
            Debug.Log("2");
            if (!leftBtn.activeSelf) leftBtn.SetActive(true);
            if (rightBtn.activeSelf) rightBtn.SetActive(false);
            leftTxt.text = "<< Общие правила";
        }
        else if (num == ((rools.Length-1)/2))
        {
            Debug.Log("1");
            if (!rightBtn.activeSelf) rightBtn.SetActive(true);
            if (!leftBtn.activeSelf) leftBtn.SetActive(true);
            rightTxt.text = "Карточная игра >>";
            leftTxt.text = "<< Новелла";
        }
    }

}
