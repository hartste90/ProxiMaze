﻿using System.Collections;
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
            LevelController.OnLevelComplete();
        }
    }
}
