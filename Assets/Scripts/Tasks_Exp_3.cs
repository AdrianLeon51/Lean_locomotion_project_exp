using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;


public class Tasks_Exp_3 : MonoBehaviour
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

    [SerializeField] private float disappearLimit = 0.2f;

    [SerializeField] private OVRCameraRig ovrCamerarig;

    [SerializeField] private SeatedLocomotion seatedLocomotion;


    private List<float> orderList = new List<float> { 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3 };
    private List<float> targetList = new List<float> { 3, 6, 9 };
    private List<float> obstacleList = new List<float> { 1.5f, 3f, 4.5f };
    //private List<(int, int)> obstacleVectorList = new List<(int, int)> { (1, 0), (2,1), (3,2) };
    private List<(int,int)> obstacleVectorList = new List<(int,int)> { (1, 0), (1, 0), (1, 0), (1, 1), (1, 1), (1, 1), (1, 2), (1, 2), (1, 2), 
        (2, 0), (2, 0), (2, 0), (2, 1), (2, 1), (2, 1), (2, 2), (2, 2), (2, 2), 
        (3, 0), (3, 0), (3, 0), (3, 1), (3, 1), (3, 1), (3, 2), (3, 2), (3, 2)};
    private List<(int, int)> dataPositionOrder = new List<(int, int)> { };

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
    private bool targetReached = true;

    // Start is called before the first frame update
    void Start()
    {
        Camera mainCamera = Camera.main;

        originalPosition = ovrCamerarig.transform.position;
        originalRotation = ovrCamerarig.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
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

        targetReached = true;

        counterStates++;
        Debug.Log("Counter states: " + counterStates);

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
        dataPositionOrder.Add(obstacleVector);


        Vector3 objRightPos = obstacleRightObject.transform.position;
        Vector3 objLeftPos = obstacleLeftObject.transform.position;
        //obstacleCenterObject.transform.localScale = targetScale;
        //obstacleRightObject.transform.localScale = targetScale;
        //obstacleLeftObject.transform.localScale = targetScale;
        //obstaclesObject.transform.localScale = targetScale;

        if (obstacleVector.Item1 == 1)
        {
            objRightPos.x = -3;
            objLeftPos.x = 3;
            obstacleRightObject.transform.position = objRightPos;
            obstacleLeftObject.transform.position = objLeftPos;
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
            obstacleRightObject.transform.position = objRightPos;
            obstacleLeftObject.transform.position = objLeftPos;
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
            obstacleRightObject.transform.position = objRightPos;
            obstacleLeftObject.transform.position = objLeftPos;
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

    private IEnumerator PauseCoroutine()
    {
        // Pause the game
        seatedLocomotion.enabled = false;

        // Wait for 2 seconds in real time
        yield return new WaitForSecondsRealtime(2f);

        // Continue the game
        seatedLocomotion.enabled = true;
    }

    void WriteTupleListToCSV(string text, string fileName, List<(int, int)> list)
    {
        //string filePath = Application.dataPath + "/" + fileName + ".csv";

        // Define the base file path
        string baseFilePath = Application.dataPath + "/" + fileName + ".csv";
        string filePath = baseFilePath;
        int counter = 1;

        // Check if the file exists and find an available file name
        while (File.Exists(filePath))
        {
            filePath = Application.dataPath + "/" + fileName + $"({counter}).csv";
            counter++;
        }

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


}
