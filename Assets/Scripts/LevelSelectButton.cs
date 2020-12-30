using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelSelectButton : MonoBehaviour
{
    public Button btn;
    public TextMeshProUGUI levelLabel;
    public Color unlockedTextColor;
    public Color lockedTextColor;
    public List<StarView> starList;
    public List<Image> lockedImageList;


    public void Init(int levelNumberSet, int numStars, bool isLocked, System.Action<int> onPressedCallbackSet)
    {
        //button callback
        if (isLocked)
        {
            btn.onClick.AddListener(PlayNegativeCTA);
        }
        else
        {
            btn.onClick.AddListener(() =>
            {
                onPressedCallbackSet?.Invoke(levelNumberSet);
            });
        }
        //text
        levelLabel.text = levelNumberSet.ToString();
        levelLabel.color = isLocked ? lockedTextColor : unlockedTextColor;
        //locked setup
        foreach (Image img in lockedImageList)
        {
            img.gameObject.SetActive(isLocked);
        }
        levelLabel.gameObject.SetActive(!isLocked);
        //stars
        for( int idx = 0; idx < starList.Count; idx++)
        {
            StarView star = starList[idx];
            if (!isLocked && idx <= numStars-1)
            {
                star.ShowFilled();
            }
            else
            {
                star.ShowEmpty();
            }
        }
    }

    void PlayNegativeCTA()
    {
        transform.DOShakePosition(.5f);
    }
}
