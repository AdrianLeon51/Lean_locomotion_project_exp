using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Exp3 : MonoBehaviour
{
    public Tasks_Exp_3 tasksExp3;
    public OVRCameraRig ovrCameraRig;

    public Color originalColor;
    public Color collisionColor;
    private Renderer sphereRenderer;

    private int targetEntries = 0;

    private void Start()
    {
        // Get the Renderer component from the sphere
        sphereRenderer = GetComponent<Renderer>();
        // Set the sphere's original color
        sphereRenderer.material.color = originalColor;

        
        
    }



    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision entered");
        Debug.Log("Collision name: " + other.gameObject.name);
        Debug.Log("Player name: " + ovrCameraRig.name);

        // Check if the other collider is part of a LineRenderer
        if (other.GetComponent<LineRenderer>() != null)
        {
            // Change the sphere's color to the collision color
            sphereRenderer.material.color = collisionColor;
        }

        if (other.gameObject.name == ovrCameraRig.name && tasksExp3.targetReached == false)
        {
            //Data_Exp1.entriesList.Add(Data_Exp1.entriesCounter);
            //Data_Exp1.entriesCounter = 0;
            Debug.Log("OnTriggerEntered");

            targetEntries++;
            Data_Exp3.numberNTry = targetEntries;

            StartCoroutine(TargetReachedCoroutine());
            //tasksExp3.TargetReached();
            
            

        }
        
    }


    IEnumerator TargetReachedCoroutine()
    {
        tasksExp3.TargetReached();
        
        yield return new WaitForEndOfFrame();
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the other collider is part of a LineRenderer
        if (other.GetComponent<LineRenderer>() != null)
        {
            // Change the sphere's color back to the original color
            sphereRenderer.material.color = originalColor;
        }
    }


}
