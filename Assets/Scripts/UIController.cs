using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class UIController : MonoBehaviour
{
    public TextMeshProUGUI levelLabel;
    public TrialCompletePanel trialCompletePanel;
    public StarPanelView starPanelView;
    public Doober starDooberPrefab;
    public LevelSelectPanel levelSelectPanel;

    int starsCollected;

    public void SetLevelText(string levelSet)
    {
        levelLabel.text = levelSet;
    }

    public void ToggleTrialCompletePanel()
    {
        trialCompletePanel.ToggleVisibility();
    }

    public void OnTrialBegin()
    {
        starsCollected = 0;
        starPanelView.OnLevelBegin();
    }

    public void OnTrialComplete()
    {
        ToggleTrialCompletePanel();
        PrefsManager.SetStarsForLevel(GameController.GetCurrentTrialIdx(), starsCollected);
    }

    public void OnStarPickupCollected(Vector3 origin)
    {
        starsCollected++;
        Doober starDoober = Instantiate<Doober>(starDooberPrefab, starPanelView.transform);
        Vector3 screenOrigin = Camera.main.WorldToScreenPoint(origin);
        starDoober.Init(1, screenOrigin, GetNextEmptyStarPos(), starPanelView.AddStarActual);
        starPanelView.AddStarVirtual();
    }

    public Vector3 GetNextEmptyStarPos()
    {
        return starPanelView.GetNextEmptyStarPos();
    }

    public void InitLevelSelect(List<WorldData> worldDataList, int worldIdx)
    {
        levelSelectPanel.Init(worldDataList, worldIdx);
    }
}
