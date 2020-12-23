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
        starPanelView.OnLevelBegin();
    }

    public void OnStarPickupCollected(Vector3 origin)
    {
        Doober starDoober = Instantiate<Doober>(starDooberPrefab, starPanelView.transform);
        Vector3 screenOrigin = Camera.main.WorldToScreenPoint(origin);
        starDoober.Init(1, screenOrigin, GetNextEmptyStarPos(), starPanelView.AddStarActual);
        starPanelView.AddStarVirtual();
    }

    public Vector3 GetNextEmptyStarPos()
    {
        return starPanelView.GetNextEmptyStarPos();
    }
}
