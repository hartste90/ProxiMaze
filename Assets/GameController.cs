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
    public FirstPersonController playerPrefab;

    FirstPersonController player;
    // Start is called before the first frame update
    void Start()
    {
        //create level
        LevelController.BeginTrial(trialDataList[currentTrialIdx]);
        //create player
        //enable input
    }

    public static FirstPersonController GetPlayer()
    {
        if (Instance.player == null)
        {
            Instance.player = Instantiate<FirstPersonController>(Instance.playerPrefab);
        }
        return Instance.player;
    }
}
