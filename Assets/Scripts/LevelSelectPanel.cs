using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPanel : MonoBehaviour
{
    public LevelSelectButton levelSelectButtonPrefab;
    public Transform levelSelectGridParent;

    bool isVisible = false;
    List<TrialData> gameTrialData;
    /// <summary>
    /// Create and populate all level select buttons according to player data
    /// </summary>
    public void Init(List<TrialData> trialDataList)
    {
        gameTrialData = trialDataList;
        PopulatePanel();
        
    }

    void PopulatePanel()
    {
        ClearPanel();

        int playerHighestUnlockedLevel = PrefsManager.GetPlayerUnlockedLevel();

        for (int i = 0; i < gameTrialData.Count; i++)
        {
            TrialData trialData = gameTrialData[i];
            //create button
            LevelSelectButton button = Instantiate<LevelSelectButton>(levelSelectButtonPrefab, levelSelectGridParent);
            int starsForLevel = PrefsManager.GetStarsForLevel(i);
            bool isLocked = playerHighestUnlockedLevel < i;
            button.Init(i + 1, starsForLevel, isLocked, (levelSelected) =>
            {
                OnSelectedLevel(levelSelected);
            });
        }
    }

    void ClearPanel()
    {
        foreach(Transform t in levelSelectGridParent)
        {
            Destroy(t.gameObject);
        }
    }

    public void OnSelectedLevel(int levelNum)
    {
        TogglePanel();
        GameController.OpenTrial(levelNum);
    }

    public void TogglePanel()
    {
        isVisible = !isVisible;
        Debug.Log("Toggling level select panel to: " + isVisible);
        if (isVisible)
        {
            PopulatePanel();
        }
        gameObject.SetActive(isVisible);
        
    }

    public void DebugOnDestroyPlayerPrefsButtonPressed()
    {
        PlayerPrefsPro.DeleteAll();
        PlayerPrefsPro.Save();
    }
}
