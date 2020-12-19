using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelController : MonoBehaviour
{
    #region singleton
    private static LevelController instance;
    public static LevelController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<LevelController>();
                if (instance == null)
                {
                    Debug.LogError("No LevelController was found in this scene. Make sure you place one in the scene.");
                    return instance;
                }
            }
            return instance;
        }
    }
    #endregion

    public Transform topViewAnchor;
    public Transform mazeCenterAnchor;
    public FirstPersonController playerController;

    public static void OnLevelComplete()
    {
        Instance.OnLevelCompleteImpl();
    }

    public void OnLevelCompleteImpl()
    {
        DisablePlayerController();
        MoveCameraAbove();
        DanceWalls();
    }

    void DisablePlayerController()
    {
        playerController.enabled = false;
    }

    void MoveCameraAbove()
    {
        Camera.main.transform.SetParent(null);
        //Camera.main.transform.DOLookAt(mazeCenterAnchor.position, .75f);
        Camera.main.transform.DORotate(new Vector3(90f, 0f, 0f), 1.9f);

        Camera.main.transform.DOMove(topViewAnchor.position, 2f).SetEase(Ease.OutQuad);
    }

    void DanceWalls()
    {
        GameObject[] wallsList = GameObject.FindGameObjectsWithTag("Wall");
        foreach(GameObject obj in wallsList)
        {
            obj.GetComponent<ProxiRise>().TweenDance();
        }

    }
}
