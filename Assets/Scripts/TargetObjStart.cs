using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjStart : MonoBehaviour
{
    [SerializeField] private float enterTimeLimit = 2f;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private GameObject leftArrow;
    private float enterTimer = 0f;

    public Tasks tasks;
    private Vector3 initialPosition;


    public static List<int> positionsList = new List<int> {20, 20, 20, -20, -20, -20, 40, 40, 40, -40, -40, -40, 80, 80, 80, -80, -80, -80, 120, 120, 120, -120, -120, -120, 160, 160, 160, -160, -160, -160, 180, 180, 180 }; 
    // Start is called before the first frame update
    void Awake()
    {
        Camera mainCamera = Camera.main;
        initialPosition = mainCamera.transform.forward;
    }

    private void Update()
    {
        if (SphereRay.dwellTargetFinished && tasks.runningTask && tasks.isCalibrated)
        {
            
            tasks.isCalibrated = false;
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
        PositionInFrontOfCamera();
    }

    void PositionInFrontOfCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            int indexPositionList = Random.Range(0, positionsList.Count);
            int angle = positionsList[indexPositionList];
            positionsList.RemoveAt(indexPositionList);
            //Vector3 forward = mainCamera.transform.forward;
            if (angle > 0)
            {
                rightArrow.SetActive(true);
            }
            else if(angle < 0)
            {
                leftArrow.SetActive(true);
            }
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 direction = rotation * initialPosition;
            Vector3 correction = new Vector3(0, -0.1f, 0);
            Vector3 targetPosition = mainCamera.transform.position + correction + direction.normalized * Tasks.distanceToObject;
            transform.position = targetPosition;
            // Set the position two meters in front of the camera
            //transform.position = mainCamera.transform.position + mainCamera.transform.forward * Tasks.distanceToObject;

            
        }
    }


    
    private void OnCollisionStay(Collision collision)
    {
        enterTimer += Time.deltaTime;

        if (enterTimer >= enterTimeLimit)
        {
            gameObject.SetActive(false);
            tasks.runningTask = false;
            tasks.isCalibrated = false;

        }
    }
}
