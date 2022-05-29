using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    private int _widthGroup, _heightGroup;
    private RectTransform _rectTransform;
    private GridLayoutGroup _group;
    [SerializeField]
    private GridLayoutGroup groupDesk;
    private int _column = 2;

    //помогает в работе с сеткой объектов на сцене
    private void Start()
    {
        _group = GetComponent<GridLayoutGroup>();
        _rectTransform = GetComponent<RectTransform>();
        _widthGroup = (int)_rectTransform.rect.width;
        _heightGroup = (int)_rectTransform.rect.height;
        _column = _group.constraintCount;
        double cell = (_widthGroup) / (_column+0.7);
        cell = Round(CheckCell(cell));
        groupDesk.cellSize = _group.cellSize = new Vector2((float)cell, (float)cell);
        groupDesk.spacing = CheckSpace(groupDesk.spacing);
        int x = (int) (groupDesk.spacing.x*1.1);
        groupDesk.padding = new RectOffset(x,x,x,x);
        
    }
    double CheckCell(double cell)
    {
        _group.padding.top = Round ((int) cell/ 3);
        if (cell * 4 + _group.spacing.x * 3 + _group.padding.top*2 > _heightGroup)
            return CheckCell(cell *= 0.9);
        else
            return cell;
    }

    int Round(double cell)
    {
        if (cell % 10 != 0)
            if (cell % 10 > 5) return ((int)cell / 10 + 1) * 10;
            else return ((int)cell / 10) * 10;
        else return (int) cell;
    }

    Vector2 CheckSpace(Vector2 space)
    {
        if (groupDesk.cellSize.x * 4 + space.x * 5 > _heightGroup)
            return (groupDesk.spacing.x - 5 > 10) ? CheckSpace(space -= new Vector2(5, 5)) : space ;
        else
           return space;
    }


}
