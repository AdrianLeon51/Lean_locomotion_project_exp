using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStart : MonoBehaviour
{
    //public OVRCameraRig ovrCameraRig;

    private Vector3 originalPosition;
    private Quaternion originalRotation;


    public Tasks tasks;
    
    // Start is called before the first frame update
    void Awake()
    {
        Camera mainCamera = Camera.main;
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * Tasks.distanceToObject;

    }

    // Update is called once per frame
    void Update()
    {
        //if (CheckIdlePosition() && tasks.DwellTimer())
        if (CheckIdlePosition() && SphereRay.dwellFinished)
        {
            tasks.isCalibrated = true;
            tasks.runningTask = false;
            gameObject.SetActive(false);

        }
        else
        {
            tasks.runningTask = true;
        }
    }


    private void OnEnable()
    {
        //FaceCamera();
        //PositionInFrontOfCamera();
    }

    void FaceCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            // Calculate the direction from the object to the camera
            Vector3 directionToCamera = mainCamera.transform.position - transform.position;

            // Make the object's forward direction point towards the camera
            //transform.forward = -directionToCamera.normalized;

            // Make the front face of the cube (positive Z direction) point towards the camera
            transform.rotation = Quaternion.LookRotation(directionToCamera);

            // Make the object's forward direction point towards the camera
            //Quaternion lookRotation = Quaternion.LookRotation(-directionToCamera.normalized);

            // Apply the rotation to the object
            //transform.rotation = lookRotation;
        }
    }

    void PositionInFrontOfCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 correction = new Vector3(0f, -0.1f, 0f);
            // Set the position two meters in front of the camera
            transform.position = mainCamera.transform.position + correction + mainCamera.transform.forward * Tasks.distanceToObject;
        }
    }

    bool CheckIdlePosition()
    {
        if (SeatedLocomotion.forwardIdleActive == true && SeatedLocomotion.lateralIdleActive == true && SeatedLocomotion.rotationIdleActive == true)
        {
            //gameObject.transform.GetComponent<MeshRenderer>().material = "Object_NonIdle";
            gameObject.transform.GetComponent<Renderer>().material.color = Color.blue;
            return true;
        }
        else
        {
            gameObject.transform.GetComponent<Renderer>().material.color = Color.green;
            return false;
        }
        
    }
}
