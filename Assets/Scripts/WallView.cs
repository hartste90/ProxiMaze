using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallView : MonoBehaviour
{
    public void SetMaterial(Material matSet)
    {
        GetComponent<MeshRenderer>().material = matSet;
    }
}
