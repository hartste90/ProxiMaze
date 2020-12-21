using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    #region singleton
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameController>();
                if (instance == null)
                {
                    Debug.LogError("No GameController was found in this scene. Make sure you place one in the scene.");
                    return instance;
                }
            }
            return instance;
        }
    }
    #endregion

    public List<TrialData> trialDataList;
    public int currentTrialIdx = 0;
    public Camera cameraPrefab;
    public FirstPersonController playerPrefab;
    public UIController uiController;

    FirstPersonController player;
    // Start is called before the first frame update
    void Start()
    {
        GetPlayer();
        //create level
        LevelController.BeginTrial(trialDataList[currentTrialIdx]);
        uiController.SetLevelText("Level " + (currentTrialIdx + 1));
    }



    public static FirstPersonController GetPlayer()
    {
        if (Instance.player == null)
        {
            Instance.player = Instantiate<FirstPersonController>(Instance.playerPrefab);
            Camera mainCamera = Instantiate<Camera>(Instance.cameraPrefab, Instance.player.transform);
            Instance.player.RegisterCamera(mainCamera);
        }
        return Instance.player;
    }

    public static void OnMazeEndReached()
    {
        Instance.uiController.ToggleTrialCompletePanel();
    }

    public static void OnContinueButtonPressed()
    {
        Instance.uiController.ToggleTrialCompletePanel();
        LevelController.EndTrial();
        Instance.currentTrialIdx++;
        Instance.BeginLevel();
        
    }

    public void BeginLevel()
    {
        uiController.SetLevelText("Level " + (currentTrialIdx + 1));
        LevelController.BeginTrial(trialDataList[currentTrialIdx]);
    }

}