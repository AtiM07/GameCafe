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
    private Toggle soundToggle;
    [SerializeField]
    private GameObject rulesPanel;
    [SerializeField]
    private GameObject resultPanel;

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

    //изображения для иконки текущей игры
    [SerializeField]
    private Image game;
    [SerializeField]
    private Sprite gameImg;
    [SerializeField]
    private Sprite novImg;
    [SerializeField]
    private Sprite noneImg;

    private void Start()
    {
        // скрывает выезжающую панель меню
        contentPanel.enabled = false;
        RectTransform transform = contentPanel.gameObject.transform as RectTransform;
        Vector2 position = transform.anchoredPosition;
        position.y += transform.rect.height;
        transform.anchoredPosition = position;
        rulesPanel.SetActive(false);

        SpriteGame();
        soundToggle.isOn = Interpreter.Volume;
    }

    public void ToggleMenu()// Запускает анимацию выезжающей панели и смену изображения кнопки меню
    {
        StartCoroutine(AnimMenu());
        SaveMenu();
    }
    private void SaveMenu()
    {
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
    public void ToggleSound()// Включение/выключение звукового сопровождения 
    { 
        if (soundToggle.isOn)
        {
            soundToggle.gameObject.GetComponent<Image>().sprite = volImg;
        }
        else
        {
            soundToggle.gameObject.GetComponent<Image>().sprite = muteImg;
        }

        Interpreter.Volume = soundToggle.isOn;
        //добавить отключение/включение звука
    }
    public void SpriteGame()// Динамическое изменение изображения игры
    {
        if (SceneManager.GetActiveScene().name == "NovellaScene")
        {
            game.sprite = novImg;
        }
        else
        if (SceneManager.GetActiveScene().name == "CardGameScene")
        {
            game.sprite = gameImg;
        }
        else game.sprite = noneImg;
    }
    public void ToggleHelp() // Активирует панель с правилами игры
    {
        rulesPanel.SetActive(true);
    }
    public void ClosePanel()// Деактивирует панель c правилами игры
    {
        rulesPanel.SetActive(false);
    }

    public void ExitMenu() //выход из игры
    {
        Application.Quit();
    }
}