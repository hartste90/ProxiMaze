using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMazeOperator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("Trigger hit: " + other.name);

        if (other.name == "Maze End")
        {
            Debug.LogFormat("Maze Complete");
            LevelController.OnMazeEndReached();
        }

        else if (other.tag == "Pickup")
        {
            Pickup pickup = other.GetComponentInParent<Pickup>();
            if (pickup.type == PickupType.star)
            {
                GameController.OnStarPickupCollected(transform.position);
            }
            pickup.DestroySelf();
        }
    }


}
