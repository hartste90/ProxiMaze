using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    star = 0
}

public class Pickup : MonoBehaviour
{
    public PickupType type;

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
