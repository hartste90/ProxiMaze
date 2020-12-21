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

    public void SetLevelText(string levelSet)
    {
        levelLabel.text = levelSet;
    }

    public void ToggleTrialCompletePanel()
    {
        trialCompletePanel.ToggleVisibility();
    }
}
