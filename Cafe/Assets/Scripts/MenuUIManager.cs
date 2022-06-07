using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
class MenuUIManager : MonoBehaviour
{
    public static MenuUIManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    [SerializeField]
    private Animator contentPanel;
    [SerializeField]
    private Toggle volumeToggle;
    [SerializeField]
    private Toggle soundToggle;
    [SerializeField]
    private GameObject rulesPanel;

    //изображения для меню
    [SerializeField]
    private Image menu;
    [SerializeField]
    private Sprite menuImg;
    [SerializeField]
    private Sprite hideImg;

    //изображения для звука
    [SerializeField]
    private Sprite volImg;
    [SerializeField]
    private Sprite muteImg;
    [SerializeField]
    private Sprite soundImg;
    [SerializeField]
    private Sprite unsoundImg;

    private void Start()
    {
        // скрывает выезжающую панель меню
        contentPanel.enabled = false;
        RectTransform transform = contentPanel.gameObject.transform as RectTransform;
        Vector2 position = transform.anchoredPosition;
        position.y += transform.rect.height;
        transform.anchoredPosition = position;
        rulesPanel.SetActive(false);

        soundToggle.isOn = Interpreter.Sound;
        volumeToggle.isOn = Interpreter.Volume;
    }

    public void ToggleMenu()// Запускает анимацию выезжающей панели и смену изображения кнопки меню
    {
        StartCoroutine(AnimMenu());
        SaveMenu();
    }
    private void SaveMenu()
    {
        Interpreter.Volume = volumeToggle.isOn;
        Interpreter.Volume = soundToggle.isOn;
    }
    private IEnumerator AnimMenu()
    {
        contentPanel.enabled = true;

        bool isHidden = contentPanel.GetBool("isHidden");
        contentPanel.SetBool("isHidden", !isHidden);

        yield return StartCoroutine(ChangeImgMenu(isHidden));
    }
    private IEnumerator ChangeImgMenu(bool isHidden)
    {        
        if (isHidden)
        {
            yield return new WaitForSeconds(1f);

            menu.sprite = hideImg;

            yield return new WaitForSeconds(1.2f);
        }
        else
        {
            yield return new WaitForSeconds(1.2f);
            menu.sprite = menuImg;
        }       
    }
    public void ToggleVolume()// Включение/выключение звукового сопровождения 
    { 
        if (volumeToggle.isOn)
        {
            volumeToggle.gameObject.GetComponent<Image>().sprite = volImg;
        }
        else
        {
            volumeToggle.gameObject.GetComponent<Image>().sprite = muteImg;
        }

        Interpreter.Volume = volumeToggle.isOn;
        //добавить отключение/включение звука
    }
    public void ToggleSound()// Включение/выключение мелодии
    {
        if (soundToggle.isOn)
        {
            soundToggle.gameObject.GetComponent<Image>().sprite = soundImg;
        }
        else
        {
            soundToggle.gameObject.GetComponent<Image>().sprite = unsoundImg;
        }
        Interpreter.Sound = soundToggle.isOn;
    }
    public void AnimPanel() // Активирует панель с правилами игры
    {
        rulesPanel.SetActive(!rulesPanel.activeSelf);
    }
    public void ClosePanel()// Деактивирует панель c правилами игры
    {
        rulesPanel.SetActive(false);
    }
    public void ExitMenu() //выход из игры
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        Application.Quit();
        else
            SceneManager.LoadScene("MainScene");
    }
}