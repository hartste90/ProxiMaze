using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelController : MonoBehaviour
{
    #region singleton
    private static LevelController instance;
    public static LevelController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<LevelController>();
                if (instance == null)
                {
                    Debug.LogError("No LevelController was found in this scene. Make sure you place one in the scene.");
                    return instance;
                }
            }
            return instance;
        }
    }
    #endregion

    public Transform trialParent;

    TrialData trialData;
    TrialController trialController;
    FirstPersonController playerController;


    public static void BeginTrial(TrialData trialData)
    {
        Instance.trialData = trialData;
        Instance.BeginTrialImpl();
    }

    public void BeginTrialImpl()
    {
        //disappear old trial if necessary
        //appear next trial
        trialController = Instantiate<TrialController>(trialData.trialPrefab, trialParent);
        //move OR create player if necessary
        playerController = GameController.GetPlayer();
        trialController.SetPlayer(playerController);
        //allow input
        OnTrialBegin();

    }

    public static void OnTrialBegin()
    {
        TutorialController.ShowJoystickTutorial();
    }

    public static void OnMazeEndReached()
    {
        Instance.trialController.OnMazeEndReached();
    }

    
}
