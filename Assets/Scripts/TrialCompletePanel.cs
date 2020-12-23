using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrialCompletePanel : MonoBehaviour
{
    public CanvasGroup cg;
    bool isShowing = true;
    

    private void Start()
    {
        Hide();
    }


    public void OnContinueButtonPressed()
    {
        GameController.OnContinueButtonPressed();
    }

    public void ToggleVisibility()
    {
        if (isShowing)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Show()
    {
        isShowing = true;
        gameObject.SetActive(true);
        cg.alpha = 0f;
        cg.DOFade(1f, .5f);
    }

    public void Hide()
    {
        isShowing = false;
        gameObject.SetActive(false);
        cg.alpha = 1f;
        cg.DOFade(0f, .5f);
    }


}
