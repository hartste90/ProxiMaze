using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityStandardAssets.CrossPlatformInput;

public class TutorialController : MonoBehaviour
{
    #region singleton
    private static TutorialController instance;
    public static TutorialController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<TutorialController>();
                if (instance == null)
                {
                    Debug.LogError("No TutorialController was found in this scene. Make sure you place one in the scene.");
                    return instance;
                }
            }
            return instance;
        }
    }
    #endregion

    private bool isShowingJoystickTutorial = false;
    public CanvasGroup joystickTutorialPanel;


    private void Update()
    {
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        if (isShowingJoystickTutorial && (horizontal != 0 || vertical != 0))
        {
            HideJoystickTutorial();
        }
    }
    public static void ShowJoystickTutorial()
    {
        Instance.isShowingJoystickTutorial = true;
        Instance.joystickTutorialPanel.gameObject.SetActive(true);
        Instance.joystickTutorialPanel.DOFade(1f, .1f);
    }

    public static void HideJoystickTutorial()
    {
        Instance.isShowingJoystickTutorial = false;
        Instance.joystickTutorialPanel.gameObject.SetActive(false);
        Instance.joystickTutorialPanel.DOFade(0f, .1f);
    }

    public static bool IsShowingJoystickTutorial()
    {
        return Instance.isShowingJoystickTutorial;
    }
}
