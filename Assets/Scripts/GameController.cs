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

    public List<TrialData> trialDataList;
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
        uiController.InitLevelSelect(trialDataList);
        DOTween.SetTweensCapacity(1250, 500);
        GetPlayer();
        //create level
        LevelController.BeginTrial(trialDataList[currentTrialIdx]);
        uiController.SetLevelText("Level " + (currentTrialIdx + 1));
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
        Instance.uiController.ToggleTrialCompletePanel();
    }

    public static void OnContinueButtonPressed()
    {
        Instance.uiController.ToggleTrialCompletePanel();
        Instance.currentTrialIdx++;
        PrefsManager.SetUnlockedLevel(Instance.currentTrialIdx);
        LevelController.TransitionToTrial(Instance.trialDataList[Instance.currentTrialIdx]);
        //Instance.currentTrialIdx++;
        //Instance.BeginLevel();
        
    }

    public static void OpenTrial(int trialNum)
    {
        Instance.currentTrialIdx = trialNum;
        LevelController.TransitionToTrial(Instance.trialDataList[Instance.currentTrialIdx]);
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

}