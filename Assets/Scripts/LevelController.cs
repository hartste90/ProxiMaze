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
    public Transform environment;

    TrialData trialData;
    TrialController trialController;
    FirstPersonController playerController;

    public static void TransitionToTrial(TrialData data)
    {
        Instance.TransitionToTrialImpl(data);
    }

    public void TransitionToTrialImpl(TrialData data)
    {
        //outro last trial
        EndTrialImpl();
        //intro this trial
        trialData = data;
        Invoke("BeginTrialImpl", 1.2f);
    }

    public static void BeginTrial(TrialData trialData)
    {
        Instance.trialData = trialData;
        Instance.BeginTrialImpl();
        
    }

    void BeginTrialImpl()
    {
        //disappear old trial if necessary
        //move OR create player if necessary
        playerController = GameController.GetPlayer();
        //appear next trial
        trialController = Instantiate<TrialController>(trialData.trialPrefab, trialParent);
        Vector3 offset = playerController.transform.position - trialController.GetPlayerStartPosition();
        trialController.transform.position = new Vector3(offset.x, 0, offset.z);
        environment.position = new Vector3(trialController.mazeCenterAnchor.position.x, 0, trialController.mazeCenterAnchor.position.z);


        trialController.SetPlayer(playerController);
        //allow input
        TutorialController.ShowJoystickTutorial();
        trialController.BeginTrial();
        GameController.OnBeginTrial();
    }

    public static void EndTrial()
    {
        Instance.EndTrialImpl();
    }

    void EndTrialImpl()
    {
        trialController.EndTrial();
        trialController = null;
        trialData = null;
    }


    public static void OnMazeEndReached()
    {
        Instance.trialController.OnMazeEndReached();
        GameController.OnMazeEndReached();
    }

    
}
