using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasks_Exp_2 : MonoBehaviour
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

    [SerializeField] private SeatedLocomotion seatedLocomotion;


    private List<float> orderList = new List<float> { 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3 };
    private List<float> targetList = new List<float> { 3, 6, 9 };
    private List<float> obstacleList = new List<float> { 1.5f, 3f, 4.5f };
    private List<(int,int)> obstacleVectorList = new List<(int,int)> { (3, 0), (3, 0), (3, 0), (3, 1), (3, 1), (3, 1), (3, 2), (3, 2), (3, 2), (3, 3), (3, 3), (3, 3), 
        (6, 0), (6, 0), (6, 0), (6, 1), (6, 1), (6, 1), (6, 2), (6, 2), (6, 2), (6, 3), (6, 3), (6, 3), 
        (9, 0), (9, 0), (9, 0), (9, 1), (9, 1), (9, 1), (9, 2), (9, 2), (9, 2), (9, 3), (9, 3), (9, 3)};
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

        //seatedLocomotion.enabled = true;
    }

    private void RepositionObstacles()
    {
        int indexObstacleList = Random.Range(0, obstacleVectorList.Count);
        (int, int) obstacleVector = obstacleVectorList[indexObstacleList];
        obstacleVectorList.RemoveAt(indexObstacleList);
        dataPositionOrder.Add(obstacleVector);

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
