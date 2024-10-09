using System.Collections.Generic;
using UnityEngine;
public static class Data_Exp3
{
    public static int participantNumber;

    public static string lsNumberPerParticipant;
    public static List<string> nameScenesList = new List<string> { };

    public static string sceneName;
    public static List<int> numberNTryList = new List<int> { };
    //Current N Try number
    public static int numberNTry = 0;
    //Total N Try number
    public static int numberFinalNTry;

    //Number refering to the order of appearance
    public static int numberVariableOrder;
    public static int numberVariable;

    public static List<float> timeTrialList = new List<float> { };
    public static List<float> timeSceneList = new List<float> { };
    public static List<float> timeExperimentList = new List<float> { };
    public static List<float> timeNTryList = new List<float> { };
    public static List<float> timeNTryStartList = new List<float> { };
    public static List<float> timeNTryEndList = new List<float> { };
    //Time per block or variable to study. Example: "Eye+Deadzone".
    public static float timeTrial;
    //Time per scene, which could be intro, training, experiment or questionnaire
    public static float timeScene;
    //Time per scene START, is right after the first movement is allowed. Thus the start time of the experiment
    public static float timeSceneStart;
    
    //Time after first move is allowed in experiment = timeScene - (time till user moves)
    public static float timeExperiment;
    //Time after each move is allowed in experiment
    public static float timeNTry;
    //Time stamp each move is allowed START in experiment
    public static float timeNTryStart;
    //Time stamp each move is allowed FINISH in experiment
    public static float timeNTryEnd;

    public static List<float> distanceSceneList = new List<float> { };
    public static List<float> distanceNTryList = new List<float> { };
    //Distance travelled in full scene
    public static float distanceScene;
    //Distance travelled after each move is allowed
    public static float distanceNTry;

    public static List<float> numberSceneCollissionsList = new List<float> { };
    public static List<int> numberNTryCollissionsList = new List<int> { };
    public static List<string> currentObsNameList = new List<string> { };
    public static List<string> currentObsSizeList = new List<string> { };
    //Collissions detected in full scene
    public static int numberSceneCollissions;
    //Collissions detected after each move is allowed
    public static int numberNTryCollissions;
    //Name of current obstacle
    public static string currentObsName;
    //Size of current obstacle
    public static string currentObsSize;

    public static List<float> numberSceneReentryGazeList = new List<float> { };
    public static List<int> numberNTryReentryGazeList = new List<int> { };
    public static List<int> numberNTryReentryThresholdGazeList = new List<int> { };
    //Reentries detected in full scene
    public static int numberSceneReentryGaze;
    //Reentries detected after each move is allowed - Equivalent to Dwell time
    public static int numberNTryReentryGaze;
    //Reentries detected after defined threshold
    public static int numberNTryReentryThresholdGaze;

    public static List<(float,float)> distanceWaypoint1LList = new List<(float,float)> { };
    public static List<(float,float)> distanceWaypoint2LList = new List<(float,float)> { };
    public static List<(float,float)> distanceWaypoint3LList = new List<(float,float)> { };
    public static List<(float, float)> distanceWaypoint1RList = new List<(float, float)> { };
    public static List<(float, float)> distanceWaypoint2RList = new List<(float, float)> { };
    public static List<(float, float)> distanceWaypoint3RList = new List<(float, float)> { };
    //Has a time stamp on when waypoit was reached
    //Time at waypoint 1 per N try
    public static float timeWaypoint1;
    //Time at waypoint 2 per N Try
    public static float timeWaypoint2;
    //Time at waypoint 3 per N Try
    public static float timeWaypoint3;
    //Distance to waypoint 1 per N try
    public static float distanceWaypoint1L;
    //Distnace to waypoint 2 per N Try
    public static float distanceWaypoint2L;
    //Distance to waypoint 3 per N Try
    public static float distanceWaypoint3L;
    //Distance to waypoint 1 per N try
    public static float distanceWaypoint1R;
    //Distnace to waypoint 2 per N Try
    public static float distanceWaypoint2R;
    //Distance to waypoint 3 per N Try
    public static float distanceWaypoint3R;

