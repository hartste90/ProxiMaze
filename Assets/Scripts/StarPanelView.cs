using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPanelView : MonoBehaviour
{

    public StarView star1;
    public StarView star2;
    public StarView star3;

    private int actualStarCount;
    private int visibleStarCount;

    public void OnLevelBegin()
    {
        actualStarCount = 0;
        visibleStarCount = 0;
        star1.ShowEmpty();
        star2.ShowEmpty();
        star3.ShowEmpty();
    }

    public void AddStarVirtual()
    {
        actualStarCount++;
    }

    public void AddStarActual(int amount = 1)
    {
        switch (visibleStarCount)
        {
            case 0:
                star1.AnimateFilled();
                visibleStarCount++;
                break;
            case 1:
                star2.AnimateFilled();
                visibleStarCount++;
                break;
            case 2:
                star3.AnimateFilled();
                visibleStarCount++;
                break;
            default:
                throw new System.Exception("Cannot add more than 3 stars");
        }
    }

    public Vector3 GetNextEmptyStarPos()
    {
        switch(actualStarCount)
        {
            case 0:
                return star1.transform.position;
            case 1:
                return star2.transform.position;
            case 2:
                return star3.transform.position;
            default:
                throw new System.Exception("Invalid number of filled stars");
        }
    }
}
