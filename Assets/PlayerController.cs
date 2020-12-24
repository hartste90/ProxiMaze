using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public FirstPersonController firstPersonController;
    public ControllerInterpreter controllerInterpreter;
    public PlayerMazeOperator playerMazeOperator;


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
        const float ZIPLINE_DELAY = 0f;
        GameController.DisableJoystick();
        //position to start
        transform.position = zipline.GetStartPos() - Vector3.up * firstPersonController.GetCharacterHeight();
        Debug.Break();
        zipline.StartZiplineUse();
        //calc distance to endpoint = find time to lerp
        float ziplineTimeDuration = zipline.GetTimeDuration();
        //lerp position to end point
        Vector3 endpoint = zipline.GetEndPos() - Vector3.up * firstPersonController.GetCharacterHeight();
        transform.DOMove(endpoint, ziplineTimeDuration)
            .SetDelay(ZIPLINE_DELAY)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
                {
                    OnZiplineComplete(zipline);
                });
    }

    public void OnZiplineComplete(Zipline zipline)
    {
        transform.position = zipline.GetExitPos();
        zipline.EndZiplineUse();
        GameController.EnableJoystick();
    }
}
