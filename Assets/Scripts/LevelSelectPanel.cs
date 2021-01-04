using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectPanel : MonoBehaviour
{
    public LevelSelectButton levelSelectButtonPrefab;
    public Transform levelSelectGridParent;
    public TextMeshProUGUI worldLabel;
    public TextMeshProUGUI starCountLabel;
    public TextMeshProUGUI worldProgressLabel;

    bool isVisible = false;
    List<WorldData> worldDataList;
    /// <summary>
    /// Create and populate all level select buttons according to player data
    /// </summary>
    public void Init(List<WorldData> worldDataListSet, int worldIdx)
    {
        worldDataList = worldDataListSet;
        PopulatePanel(worldIdx);
        
    }

    void PopulatePanel(int worldIdx)
    {
        ClearPanel();
        List<TrialData> trialData = worldDataList[worldIdx].GetTrialDataList();
        int playerHighestUnlockedLevel = PrefsManager.GetPlayerUnlockedLevel();
        int currentWorldStars = GetCurrentStars(worldIdx);
        int totalStars = trialData.Count * 3;
        worldLabel.text = string.Format("World {0}", worldIdx+1);
        starCountLabel.text = string.Format("{0}/{1}", currentWorldStars, totalStars);
        float normalizedProgress = (float)currentWorldStars / (float)totalStars;
        float progress = 100 * normalizedProgress;
        int roundedProgress = (int)Mathf.Ceil(progress);
        int cappedProgress = Mathf.Min(100, roundedProgress);
        worldProgressLabel.text = string.Format("{0}% complete", cappedProgress);

        for (int i = 0; i < trialData.Count; i++)
        {
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

    int GetCurrentStars(int worldIdx)
    {
        List<TrialData> trialData = worldDataList[worldIdx].GetTrialDataList();
        int starCount = 0;
        for(int i = 0; i < trialData.Count; i++)
        {
            starCount += PrefsManager.GetStarsForLevel(worldIdx, i);
        }
        return starCount;
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
            PopulatePanel(GameController.GetCurrentWorldIdx());
        }
        gameObject.SetActive(isVisible);
        
    }

    public void DebugOnDestroyPlayerPrefsButtonPressed()
    {
        PlayerPrefsPro.DeleteAll();
        PlayerPrefsPro.Save();
    }
}
