using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasks : MonoBehaviour
{
    public static bool Task1 = false;
    public static bool Task2 = false;
    public static bool Task3 = false;

    [SerializeField] private GameObject calObject;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private SeatedLocomotion seatedLocomotion;

    public bool isCalibrated = false;
    public bool runningTask = false;

    private bool isDwelling = false; // Flag to track if dwelling is in progress
    private float dwellTimer = 0f; // Timer for tracking dwell time
    public float dwellTime = 2f; // Dwell time in seconds

    public static float distanceToObject = 3f;
    // Start is called before the first frame update
    void Start()
    {
        seatedLocomotion.activateForward = false;
        seatedLocomotion.activateBackward = false;
        seatedLocomotion.activateLateral = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SphereRay.dwellTargetFinished)
        {
            SphereRay.dwellTargetFinished = false;
            CalibrationBox();
        }
        else if (SphereRay.dwellFinished)
        {
            SphereRay.dwellFinished = false;
            TargetBox();
        }
    }

    void Extras()
    {
        if (isCalibrated == false && runningTask == false && SphereRay.dwellTargetFinished)
        {
            CalibrationBox();
        }
        else if (isCalibrated == true && runningTask == false && SphereRay.dwellFinished)
        {
            TargetBox();
        }
    }

    private void CalibrationBox()
    {
        targetObject.SetActive(false);
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
        calObject.SetActive(true);
        SphereRay.dwellTargetFinished = false;
        runningTask = true;
    }

    private void TargetBox()
    {
        calObject.SetActive(false);
        targetObject.SetActive(true);
        runningTask = true;
    }

    public bool DwellTimer()
    {
        
        
        // Start dwelling
        if (!isDwelling)
        {
            isDwelling = true;
            dwellTimer = 0f;
        }
        else
        {
            // Update dwell timer
            dwellTimer += Time.deltaTime;

            // Check if dwell time is reached
            if (dwellTimer >= dwellTime + 1)
            {
                
                isDwelling = false;
                dwellTimer = 0f;
                return true;
            }
        }

        return false;
        
    }

    

}
