using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StarView : MonoBehaviour
{
    public Image filledImage;

    public void ShowEmpty()
    {
        filledImage.gameObject.SetActive(false);
    }

    public void ShowFilled()
    {
        filledImage.gameObject.SetActive(true);
    }

    public void AnimateFilled()
    {
        ShowFilled();
        transform.DOPunchScale(Vector3.one * .2f, .2f);
    }
}
