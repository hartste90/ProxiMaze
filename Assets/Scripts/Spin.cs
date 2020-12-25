using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spin : MonoBehaviour
{
    public float loopDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(Vector3.up * 360f, loopDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }
}