    //Vectors missing
    public static List<(float, Vector3, Vector3, Vector3,Vector3,Quaternion)> rawDirectionVectors = new List<(float, Vector3, Vector3, Vector3,Vector3,Quaternion)> { };
    public static List<(int, float, Vector3, string, string)> rawPositionVector = new List<(int, float, Vector3, string, string)> { };
    public static List<(int, float, Vector3)> rawTranslationVector = new List<(int, float, Vector3)> { };
    public static List<(int, float, Vector3)> rawHeadDirVector = new List<(int, float, Vector3)> { };
    public static List<(int, float, Vector3)> rawGazeHeadDirVector = new List<(int, float, Vector3)> { };
    public static List<(int, float, Quaternion)> rawGazeHeadRotVector = new List<(int, float, Quaternion)> { };
    //Time stamp when Vectors are recorded
    public static float timeVectorDirections;
    //Position of the player every frame
    public static Vector3 positionPlayer;
    //Vector 3D for translation everyframe Translation Vector = Position Head Local Vector
    public static Vector3 translationVec;
    //Vector 3D for forward head direction everyframe
    public static Vector3 headDirVec;
    //Vector 3D for gaze-head direction everyframe
    public static Vector3 gazeHeadDirVec;
    //Quaternion for gaze-head rotation every frame
    public static Quaternion gazeHeadRotVec;

    //Still not assigned
    public static List<(float, Vector3, Quaternion,string,Vector3 , Quaternion, float, float)> rawHeadData = new List<(float, Vector3, Quaternion, string, Vector3, Quaternion, float, float)> { };
    public static List<(int, float, Vector3)> rawLocalHeadPosList = new List<(int, float, Vector3)> { };
    public static List<(int, float, Quaternion)> rawLocalHeadRotList = new List<(int, float, Quaternion)> { };
    public static List<(int, float, string)> rawGazeObjNameList = new List<(int, float, string)> { };
    public static List<(int, float, Vector3)> rawLocalBodyPosList = new List<(int, float, Vector3)> { };
    public static List<(int, float, Quaternion)> rawLocalBodyRotList = new List<(int, float, Quaternion)> { };
    //Time stamp at which all raw string data is collected
    public static float timeRawHeadData;
    //Local Head Position every frame. Translation Vector = Position Head Local Vector
    public static Vector3 positionHeadLocal;
    //Local Head Rotation Angle every frame
    public static Quaternion rotationHeadLocal;
    //Name of gazing pointing object every frame
    public static string pointedObjectName;
    //original vector position
    public static Vector3 originalPosition;
    //orignial quaternion rotation
    public static Quaternion originalRotation;
    //Vector for body direction with respect to the orginial leaning vector
    public static Vector3 bodyHeadData;
    //Quaternion for body rotation with respect to the orginial leaning vector
    public static Quaternion bodyHeadRotData;
    //Horizontal body angle every frame, in reference to original leaning vector
    public static float horizontalAngle;
    //Vertical body angle every frame, in reference to original leaning vector
    public static float verticalAngle;

    //Questionnaires
    public static int physicalDemandQ;
    public static int presenceQ;
    public static float question1;
    public static float question2;
    public static float question3;
    public static float question4;
    public static float question5;
    public static float question6;
    public static float question7;
    public static float question8;
    public static float question9;
    public static float question10;
    public static float question11;
    public static float question12;
    public static float question13;
    public static float question14;
    public static float question15;
    public static float question16;
    public static float question17;
    public static float question18;
    public static float question19;
    public static float question20;
    public static float question21;
    public static float question22;
    public static float question23;
    public static float question24;
    public static float question25;
    public static float question26;
    public static float question27;
    public static float question28;
    public static float question29;
    public static float question30;
    public static float question31;
    public static float question32;
    public static float question33;
    public static float question34;
    public static float question35;

    //Target Object 
    public static int entriesCounter;
    public static List<int> entriesList = new List<int> { };

    public static List<(int, int)> dataPositionOrder = new List<(int, int)> { };

    public static List<(float, float)> dataHeadVsTranslationAngle = new List<(float, float)> { };
    public static List<(float, float)> dataEyeVsTranslationAngle = new List<(float, float)> { };
    public static List<(float, float)> dataEyeVsHeadAngle = new List<(float, float)> { };
}
