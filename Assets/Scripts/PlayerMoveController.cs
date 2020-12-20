using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    //public float XRotSpeed = 175;
    //public float YRotSpeed = 175;
    //public float Speed = 1;

    //Vector3 LastPosition;
    //Vector3 CurrentPosition;
    //bool AllowMovement;
    //private Camera Camera;



    ////private void Start()
    ////{
    ////    Camera = GameManager.Instance.MainCamera;
    ////}

    //public void EnableControl()
    //{
    //    StartCoroutine(EnableMovement());
    //}

    //public void DisableControl()
    //{
    //    Speed = 0;
    //    AllowMovement = false;
    //}

    //IEnumerator EnableMovement()
    //{
    //    Speed = 10f;// GameManager.Instance.DefaultSpeed;
    //    yield return new WaitForSeconds(.5f);

    //    AllowMovement = true;
    //    LastPosition = CurrentPosition;
    //    CurrentPosition = Input.mousePosition;
    //}


    //void Update()
    //{

    //    //Move forward
    //    transform.Translate(Vector3.forward * (Time.deltaTime * Speed), Space.Self);

    //    //Enabling collisions and not messing up movement
    //    GetComponent<Rigidbody>().velocity = Vector3.zero;

    //    //Turn Controls

    //    if (!AllowMovement)
    //        return;

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        LastPosition = CurrentPosition;
    //        CurrentPosition = Input.mousePosition;
    //    }


    //    if (Input.GetMouseButton(0))
    //    {

    //        LastPosition = CurrentPosition;
    //        CurrentPosition = Input.mousePosition;
    //        Vector2 deltaPosition = CurrentPosition - LastPosition;

    //        //Vector2 newDeltaPosition = new Vector2((float)deltaPosition.x / (float)Screen.width, (float)deltaPosition.y / (float)Screen.height);
    //        Vector2 newDeltaPosition = Camera.ScreenToViewportPoint(new Vector2((float)deltaPosition.x, (float)deltaPosition.y));

    //        if (deltaPosition.magnitude >= .01f)
    //        {

    //            Quaternion yaw = Quaternion.Euler(0f, newDeltaPosition.x * XRotSpeed, 0f);
    //            transform.rotation = yaw * transform.rotation;

    //            Quaternion pitch = Quaternion.Euler(-newDeltaPosition.y * YRotSpeed, 0f, 0f);
    //            transform.rotation = transform.rotation * pitch;

    //        }
    //    }

    //    var currentRot = transform.localEulerAngles;
    //    currentRot.x = ClampAngle(currentRot.x, -40, 40);
    //    currentRot.z = 0;
    //    transform.localEulerAngles = currentRot;


    //}

    //float ClampAngle(float angle, float from, float to)
    //{
    //    if (angle < 0f)
    //    {
    //        angle = 360 + angle;
    //    }
    //    if (angle > 180f)
    //    {
    //        return Mathf.Max(angle, 360 + from);
    //    }
    //    return Mathf.Min(angle, to);
    //}

}
