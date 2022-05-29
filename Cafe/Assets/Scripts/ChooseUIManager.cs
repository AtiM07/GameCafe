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

    //toggle ��� ������ ������ ����
    [SerializeField]
    private Toggle[] games;

    //toggle ��� ������ ������ �������
    [SerializeField]
    private Toggle[] locations;

    //toggle ��� ������ ������ �������
    [SerializeField]
    private Toggle[] lvls;

    public void Play()
    {
        // �������� ������ ������        
        if (!mainPanel.activeSelf) mainPanel.SetActive(true);

        //�������� ������ ������
        if (startBtn.activeSelf) startBtn.SetActive(false);
    }

    public void MainOK() //�������� ����� ������� � ������������ �� ����
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
                if (toggle.name == "CardsGame") //���� ������� ��������� ����, �� ���������� ����� ����������
                {
                    mainPanel.SetActive(false);
                    cardLvLPanel.SetActive(true);
                }

    }
    public void LvlOK() //�������� ��������� ����
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
