using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChooseUIManager : MonoBehaviour
{
    public static ChooseUIManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private GameObject cardLvLPanel;

    [SerializeField]
    private GameObject startBtn;

    //toggle для кнопки выбора игры
    [SerializeField]
    private Toggle[] games;

    //toggle для кнопки выбора локации
    [SerializeField]
    private Toggle[] locations;

    //toggle для кнопки выбора локации
    [SerializeField]
    private Toggle[] lvls;

    public void Play()
    {
        // скрывает панель выбора        
        if (!mainPanel.activeSelf) mainPanel.SetActive(true);

        //скрывает кнопку старта
        if (startBtn.activeSelf) startBtn.SetActive(false);
    }

    public void MainOK() //передача стиля локации и переключение на игру
    {

        foreach (Toggle toggle in locations)
            if (toggle.isOn)
            {
                Interpreter.Location = int.Parse(toggle.name);
            }

        foreach (Toggle toggle in games)
            if (toggle.isOn)
                if (toggle.name == "Novella") SceneManager.LoadScene("NovellaScene");
                else
                if (toggle.name == "CardsGame") //если выбрана карточная игра, то предложить выбор сложностиы
                {
                    mainPanel.SetActive(false);
                    cardLvLPanel.SetActive(true);
                }

    }
    public void LvlOK() //передача сложности игры
    {
        foreach (Toggle toggle in lvls)
            if (toggle.isOn)
            {
                Interpreter.LevelCardsGame = toggle.name;
            }
        SceneManager.LoadScene("CardGameScene");
    }

    [SerializeField]
    private GameObject locationPanel;
    [SerializeField]
    private Image imageLocationPanel;
    public void OpenImage(Image image)
    {
        locationPanel.SetActive(!locationPanel.activeSelf);
        imageLocationPanel.sprite = image.sprite;
    }
    public void CloseImage()
    {
        locationPanel.SetActive(!locationPanel.activeSelf);
    }
}
