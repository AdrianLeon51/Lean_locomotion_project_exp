using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Tasks_Exp_3 : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private GameObject obstaclesObject;

    [SerializeField] private GameObject centerAnchorObject;
    [SerializeField] private GameObject leftEyeAnchorObject;

    [SerializeField] private GameObject waypoint1L;
    [SerializeField] private GameObject waypoint2L;
    [SerializeField] private GameObject waypoint3L;
    [SerializeField] private GameObject waypoint1R;
    [SerializeField] private GameObject waypoint2R;
    [SerializeField] private GameObject waypoint3R;

    [SerializeField] private GameObject obstacleCenterObject;
    [SerializeField] private GameObject obstacleOpnCenterObject;
    [SerializeField] private GameObject obstacleLeftObject;
    [SerializeField] private GameObject obstacleRightObject;

    [SerializeField] private GameObject canvasCenterObject;
    [SerializeField] private GameObject canvasOpenCenterObject;
    [SerializeField] private GameObject canvasLeftObject;
    [SerializeField] private GameObject canvasRightObject;

    [SerializeField] private float disappearLimit = 0.2f;

    [SerializeField] private OVRCameraRig ovrCamerarig;

    [SerializeField] private SeatedLocomotion seatedLocomotion;

    [SerializeField] private SceneChange sceneChange;

    public WriteDataPerScene writeDataPerScene;

    private bool coroutinesFinished = false;


    private List<float> orderList = new List<float> { 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3 };
    private List<float> targetList = new List<float> { 3, 6, 9 };
    private List<float> obstacleList = new List<float> { 1.5f, 3f, 4.5f };
    //private List<(int, int)> obstacleVectorList = new List<(int, int)> { (1, 0), (2,0), (3,0) };
    private List<(int, int)> obstacleVectorList = new List<(int, int)> { 
    //    (1, 0), (1, 0), (1, 0), 
        (1, 1), (1, 1), (1, 1), (1, 2), (1, 2), (1, 2),
    //    (2, 0), (2, 0), (2, 0), 
        (2, 1), (2, 1), (2, 1), (2, 2), (2, 2), (2, 2), 
    //    (3, 0), (3, 0), (3, 0), 
        (3, 1), (3, 1), (3, 1), (3, 2), (3, 2), (3, 2)};
    private List<(int, int)> dataPositionOrder = new List<(int, int)> { };

    public int initObsVectorList;

    private int counterObsOpenCenter = 0;
    private int counterObsCenter = 0;
    private int counterObsLeft = 0;
    private int counterObsRight = 0;
    private int counterShorDistance = 0;
    private int counterMidDistance = 0;
    private int counterLongDistance = 0;

    private int counterStates = 0;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private float elapsedTime = 0;
    private bool wasAccessed = false;
    public bool targetReached = true;

    public bool writing = false;

    private float checkInterval = 0.1f;

    private List<(float,Vector3)> playerPositions = new List<(float,Vector3)>(); // List to store player positions
    private float closestDistanceWP1L = float.MaxValue; // Initialize with maximum possible value
    private float timeAtClosestDistanceWP1L;
    private float closestDistanceWP2L = float.MaxValue; // Initialize with maximum possible value
    private float timeAtClosestDistanceWP2L;
    private float closestDistanceWP3L = float.MaxValue; // Initialize with maximum possible value
    private float timeAtClosestDistanceWP3L;
    private float closestDistanceWP1R = float.MaxValue; // Initialize with maximum possible value
    private float timeAtClosestDistanceWP1R;
    private float closestDistanceWP2R = float.MaxValue; // Initialize with maximum possible value
    private float timeAtClosestDistanceWP2R;
    private float closestDistanceWP3R = float.MaxValue; // Initialize with maximum possible value
    private float timeAtClosestDistanceWP3R;

    // Start is called before the first frame update
    void Start()
    {
        Camera mainCamera = Camera.main;
        

        // Start tracking the player's path
        InvokeRepeating("TrackPlayerPosition", 0f, checkInterval);
        InvokeRepeating("TrackPlayerHeadsetData", 0f, checkInterval);
        //InvokeRepeating("RawTrackPlayerPosition", 0f, checkInterval);

        writing = false;

        initObsVectorList = obstacleVectorList.Count;

        originalPosition = ovrCamerarig.transform.position;
        originalRotation = ovrCamerarig.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        Debug.Log("List Variables Count: " + obstacleVectorList.Count);
        Debug.Log("Number of N Try: " + Data_Exp3.numberNTry);
        Debug.Log("Total Number of N Try: " + Data_Exp3.numberFinalNTry);
        Debug.Log("Raw Vector Direction Count: " + Data_Exp3.rawPositionVector.Count);

        //RawTrackPlayerPosition();
        //Data_Exp3.rawPositionVector.Add((Data_Exp3.numberNTry, Time.time, ovrCamerarig.transform.position));

        //CalculateVectors();
        if (obstacleVectorList.Count == 0 && wasAccessed == false)
        {
            //PrintTupleElementInt("Entries List", Data_Exp1.entriesList);
            //PrintTupleElementsInt("Order of appearance", Data_Exp1.dataPositionOrder);
            //PrintTupleElementsFloat("Angle Head vs. Translation", Data_Exp1.dataHeadVsTranslationAngle);

            //WriteTupleListToCSV("Order of appearance", "order_file", Data_Exp1.dataPositionOrder);

            wasAccessed = true;
        }

        //Set active for canvas dissappearing after move.
        if (Mathf.Abs(ovrCamerarig.transform.position.x) >= disappearLimit && targetReached == true)
        {
            
            OffAfterMove();
        }

        if (Data_Exp3.numberNTry > initObsVectorList && writing == false)
        {
            writing = true;
            Debug.Log("N Try + Vector Obstacle: " + Data_Exp3.numberNTry + " " + initObsVectorList);
            Data_Exp3.numberFinalNTry = Data_Exp3.numberNTry;
            Debug.Log("Application Quit");
            Debug.Log("Raw Vector Direction Count: " + Data_Exp3.rawDirectionVectors.Count);
            //WriteAndApplicationQuit();
            sceneChange.LoadNextScene();
            
        }


        //if (coroutinesFinished == true)
        //{
        //    Debug.Log("Coroutines finished");
        // save any game data here
        //    #if UNITY_EDITOR

        //        UnityEditor.EditorApplication.isPlaying = false;
        //    #else
        //        Application.Quit();
        //    #endif
        //}

    }


    public void WriteAndApplicationQuit()
    {
        //CancelInvoke();
        //Data_Exp3.participantNumber = Data_Exp3.participantNumber;
        Data_Exp3.numberVariableOrder = 1;
        //int totalNTry = 3;
        Data_Exp3.sceneName = SceneManager.GetActiveScene().name;
        //float timeStart = Data_Exp3.timeSceneStart;
        Data_Exp3.timeScene = Time.time;
        Data_Exp3.timeExperiment = Time.time - Data_Exp3.timeSceneStart;
        Data_Exp3.distanceScene = 1000f;
        //Data_Exp3.numberSceneCollissions;
        int numReentryGaze = Data_Exp3.numberSceneReentryGaze;
        StartCoroutine(ApplicationQuitCoroutine());
        //writeDataPerScene.DataScene();
        
    }

    IEnumerator ApplicationQuitCoroutine()
    {
        writeDataPerScene.DataScene();

        for (int i = 0; i < Data_Exp3.numberNTryList.Count; i++)
        {
            Debug.Log("Count number n try list: " + Data_Exp3.numberNTryList.Count + " i " + i);
            writeDataPerScene.DataNTry(i);
        }
        for (int i = 0; i < Data_Exp3.rawPositionVector.Count; i++)
        {
            writeDataPerScene.DataDirections(i);
            writeDataPerScene.DataHeadset(i);
        }
        
        // Wait until the end of the frame to ensure the data is written
        yield return new WaitForEndOfFrame();

        //yield return new WaitForSeconds(3);

        coroutinesFinished = true;
    }

    private void RepositionTarget()
    {
        int indexPositionList = Random.Range(0, targetList.Count);
        float positionOrder = targetList[indexPositionList];

        Vector3 targetObjectPosition = targetObject.transform.position;
        Vector3 obstaclesObjectPosition = obstaclesObject.transform.position;
        Vector3 canvasPosition = canvasObject.transform.position;

        if (positionOrder == 3 && counterShorDistance < 12)
        {
            targetObjectPosition.z = 3;
            obstaclesObjectPosition.z = 1.5f;
            canvasPosition.z = 1.5f;
            targetObject.SetActive(true);
            counterShorDistance++;
            
        }
        else if (positionOrder == 6 && counterMidDistance < 12)
        {
            targetObjectPosition.z = 6;
            obstaclesObjectPosition.z = 3;
            canvasPosition.z = 3;
            targetObject.SetActive(true);
            counterMidDistance++;
        }
        else if (positionOrder == 9 && counterLongDistance < 12)
        {
            targetObjectPosition.z = 3;
            obstaclesObjectPosition.z = 1.5f;
            canvasPosition.z = 1.5f;
            targetObject.SetActive(true);
            counterLongDistance++;
        }
    }

    private void TrackPlayerHeadsetData()
    {
        Debug.Log("Track Player Position is called");
        // Record the current position of the player
        float timeFrame = Time.time;
        Data_Exp3.rawLocalHeadPosList.Add((Data_Exp3.numberNTry, timeFrame, centerAnchorObject.transform.localPosition));
        Data_Exp3.rawLocalHeadRotList.Add((Data_Exp3.numberNTry, timeFrame, centerAnchorObject.transform.localRotation));
        Data_Exp3.rawGazeObjNameList.Add((Data_Exp3.numberNTry, timeFrame, Data_Exp3.pointedObjectName));
        Data_Exp3.rawLocalBodyPosList.Add((Data_Exp3.numberNTry, timeFrame, ovrCamerarig.transform.localPosition));
        Data_Exp3.rawLocalBodyRotList.Add((Data_Exp3.numberNTry, timeFrame, ovrCamerarig.transform.localRotation));
        
    }

        private void TrackPlayerPosition()
    {
        Debug.Log("Track Player Position is called");
        // Record the current position of the player
        float timeFrame = Time.time;
        Data_Exp3.rawPositionVector.Add((Data_Exp3.numberNTry, timeFrame, ovrCamerarig.transform.position));
        Data_Exp3.rawTranslationVector.Add((Data_Exp3.numberNTry, timeFrame, centerAnchorObject.transform.localPosition));
        Data_Exp3.rawHeadDirVector.Add((Data_Exp3.numberNTry, timeFrame, centerAnchorObject.transform.forward));
        Data_Exp3.rawGazeHeadDirVector.Add((Data_Exp3.numberNTry, timeFrame, leftEyeAnchorObject.transform.forward));
        Data_Exp3.rawGazeHeadRotVector.Add((Data_Exp3.numberNTry, timeFrame, leftEyeAnchorObject.transform.localRotation));
        //Debug.Log("head local translation: " + centerAnchorObject.transform.forward);
        Debug.Log("Track Plater vector added");
        //playerPositions.Add(ovrCamerarig.transform.position);

        // Check distance to the target object from the current position
        float distanceToTargetWP1L = Vector3.Distance(ovrCamerarig.transform.position, waypoint1L.transform.position);
        float distanceToTargetWP2L = Vector3.Distance(ovrCamerarig.transform.position, waypoint2L.transform.position);
        float distanceToTargetWP3L = Vector3.Distance(ovrCamerarig.transform.position, waypoint3L.transform.position);
        float distanceToTargetWP1R = Vector3.Distance(ovrCamerarig.transform.position, waypoint1R.transform.position);
        float distanceToTargetWP2R = Vector3.Distance(ovrCamerarig.transform.position, waypoint2R.transform.position);
        float distanceToTargetWP3R = Vector3.Distance(ovrCamerarig.transform.position, waypoint3R.transform.position);

        // Update the closest distance if this one is smaller
        if (distanceToTargetWP1L < closestDistanceWP1L)
        {
            timeAtClosestDistanceWP1L = Time.time;
            closestDistanceWP1L = distanceToTargetWP1L;
            Data_Exp3.distanceWaypoint1L = distanceToTargetWP1L;

        }
        if (distanceToTargetWP2L < closestDistanceWP2L)
        {
            timeAtClosestDistanceWP2L = Time.time;
            closestDistanceWP2L = distanceToTargetWP2L;
            Data_Exp3.distanceWaypoint2L = distanceToTargetWP2L;
        }
        if (distanceToTargetWP3L < closestDistanceWP3L)
        {
            timeAtClosestDistanceWP3L = Time.time;
            closestDistanceWP3L = distanceToTargetWP3L;
            Data_Exp3.distanceWaypoint3L = distanceToTargetWP3L;
        }
        if (distanceToTargetWP1R < closestDistanceWP1R)
        {
            timeAtClosestDistanceWP1R = Time.time;
            closestDistanceWP1R = distanceToTargetWP1R;
            Data_Exp3.distanceWaypoint1R = distanceToTargetWP1R;
        }
        if (distanceToTargetWP2R < closestDistanceWP2R)
        {
            timeAtClosestDistanceWP2R = Time.time;
            closestDistanceWP2R = distanceToTargetWP2R;
            Data_Exp3.distanceWaypoint2R = distanceToTargetWP2R;
        }
        if (distanceToTargetWP3R < closestDistanceWP3R)
        {
            timeAtClosestDistanceWP3R = Time.time;
            closestDistanceWP3R = distanceToTargetWP3R;
            Data_Exp3.distanceWaypoint3R = distanceToTargetWP3R;
        }
    }

    public void TargetReached()
    {

        ovrCamerarig.transform.position = originalPosition;
        ovrCamerarig.transform.rotation = originalRotation;

        StartCoroutine(PauseCoroutine());

        if (obstacleCenterObject.activeInHierarchy)
        {
            Data_Exp3.currentObsName = obstacleCenterObject.name;
        }
        else if (obstacleLeftObject.activeInHierarchy)
        {
            Data_Exp3.currentObsName = obstacleLeftObject.name;
        }
        else if (obstacleRightObject.activeInHierarchy)
        {
            Data_Exp3.currentObsName = obstacleRightObject.name;
        }

        obstacleCenterObject.SetActive(false);
        obstacleOpnCenterObject.SetActive(false);
        obstacleLeftObject.SetActive(false);
        obstacleRightObject.SetActive(false);

        canvasCenterObject.SetActive(false);
        canvasOpenCenterObject.SetActive(false);
        canvasLeftObject.SetActive(false);
        canvasRightObject.SetActive(false);


        Data_Exp3.currentObsNameList.Add(Data_Exp3.currentObsName);
        Data_Exp3.currentObsName = "";

        Data_Exp3.numberNTryList.Add(Data_Exp3.numberNTry);

        Data_Exp3.timeNTryEnd = Time.time;
        Data_Exp3.timeNTryStartList.Add(Data_Exp3.timeNTryStart);
        Data_Exp3.timeNTryEndList.Add(Data_Exp3.timeNTryEnd);

        Data_Exp3.distanceNTry = CalculateTraveledDistance();
        Data_Exp3.distanceNTryList.Add(Data_Exp3.distanceNTry);
        Data_Exp3.distanceNTry = 0;

        Data_Exp3.numberNTryCollissionsList.Add(Data_Exp3.numberNTryCollissions);
        Data_Exp3.numberNTryCollissions = 0;

        Data_Exp3.numberNTryReentryGazeList.Add(Data_Exp3.numberNTryReentryGaze);
        Data_Exp3.numberNTryReentryGaze = 0;

        Data_Exp3.distanceWaypoint1LList.Add((timeAtClosestDistanceWP1L, closestDistanceWP1L));
        Data_Exp3.distanceWaypoint2LList.Add((timeAtClosestDistanceWP2L, closestDistanceWP2L));
        Data_Exp3.distanceWaypoint3LList.Add((timeAtClosestDistanceWP3L, closestDistanceWP3L));
        Data_Exp3.distanceWaypoint1RList.Add((timeAtClosestDistanceWP1R, closestDistanceWP1R));
        Data_Exp3.distanceWaypoint2RList.Add((timeAtClosestDistanceWP2R, closestDistanceWP2R));
        Data_Exp3.distanceWaypoint3RList.Add((timeAtClosestDistanceWP3R, closestDistanceWP3R));
        timeAtClosestDistanceWP1L = 0;
        closestDistanceWP1L = float.MaxValue;
        timeAtClosestDistanceWP2L = 0;
        closestDistanceWP2L = float.MaxValue;
        timeAtClosestDistanceWP3L = 0;
        closestDistanceWP3L = float.MaxValue;
        timeAtClosestDistanceWP1R = 0;
        closestDistanceWP1R = float.MaxValue;
        timeAtClosestDistanceWP2R = 0;
        closestDistanceWP2R = float.MaxValue;
        timeAtClosestDistanceWP3R = 0;
        closestDistanceWP3R = float.MaxValue;

        counterStates++;
        Debug.Log("Counter states: " + counterStates);

        targetObject.SetActive(false);

        RepositionObstacles();

        targetReached = true;

    }

    private void OffAfterMove()
    {
        targetReached = false;
        
        if (Mathf.Abs(ovrCamerarig.transform.position.x) >= disappearLimit)
        {
            Data_Exp3.timeNTryStart = Time.time;

            if (Data_Exp3.numberNTry == 0)
            {
                Data_Exp3.timeSceneStart = Data_Exp3.timeNTryStart;
            }

            canvasCenterObject.SetActive(false);
            canvasOpenCenterObject.SetActive(false);
            canvasLeftObject.SetActive(false);
            canvasRightObject.SetActive(false);
        }
    }

    private void RepositionObstacles()
    {
        int indexObstacleList = Random.Range(0, obstacleVectorList.Count);
        (int, int) obstacleVector = obstacleVectorList[indexObstacleList];
        Debug.Log("counter states obs vector: " + obstacleVector);
        obstacleVectorList.RemoveAt(indexObstacleList);
        dataPositionOrder.Add(obstacleVector);


        Vector3 objRightPos = obstacleRightObject.transform.position;
        Vector3 objLeftPos = obstacleLeftObject.transform.position;
        Vector3 wp1LeftPos = waypoint1L.transform.position;
        Vector3 wp2LeftPos = waypoint2L.transform.position;
        Vector3 wp3LeftPos = waypoint3L.transform.position;
        Vector3 wp1RightPos = waypoint1R.transform.position;
        Vector3 wp2RightPos = waypoint2R.transform.position;
        Vector3 wp3RightPos = waypoint3R.transform.position;
        //obstacleCenterObject.transform.localScale = targetScale;
        //obstacleRightObject.transform.localScale = targetScale;
        //obstacleLeftObject.transform.localScale = targetScale;
        //obstaclesObject.transform.localScale = targetScale;

        if (obstacleVector.Item1 == 1)
        {
            objRightPos.x = -3;
            objLeftPos.x = 3;
            wp1LeftPos.x = -1.5f;
            wp2LeftPos.x = -3;
            wp3LeftPos.x = -1.5f;
            wp1RightPos.x = 1.5f;
            wp2RightPos.x = 3;
            wp3RightPos.x = 1.5f;
            obstacleRightObject.transform.position = objRightPos;
            obstacleLeftObject.transform.position = objLeftPos;
            waypoint1L.transform.position = wp1LeftPos;
            waypoint2L.transform.position = wp2LeftPos;
            waypoint3L.transform.position = wp3LeftPos;
            waypoint1R.transform.position = wp1RightPos;
            waypoint2R.transform.position = wp2RightPos;
            waypoint3R.transform.position = wp3RightPos;
            Vector3 targetScaleCenter = new Vector3(4, obstacleCenterObject.transform.localScale.y, obstacleCenterObject.transform.localScale.z);
            Vector3 targetScaleRight = new Vector3(10, obstacleRightObject.transform.localScale.y, obstacleRightObject.transform.localScale.z);
            Vector3 targetScaleLeft = new Vector3(10, obstacleLeftObject.transform.localScale.y, obstacleLeftObject.transform.localScale.z);
            obstacleCenterObject.transform.localScale = targetScaleCenter;
            obstacleRightObject.transform.localScale = targetScaleRight;
            obstacleLeftObject.transform.localScale = targetScaleLeft;
        }
        else if (obstacleVector.Item1 == 2)
        {
            objRightPos.x = -2;
            objLeftPos.x = 2;
            wp1LeftPos.x = -2.5f;
            wp2LeftPos.x = -5;
            wp3LeftPos.x = -2.5f;
            wp1RightPos.x = 2.5f;
            wp2RightPos.x = 5;
            wp3RightPos.x = 2.5f;
            obstacleRightObject.transform.position = objRightPos;
            obstacleLeftObject.transform.position = objLeftPos;
            waypoint1L.transform.position = wp1LeftPos;
            waypoint2L.transform.position = wp2LeftPos;
            waypoint3L.transform.position = wp3LeftPos;
            waypoint1R.transform.position = wp1RightPos;
            waypoint2R.transform.position = wp2RightPos;
            waypoint3R.transform.position = wp3RightPos;
            Vector3 targetScaleCenter = new Vector3(8, obstacleCenterObject.transform.localScale.y, obstacleCenterObject.transform.localScale.z);
            Vector3 targetScaleRight = new Vector3(12, obstacleRightObject.transform.localScale.y, obstacleRightObject.transform.localScale.z);
            Vector3 targetScaleLeft = new Vector3(12, obstacleLeftObject.transform.localScale.y, obstacleLeftObject.transform.localScale.z);
            obstacleCenterObject.transform.localScale = targetScaleCenter;
            obstacleRightObject.transform.localScale = targetScaleRight;
            obstacleLeftObject.transform.localScale = targetScaleLeft;
        }
        else if (obstacleVector.Item1 == 3)
        {
            objRightPos.x = -1;
            objLeftPos.x = 1;
            wp1LeftPos.x = -3.5f;
            wp2LeftPos.x = -7;
            wp3LeftPos.x = -3.5f;
            wp1RightPos.x = 3.5f;
            wp2RightPos.x = 7;
            wp3RightPos.x = 3.5f;
            obstacleRightObject.transform.position = objRightPos;
            obstacleLeftObject.transform.position = objLeftPos;
            waypoint1L.transform.position = wp1LeftPos;
            waypoint2L.transform.position = wp2LeftPos;
            waypoint3L.transform.position = wp3LeftPos;
            waypoint1R.transform.position = wp1RightPos;
            waypoint2R.transform.position = wp2RightPos;
            waypoint3R.transform.position = wp3RightPos;
            Vector3 targetScaleCenter = new Vector3(12, obstacleCenterObject.transform.localScale.y, obstacleCenterObject.transform.localScale.z);
            Vector3 targetScaleRight = new Vector3(14, obstacleRightObject.transform.localScale.y, obstacleRightObject.transform.localScale.z);
            Vector3 targetScaleLeft = new Vector3(14, obstacleLeftObject.transform.localScale.y, obstacleLeftObject.transform.localScale.z);
            obstacleCenterObject.transform.localScale = targetScaleCenter;
            obstacleRightObject.transform.localScale = targetScaleRight;
            obstacleLeftObject.transform.localScale = targetScaleLeft;
        }

        //targetObject.SetActive(true);
        if (obstacleVector.Item2 == 0)
        {
            canvasCenterObject.SetActive(true);
            obstacleCenterObject.SetActive(true);
            targetObject.SetActive(true);
        }
        else if (obstacleVector.Item2 == 1)
        {
            canvasRightObject.SetActive(true);
            obstacleRightObject.SetActive(true);
            targetObject.SetActive(true);
        }
        else if (obstacleVector.Item2 == 2)
        {
            canvasLeftObject.SetActive(true);
            obstacleLeftObject.SetActive(true);
            targetObject.SetActive(true);
        }
        
    }

    private float CalculateTraveledDistance()
    {
        float totalDistance = 0;
        int enterCount = 0;

        int currentNTry = Data_Exp3.numberNTry;
        // Iterate over the positions list and calculate the distance between consecutive points
        for (int i = 1; i < Data_Exp3.rawPositionVector.Count; i++)
        {
            Debug.Log("distance variables NTry: " + currentNTry + " file NTry: " + Data_Exp3.rawPositionVector[i].Item1 + " vector count: " + Data_Exp3.rawPositionVector.Count);
            if ((currentNTry -1) == Data_Exp3.rawPositionVector[i].Item1)
            {
                if (enterCount == 0)
                {
                    totalDistance += 0;
                }
                else
                {
                    totalDistance += Vector3.Distance(Data_Exp3.rawPositionVector[i - 1].Item3, Data_Exp3.rawPositionVector[i].Item3);
                }
                enterCount++;
                Debug.Log("calculated distance inside: " + totalDistance);
            }
            
        }
        Debug.Log("enter count distance: " + enterCount);
        Debug.Log("calculated distance: " + totalDistance);
        return totalDistance;
    }

    private IEnumerator PauseCoroutine()
    {
        // Pause the game
        seatedLocomotion.enabled = false;

        // Wait for 2 seconds in real time
        yield return new WaitForSecondsRealtime(2f);

        // Continue the game
        seatedLocomotion.enabled = true;
    }

    


}
