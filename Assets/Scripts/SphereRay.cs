using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SphereRay : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float rayDistance = 3f;
    [SerializeField] private GameObject aimPoint;
    [SerializeField] private GameObject targetObject;

    public float dwellTime = 2f; // Dwell time in seconds
    private bool isDwelling = false; // Flag to track if dwelling is in progress
    private float dwellTimer = 0f; // Timer for tracking dwell time

    private GameObject pointedObject;

    public Tasks tasks;

    public static bool dwellFinished = false;
    public static bool dwellTargetFinished = true;

    //public static int entriesCounter = 0;
    //public static List<int> entriesList = new List<int> { };

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "CenterEyeAnchor")
        {
            lineRenderer.SetPosition(0, transform.position - new Vector3(0f, 0.5f, 0f));
            lineRenderer.SetPosition(1, transform.position - new Vector3(0f, 0.5f, 0f) + transform.forward * rayDistance);
            Debug.Log("It's into CenterEyeAnchor");
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * rayDistance);
        }
        

        //Delete from hereon to go back to the latest update
        // Perform raycast to detect object interaction
        //Vector3.Distance(transform.position, transform.position + transform.forward * rayDistance)
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (transform.forward * rayDistance).normalized, out hit, rayDistance))
        {
            pointedObject = hit.collider.gameObject;
            Data_Exp3.pointedObjectName = pointedObject.name;
            // If the ray hits an object
            Debug.Log("Hit object: " + pointedObject.name);

            if (pointedObject.name == "CalibrationObject")
            {

                if (tasks.runningTask)
                {
                    // Start dwelling
                    if (!isDwelling)
                    {
                        isDwelling = true;
                        dwellTimer = 0f;
                        Debug.Log("Dwell Timer started");
                    }
                    else
                    {
                        // Update dwell timer
                        dwellTimer += Time.deltaTime;

                        // Check if dwell time is reached
                        if (dwellTimer >= dwellTime + 1)
                        {
                            dwellFinished = true;
                            Debug.Log("Dwell Timer finished. Calibration Ready");

                            isDwelling = false;
                            dwellTimer = 0f;
                        }
                    }
                }
                else
                {
                    dwellFinished = false;
                }
            }
            

            if (pointedObject.name == "TargetObject")
            {
                //GazeColorChange(targetObject);
                if (tasks.runningTask)
                {
                    // Start dwelling
                    if (!isDwelling)
                    {
                        isDwelling = true;
                        dwellTimer = 0f;
                        Debug.Log("Dwell Timer started target");
                    }
                    else
                    {
                        // Update dwell timer
                        dwellTimer += Time.deltaTime;

                        // Check if dwell time is reached
                        if (dwellTimer >= dwellTime + 1)
                        {
                            dwellTargetFinished = true;
                            Debug.Log("Dwell Timer finished. Calibration Ready target");

                            isDwelling = false;
                            dwellTimer = 0f;
                        }
                    }
                }
                else
                {
                    dwellTargetFinished = false;
                }
            }
            

            if (pointedObject.name == "TargetSphere")
            {
                EntriesCounter();
            }

            //if (pointedObject.name != "TargetObject")
            //{
            //    GazeColorOrig(targetObject);
            //}
        }
    }


    private void EntriesCounter()
    {
        Data_Exp1.entriesCounter++;
    }

    bool DwellTimer(GameObject gameObject)
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
            if (dwellTimer >= dwellTime)
            {

                // Reset dwelling state
                isDwelling = false;
                dwellTimer = 0f;
                return true;
            }

              
        }

        return false;
        
    }

    public void GazeColorChange(GameObject targetObject)
    {
        Renderer objRenderer;
        objRenderer = targetObject.GetComponent<Renderer>();
        objRenderer.material.color = Color.blue;


    }

    public void GazeColorOrig(GameObject targetObject)
    {
        Renderer objRenderer;
        objRenderer = targetObject.GetComponent<Renderer>();
        objRenderer.material.color = Color.red;
    }

}
