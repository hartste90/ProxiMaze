using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrialController : MonoBehaviour
{
    public Transform playerStartAnchor;
    public Transform mazeCenterAnchor;
    public Transform mazeTopViewAnchor;
    public Transform wallParent;
    
    public ControllerInterpreter playerInterpreter;
    public List<ParticleSystem> celebrationParticlesList;

    private List<WallView> wallsList;

    private void Awake()
    {
        wallsList = new List<WallView>();
        foreach(Transform t in wallParent)
        {
            wallsList.Add(t.GetComponent<WallView>());
        }
    }

    public Vector3 GetPlayerStartPosition()
    {
        return playerStartAnchor.position;
    }

    public void SetPlayer(FirstPersonController playerSet)
    {
        GameController.GetPlayer().transform.position = GetPlayerStartPosition();
    }

    public void OnMazeEndReached()
    {
        DisablePlayerController();
        MoveCameraAbove();
        DanceWalls();
    }

    void DisablePlayerController()
    {
        GameController.GetPlayer().GetComponent<ControllerInterpreter>().Roll();
        GameController.GetPlayer().enabled = false;
    }

    void MoveCameraAbove()
    {
        float delay = .5f;
        Camera.main.transform.SetParent(mazeTopViewAnchor);
        Camera.main.transform.DOLocalRotate(Vector3.zero, 1.9f).SetDelay(delay);
        Camera.main.transform.DOLocalMove(Vector3.zero, 2f).SetEase(Ease.OutQuad).SetDelay(delay);
        mazeCenterAnchor.DOBlendableLocalRotateBy(Vector3.up * 360, 10f, RotateMode.FastBeyond360).SetDelay(delay).SetLoops(-1).SetEase(Ease.Linear);
    }

    void DanceWalls()
    {
        GameObject[] wallsList = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject obj in wallsList)
        {
            obj.GetComponent<ProxiRise>().TweenDance();
        }

    }

}
