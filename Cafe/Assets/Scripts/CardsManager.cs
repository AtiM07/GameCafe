using UnityEngine;

public class CardsManager : MonoBehaviour
{
    static public CardsManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
   
    //enum Persons { Fairy, Elf, Leprechaun, Kikimora }
    //enum Foods { Tea, Coffee, Cake, Icecream }

    [System.Serializable]
    public class Person
    {
        public string name;
        public Sprite image;
        public Sprite image_normal;
        public Sprite image_hard;
    }

    [System.Serializable]
    public class Food
    {
        public string name;
        public Sprite image;
        public Sprite image_normal;
        public Sprite image_hard;
    }

    [SerializeField]
    private Person[] person;

    [SerializeField]
    private Food[] food;
    private Person RandomPerson()
    {
        int rrint = Random.Range(0, person.Length);
        return person[rrint];
    }
    private Food RandomFood()
    {
        int rrint = Random.Range(0, food.Length);
        return food[rrint];
    }

    private Person _person;
    private Food _food;
    public Card SetCardPerson(Card card)
    {
        _person = RandomPerson();
        card.Object = _person.name;
        card.Image = _person.image;
        int a = Random.Range(1, 3); 
        switch (a) //возвращает либо условие-персонажа, либо условие-еда
        {
            case 1:
                _person = RandomPerson();
                card.Condition = _person.name;
                card.ImageCond = _person.image;
                break;
            case 2:
                _food = RandomFood();
                card.Condition = _food.name;
                card.ImageCond = _food.image;
                break;
        }
        return card;
    }
    public Card SetCardFood(Card card)
    {
        _food = RandomFood();
        card.Object = _food.name;
        card.Image = _food.image;

        _food = RandomFood();
        card.Condition = _food.name;
        card.ImageCond = _food.image;
        return card;
    }

    /// <summary>
    /// √енерирует рандомную карточку повышенного уровн€
    /// </summary>
    /// <param name="card">объект-карта</param>
    /// <param name="level">сложность уровн€</param>
    /// <returns></returns>
    public Card SetRandomLevelCard(Card card, Card.Levels level)
    {
        _person = RandomPerson();
        card.Object = _person.name;
        card.Image = _person.image;
        int a = Random.Range(1, 3);
        switch (a)
        {
            case 1:
                _person = RandomPerson();
                card.Condition = _person.name;
                if (level == Card.Levels.Normal)
                {
                    card.ImageCond = _person.image_normal;
                    card.Level = Card.Levels.Normal;
                }
                else if (level == Card.Levels.Hard)
                {
                    card.ImageCond = _person.image_hard;
                    card.Level = Card.Levels.Hard;
                }
                break;
            case 2:
                _food = RandomFood();
                card.Condition = _food.name;
                if (level == Card.Levels.Normal)
                {
                    card.ImageCond = _food.image_normal;
                    card.Level = Card.Levels.Normal;
                }
                else if (level == Card.Levels.Hard)
                {
                    card.ImageCond = _food.image_hard;
                    card.Level = Card.Levels.Hard;
                }
                break;
        }

        return card;
    }
}
