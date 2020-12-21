using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialCompletePanel : MonoBehaviour
{
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
    }

    public void Hide()
    {
        isShowing = false;
        gameObject.SetActive(false);
    }


}
