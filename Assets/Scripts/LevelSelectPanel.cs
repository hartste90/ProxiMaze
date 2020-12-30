using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPanel : MonoBehaviour
{
    public LevelSelectButton levelSelectButtonPrefab;
    public Transform levelSelectGridParent;

    bool isVisible = false;
    /// <summary>
    /// Create and populate all level select buttons according to player data
    /// </summary>
    public void Init(List<TrialData> trialDataList)
    {
        int playerHighestUnlockedLevel = PrefsManager.GetPlayerUnlockedLevel();

        for (int i = 0; i < trialDataList.Count; i++)
        {
            TrialData trialData = trialDataList[i];
            //create button
            LevelSelectButton button = Instantiate<LevelSelectButton>(levelSelectButtonPrefab, levelSelectGridParent);
            bool isLocked = playerHighestUnlockedLevel < i;
            button.Init(i + 1, PrefsManager.GetStarsForLevel(i), isLocked, (levelSelected) =>
              {
                  OnSelectedLevel(levelSelected);
              });
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
        gameObject.SetActive(isVisible);
        
    }
}
