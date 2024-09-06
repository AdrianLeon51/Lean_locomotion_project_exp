using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollissions : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Obstacle trigger entered");
        if (other.gameObject.name == "[BuildingBlock] Camera Rig")
        {
            Debug.Log("Player has collided with an obstacle");
            Data_Exp3.numberNTryCollissions++;
            Data_Exp3.numberSceneCollissions++;
        }
        
    }
}
