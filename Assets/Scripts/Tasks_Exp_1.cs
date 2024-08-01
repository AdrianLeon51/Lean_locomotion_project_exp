using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Tasks_Exp_1 : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private GameObject obstaclesObject;

    [SerializeField] private GameObject obstacleCenterObject;
    [SerializeField] private GameObject obstacleOpnCenterObject;
    [SerializeField] private GameObject obstacleLeftObject;
    [SerializeField] private GameObject obstacleRightObject;

    [SerializeField] private GameObject canvasCenterObject;
    [SerializeField] private GameObject canvasOpenCenterObject;
    [SerializeField] private GameObject canvasLeftObject;
    [SerializeField] private GameObject canvasRightObject;

    [SerializeField] private OVRCameraRig ovrCamerarig;
    [SerializeField] private GameObject centerEyeAnchor;

    [SerializeField] private SeatedLocomotion seatedLocomotion;

    [SerializeField] private float disappearLimit = 0.2f;


    private List<float> orderList = new List<float> { 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3 };
    private List<float> targetList = new List<float> { 3, 6, 9 };
    private List<float> obstacleList = new List<float> { 1.5f, 3f, 4.5f };
    private List<(int, int)> obstacleVectorList = new List<(int, int)> { (3, 0) };
    //private List<(int,int)> obstacleVectorList = new List<(int,int)> { (3, 0), (3, 0), (3, 0), (3, 1), (3, 1), (3, 1), (3, 2), (3, 2), (3, 2), (3, 3), (3, 3), (3, 3), 
    //    (6, 0), (6, 0), (6, 0), (6, 1), (6, 1), (6, 1), (6, 2), (6, 2), (6, 2), (6, 3), (6, 3), (6, 3), 
    //    (9, 0), (9, 0), (9, 0), (9, 1), (9, 1), (9, 1), (9, 2), (9, 2), (9, 2), (9, 3), (9, 3), (9, 3)};
    //private List<(int, int)> dataPositionOrder = new List<(int, int)> { };

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

    // Previous forward vectors
    private Vector3 previousCenterEyeForward;
    private Vector3 previousCameraRigForward;
    private float differencePrevCenterVsCameraRig;
    private float differenceCurrentCenterVsCameraRig;

    private float elapsedTime = 0;

    private bool wasAccessed = false;
    private bool targetReached = true;

    // Start is called before the first frame update
    void Start()
    {
        Camera mainCamera = Camera.main;

        originalPosition = ovrCamerarig.transform.position;
        originalRotation = ovrCamerarig.transform.rotation;

        // Initialize previous forward vectors
        previousCenterEyeForward = centerEyeAnchor.transform.forward;
        previousCameraRigForward = ovrCamerarig.transform.forward;
        differencePrevCenterVsCameraRig = Vector3.Angle(previousCameraRigForward, previousCenterEyeForward);

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        CalculateVectors();
        if (obstacleVectorList.Count == 0 && wasAccessed == false)
        {
            PrintTupleElementInt("Entries List", Data_Exp1.entriesList);
            PrintTupleElementsInt("Order of appearance",Data_Exp1.dataPositionOrder);
            PrintTupleElementsFloat("Angle Head vs. Translation",Data_Exp1.dataHeadVsTranslationAngle);

            WriteTupleListToCSV("Order of appearance", "order_file", Data_Exp1.dataPositionOrder);

            wasAccessed = true;
        }

        //Set active for canvas dissappearing after move.
        if (Mathf.Abs(ovrCamerarig.transform.position.x) >= disappearLimit && targetReached == true)
        {
            OffAfterMove();
        }
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


    public void TargetReached()
    {
        StartCoroutine(PauseCoroutine());
        //seatedLocomotion.enabled = false;

        ovrCamerarig.transform.position = originalPosition;
        ovrCamerarig.transform.rotation = originalRotation;

        obstacleCenterObject.SetActive(false);
        obstacleOpnCenterObject.SetActive(false);
        obstacleLeftObject.SetActive(false);
        obstacleRightObject.SetActive(false);

        canvasCenterObject.SetActive(false);
        canvasOpenCenterObject.SetActive(false);
        canvasLeftObject.SetActive(false);
        canvasRightObject.SetActive(false);

        targetObject.SetActive(false);

        RepositionObstacles();
        

        counterStates++;
        Debug.Log("Counter states: " + counterStates);

        targetReached = true;

        //seatedLocomotion.enabled = true;
    }

    private void OffAfterMove()
    {
        targetReached = false;
        if (Mathf.Abs(ovrCamerarig.transform.position.x) >= disappearLimit)
        {
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
        obstacleVectorList.RemoveAt(indexObstacleList);
        Data_Exp1.dataPositionOrder.Add(obstacleVector);

        Vector3 targetObjectPosition = targetObject.transform.position;
        Vector3 obstaclesObjectPosition = obstaclesObject.transform.position;
        Vector3 canvasPosition = canvasObject.transform.position;

        targetObjectPosition.z = obstacleVector.Item1;
        targetObject.transform.position = targetObjectPosition;
        obstaclesObjectPosition.z = obstacleVector.Item1/2;
        obstaclesObject.transform.position = obstaclesObjectPosition;
        canvasPosition.z = obstacleVector.Item1/2;
        canvasObject.transform.position = canvasPosition;

        //targetObject.SetActive(true);
        if (obstacleVector.Item2 == 0)
        {
            canvasCenterObject.SetActive(true);
            obstacleCenterObject.SetActive(true);
            targetObject.SetActive(true);
        }
        else if (obstacleVector.Item2 == 1)
        {
            canvasOpenCenterObject.SetActive(true);
            obstacleOpnCenterObject.SetActive(true);
            targetObject.SetActive(true);
        }
        else if (obstacleVector.Item2 == 2)
        {
            canvasRightObject.SetActive(true);
            obstacleRightObject.SetActive(true);
            targetObject.SetActive(true);
        }
        else if (obstacleVector.Item2 == 3)
        {
            canvasLeftObject.SetActive(true);
            obstacleLeftObject.SetActive(true);
            targetObject.SetActive(true);
        }
        
    }

    void WriteTupleListToCSV(string text, string fileName, List<(int, int)> list)
    {
        string filePath = Application.dataPath + "/" + fileName + ".csv";

        // Create or append to the CSV file
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            foreach (var tuple in list)
            {
                // Write each tuple as a line in the CSV file
                writer.WriteLine($"{text},{tuple.Item1},{tuple.Item2}");
            }
        }

        Debug.Log($"Tuple data has been written to: {filePath}");
    }

    void PrintTupleElementInt(string text, List<int> list)
    {
        foreach(var tuple in list)
        {
            Debug.Log(text + $" Tuple: ({tuple})");
        }
    }

    void PrintTupleElementsInt(string text, List<(int, int)> list)
    {
        foreach (var tuple in list)
        {
            Debug.Log(text + $" Tuple: ({tuple.Item1}, {tuple.Item2})");
        }
    }

    void PrintTupleElementsFloat(string text, List<(float, float)> list)
    {
        foreach (var tuple in list)
        {
            Debug.Log(text + $" Tuple: ({tuple.Item1}, {tuple.Item2})");
        }
    }

    private void CalculateVectors()
    {
        differenceCurrentCenterVsCameraRig = Vector3.Angle(ovrCamerarig.transform.forward, centerEyeAnchor.transform.forward);
        if (differencePrevCenterVsCameraRig != differenceCurrentCenterVsCameraRig)
        {
            Data_Exp1.dataHeadVsTranslationAngle.Add((elapsedTime, differenceCurrentCenterVsCameraRig));
            differencePrevCenterVsCameraRig = differenceCurrentCenterVsCameraRig;
        }
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
