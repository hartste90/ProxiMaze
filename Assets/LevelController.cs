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
    public ControllerInterpreter playerInterpreter;
    public List<ParticleSystem> celebrationParticlesList;

    public static void OnLevelComplete()
    {
        Instance.OnLevelCompleteImpl();
        float delayMax = 1f;
        foreach(ParticleSystem cannon in Instance.celebrationParticlesList)
        {
            cannon.startDelay = Random.Range(0f, delayMax);
            cannon.Play();
        }
    }

    public void OnLevelCompleteImpl()
    {
        DisablePlayerController();
        MoveCameraAbove();
        DanceWalls();
    }

    void DisablePlayerController()
    {
        playerInterpreter.Roll();
        playerController.enabled = false;
    }

    void MoveCameraAbove()
    {
        float delay = .5f;
        Camera.main.transform.SetParent(topViewAnchor);
        Camera.main.transform.DOLocalRotate(Vector3.zero, 1.9f).SetDelay(delay);
        Camera.main.transform.DOLocalMove(Vector3.zero, 2f).SetEase(Ease.OutQuad).SetDelay(delay);
        mazeCenterAnchor.DOBlendableLocalRotateBy(Vector3.up * 360, 10f, RotateMode.FastBeyond360).SetDelay(delay).SetLoops(-1).SetEase(Ease.Linear);
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
