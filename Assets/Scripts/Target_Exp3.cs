using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Exp3 : MonoBehaviour
{
    public Tasks_Exp_3 tasksExp3;
    public OVRCameraRig ovrCameraRig;
    
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision entered");
        
        if (other.gameObject.name == ovrCameraRig.name)
        {
            Data_Exp1.entriesList.Add(Data_Exp1.entriesCounter);
            Data_Exp1.entriesCounter = 0;
            tasksExp3.TargetReached();

        }
        
    }


}
