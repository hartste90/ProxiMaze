using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StarView : MonoBehaviour
{
    public Color filledOutlineColor;
    public Color emptyOutlineColor;
    public Image outlineImage;
    public Image filledImage;

    public void ShowEmpty()
    {
        outlineImage.gameObject.SetActive(true);
        outlineImage.color = emptyOutlineColor;
        filledImage.gameObject.SetActive(false);
    }

    public void ShowFilled()
    {
        outlineImage.color = filledOutlineColor;
        filledImage.gameObject.SetActive(true);
    }

    public void AnimateFilled()
    {
        ShowFilled();
        transform.DOPunchScale(Vector3.one * .2f, .2f);
    }
}
