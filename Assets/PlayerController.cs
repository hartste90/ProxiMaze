using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public FirstPersonController firstPersonController;
    public ControllerInterpreter controllerInterpreter;
    public PlayerMazeOperator playerMazeOperator;

    const string PLAYER_ZIPLINE_TWEEN = "player_zipline_tween";
    const string PLAYER_ZIPLINE_DISMOUNT_TWEEN = "player_zipline_dismount_tween";
    const string PLAYER_ZIPLINE_ANIMATION_DISMOUNT_TWEEN = "player_zipline_animation_dismount_tween";


    public void RegisterCamera(Camera cam)
    {
        firstPersonController.RegisterCamera(cam);
    }

    public Quaternion GetOGLocalCameraRotation()
    {
        return firstPersonController.GetOGLocalCameraRotation();
    }

    public Vector3 GetOGCameraLocalPosition()
    {
        return firstPersonController.GetOGCameraLocalPosition();
    }

    public void BeginZipline(Zipline zipline)
    {
        Debug.Log("Beginzipline");
        const float ZIPLINE_DELAY = 0f;
        GameController.DisableJoystick();
        //position to start
        transform.position = zipline.GetStartPos() - Vector3.up * firstPersonController.GetCharacterHeight();
        zipline.StartZiplineUse();
        //calc distance to endpoint = find time to lerp
        float ziplineTimeDuration = zipline.GetTimeDuration();
        //lerp position to end point
        Vector3 endpoint = zipline.GetEndPos() - Vector3.up * firstPersonController.GetCharacterHeight();
        KillPlayerZiplineTweens();
        transform.DOMove(endpoint, ziplineTimeDuration)
            .SetDelay(ZIPLINE_DELAY)
            .SetEase(Ease.Linear)
            .SetId(PLAYER_ZIPLINE_TWEEN)
            .OnComplete(() =>
                {
                    OnZiplineComplete(zipline);
                });

        //animation
        controllerInterpreter.OnBeginZipline();
    }

    public void OnZiplineComplete(Zipline zipline)
    {
        
        float dismountDuration = .5f;
        Debug.Log("CompleteZipline");

        //player flip dismount
        DOTween.Kill(PLAYER_ZIPLINE_DISMOUNT_TWEEN);

        Sequence s = DOTween.Sequence();
        s.AppendCallback(() =>
        {
            controllerInterpreter.OnZiplineComplete();
        });
        s.AppendInterval(dismountDuration);
        s.SetId(PLAYER_ZIPLINE_DISMOUNT_TWEEN);
        s.Play();

        //player move to exit position
        AnimatePlayerDismount(zipline.GetExitPos(), dismountDuration, () =>
        {
            OnZiplineDismountComplete(zipline);
        });
        
    }

    private void AnimatePlayerDismount(Vector3 endPos, float duration, TweenCallback callback)
    {
        Debug.Log("Dismount Zipline");


        const float dismountHeightFactor = 1.5f;
        Vector3[] path = new Vector3[3];
        path[0] = transform.position;
        path[2] = endPos;
        Vector3 midpoint = Vector3.Lerp(transform.position, endPos, .5f);
        path[1] = new Vector3(midpoint.x, 1 + endPos.y * dismountHeightFactor, midpoint.z);
        DOTween.Kill(PLAYER_ZIPLINE_ANIMATION_DISMOUNT_TWEEN);
        transform.DOPath(path, duration, PathType.CatmullRom, PathMode.Full3D, resolution: 10, gizmoColor: Color.green)
            .SetEase(Ease.Linear)
            .SetId(PLAYER_ZIPLINE_ANIMATION_DISMOUNT_TWEEN)
            .OnComplete(callback);
    }

    public void KillPlayerZiplineTweens()
    {
        DOTween.Kill(PLAYER_ZIPLINE_TWEEN);
        DOTween.Kill(PLAYER_ZIPLINE_DISMOUNT_TWEEN);
        DOTween.Kill(PLAYER_ZIPLINE_ANIMATION_DISMOUNT_TWEEN);

    }

    public void OnZiplineDismountComplete(Zipline zipline)
    {
        controllerInterpreter.OnZiplineDismountComplete();
        zipline.EndZiplineUse();
        GameController.EnableJoystick();
    }
}
