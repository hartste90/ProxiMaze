using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using DG.Tweening;

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

    public List<WorldData> worldDataList;
    public int currentWorldIdx = 0;
    public int currentTrialIdx = 0;
    public Camera cameraPrefab;
    public PlayerController playerPrefab;
    //public FirstPersonController playerPrefab;
    public UIController uiController;
    public Joystick joystick;

    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        InitData();
        uiController.InitLevelSelect(worldDataList, 0);
        DOTween.SetTweensCapacity(1250, 500);
        GetPlayer();
        //create level
        LevelController.BeginTrial(GetCurrentTrialData());
        uiController.SetLevelText("Level " + (currentTrialIdx + 1));
    }

    TrialData GetCurrentTrialData()
    {
        return worldDataList[currentWorldIdx].GetTrialDataList()[currentTrialIdx];
    }

    void InitData()
    {
        currentTrialIdx = PrefsManager.GetPlayerUnlockedLevel();
    }



    public static PlayerController GetPlayer()
    {
        if (Instance.player == null)
        {
            Instance.player = Instantiate<PlayerController>(Instance.playerPrefab);
            Camera mainCamera = Instantiate<Camera>(Instance.cameraPrefab, Instance.player.transform);
            Instance.player.RegisterCamera(mainCamera);
        }
        return Instance.player;
    }

    public static void OnMazeEndReached()
    {
        Instance.uiController.OnTrialComplete();
        PrefsManager.SetUnlockedLevel(Instance.currentTrialIdx+1);

    }

    public static void OnContinueButtonPressed()
    {
        Instance.uiController.ToggleTrialCompletePanel();
        Instance.currentTrialIdx++;
        LevelController.TransitionToTrial(Instance.GetCurrentTrialData());
        //Instance.currentTrialIdx++;
        //Instance.BeginLevel();
        
    }

    public static void OpenTrial(int trialNum)
    {
        Instance.currentTrialIdx = trialNum;
        LevelController.TransitionToTrial(Instance.GetCurrentTrialData());
    }

    public static void OnBeginTrial()
    {
        Instance.uiController.SetLevelText("Level " + (Instance.currentTrialIdx + 1));
        //LevelController.BeginTrial(Instance.trialDataList[Instance.currentTrialIdx]);
        Instance.player.enabled = true;
        EnableJoystick();
        Instance.uiController.OnTrialBegin();
    }

    public static void DisableJoystick()
    {
        Instance.joystick.ResetJoystick();
        Instance.joystick.transform.parent.gameObject.SetActive(false);
    }

    public static void EnableJoystick()
    {
        if (Instance.player.enabled)
        {
            Instance.joystick.transform.parent.gameObject.SetActive(true);
            Instance.joystick.ResetJoystick();
        }
    }

    public static void OnStarPickupCollected(Vector3 origin)
    {
        Instance.uiController.OnStarPickupCollected(origin);
    }

    public static int GetCurrentTrialIdx()
    {
        return Instance.currentTrialIdx;
    }

}