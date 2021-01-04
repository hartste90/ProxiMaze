using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zipline : MonoBehaviour
{

    public Transform startLineAnchor;
    public Transform endLineAnchor;
    public Transform exitAnchor;

    LineRenderer lineRenderer;
    const float ZIPLINE_SPEED = 5f;
    public float ZIPLINE_TEXTURE_SPEED = 2f;
    bool isBeingUsed = false;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, startLineAnchor.position);
        lineRenderer.SetPosition(1, endLineAnchor.position);
    }

    private void Update()
    {
        lineRenderer.material.SetTextureOffset("_MainTex", Vector2.right * Time.time / ZIPLINE_TEXTURE_SPEED);

    }

    public Vector3 GetStartPos()
    {
        return startLineAnchor.position;
    }

    public Vector3 GetEndPos()
    {
        return endLineAnchor.position;
    }

    public Vector3 GetExitPos()
    {
        return exitAnchor.position;
    }

    public float GetTimeDuration()
    {
        float distance = Vector3.Distance(GetStartPos(), GetEndPos());
        return distance / ZIPLINE_SPEED;
    }

    public void StartZiplineUse()
    {
        isBeingUsed = true;
    }

    public void EndZiplineUse()
    {
        isBeingUsed = false;
    }

    public bool IsBeingUsed()
    {
        return isBeingUsed;
    }

    
}
