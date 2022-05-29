using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    static public Card Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField]
    private Image _image;
    [SerializeField]
    private Image _imageCond;

    private string _object;
    private string _condition; 
    Levels _level = Levels.Easy;
    private int _position;
    private bool _onDeck = true; //находится ли она в стеке карт
    public Sprite Image { set { _image.sprite = value; } }
    public Sprite ImageCond { set { _imageCond.sprite = value; } }

    /// <summary>
    /// Объект-карта
    /// </summary>
    public string Object { set { _object = value; } get { return _object; } }
    /// <summary>
    /// Условие выполнение карты
    /// </summary>
    public string Condition { set { _condition = value; } get { return _condition; } }
    public Levels Level { set { _level = value; } get { return _level; } }
    public int Position { set { _position = value; } get { return _position; } }
    public bool OnDeck { set { _onDeck = value; } get { return _onDeck; } }

    public enum Levels { Easy, Normal, Hard}
}
