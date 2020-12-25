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

    public Transform fireworksParent;

    private List<WallView> wallsList;
    private Transform mazeEnd;


    public void Init(Material matSet)
    {
        wallsList = new List<WallView>();
        foreach (Transform t in wallParent)
        {
            WallView wall = t.GetComponent<WallView>();
            wallsList.Add(wall);
            wall.SetMaterial(matSet);

        }

        mazeEnd = fireworksParent;
    }

    public void BeginTrial()
    {
        GameController.GetPlayer().enabled = true;
        mazeEnd = fireworksParent;
        ReturnCameraToPlayer();
    }

    public Vector3 GetPlayerStartPosition()
    {
        return playerStartAnchor.position;
    }

    public void SetPlayer(PlayerController playerSet)
    {
        GameController.GetPlayer().GetComponentInChildren<TrailRenderer>().enabled = false;
        GameController.GetPlayer().transform.position = GetPlayerStartPosition();
        GameController.GetPlayer().GetComponentInChildren<TrailRenderer>().enabled = true;
    }

    public void OnMazeEndReached()
    {
        DisablePlayerController();
        MoveCameraAbove();
        DanceWalls();
        PlayFireworks();

    }

    void PlayFireworks()
    {
        foreach(Transform t in fireworksParent)
        {
            t.GetComponent<ParticleSystem>().Play();
        }
    }

    void DisablePlayerController()
    {
        GameController.GetPlayer().GetComponent<ControllerInterpreter>().Roll();
        GameController.GetPlayer().enabled = false;
        GameController.DisableJoystick();
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

    void DestroyWalls()
    {
        GameObject[] wallsList = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject obj in wallsList)
        {
            obj.GetComponent<ProxiRise>().TweenToHidden();
        }
    }

    public void EndTrial()
    {
        Sequence s = DOTween.Sequence();
        s.AppendCallback(DestroyWalls);
        s.AppendInterval(1f);
        s.OnComplete(() =>
            {
                DetachCameraFromTrial();
                DOTween.KillAll();
                Destroy(gameObject);
            });
        s.Play();
    }

    void DetachCameraFromTrial()
    {
        Camera.main.transform.SetParent(null);
    }

    void ReturnCameraToPlayer()
    {
        Camera.main.transform.SetParent(GameController.GetPlayer().transform);
        //using the x,z from rotation
        Quaternion ogRotation = GameController.GetPlayer().GetOGLocalCameraRotation();
        Vector3 ogPosition = GameController.GetPlayer().GetOGCameraLocalPosition();
        //using the y from position
        float height = GameController.GetPlayer().GetOGCameraLocalPosition().y;
        //create the new position (x, z) ==> From direction from player to target * -12
        Vector3 dir = GameController.GetPlayer().transform.position - fireworksParent.position;
        dir.Normalize();
        Vector3 newPos = ogPosition;
        MoveCameraToPosition(newPos);

        //create new rotation with y pointed at target (may not be correct if we do LOOKAT from original position)
        //Sequence s = DOTween.Sequence();
        //s.Append()

        Camera.main.transform.DOLocalRotate(ogRotation.eulerAngles, 1f);
        //FocusCameraOnTarget();
        //Vector3 ogLocalPos = GameController.GetPlayer().GetOGCameraLocalPosition();
        //ogLocalPos = new Vector3(ogLocalPos.x, ogLocalPos.y, -12);
        //MoveCameraBehindPlayer(ogLocalPos);
        //face camera to level target with player between camera and target

    }

    void FocusCameraOnTarget()
    {
        Camera.main.transform.DOLookAt(mazeEnd.position, 1f, AxisConstraint.None);
    }

    void MoveCameraToPosition(Vector3 targetPos)
    {
        Camera.main.transform.DOLocalMove(targetPos, 1f);
    }

}
