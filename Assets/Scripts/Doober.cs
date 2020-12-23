using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Doober : MonoBehaviour
{
    const float MIN_DURATION = 1f;
    const float MAX_DURATION = 1.5f;

    private int amount;
    private UnityAction<int> onCompleteCallback;



    public void Init(int amtSet, Vector2 origin, Vector2 destination, UnityAction<int> onCompleteCallbackSet)
    {
        amount = amtSet;
        onCompleteCallback = onCompleteCallbackSet;
        transform.position = origin;
        FlyTo(destination, Random.Range(MIN_DURATION, MAX_DURATION));
    }

    private void FlyTo(Vector3 targetPosSet, float durationSet)
    {
        Vector3[] waypointList = new Vector3[3];
        waypointList[0] = transform.position;
        waypointList[1] = GetRandomPointOutsidePositions(transform.position, targetPosSet);
        waypointList[2] = targetPosSet + Vector3.right * (UnityEngine.Random.Range(-1f, 1f));
        transform.DOPunchScale(new Vector3(.5f, .5f, 1), .5f);
        transform.DOPath(waypointList, durationSet, PathType.CatmullRom, PathMode.Sidescroller2D).SetEase(Ease.InSine).OnComplete(OnAnimationComplete);
    }

    private Vector3 GetRandomPointOutsidePositions(Vector3 pointA, Vector3 pointB)
    {
        float dist = Vector3.Distance(pointA, pointB);
        dist *= .5f;
        Vector3 pos = new Vector3(
            UnityEngine.Random.Range(pointA.x - dist / 1.5f, pointA.x + dist / 1.5f),
            UnityEngine.Random.Range(pointA.y, pointA.y + dist / 2),
            UnityEngine.Random.Range(pointA.z, pointB.z)
            );
        return pos;
    }

    private void OnAnimationComplete()
    {
        Destroy(gameObject);
        onCompleteCallback?.Invoke(amount);
    }
}