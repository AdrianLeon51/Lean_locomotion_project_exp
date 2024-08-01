using System.Collections.Generic;
using UnityEngine;
public static class Data_Exp3
{
    public static string sceneName;
    public static int numberTrial;
    public static int numberAmplitude;

    public static float timeTrial;
    public static float timeScene;

    public static float distanceTrial;
    public static float distanceScene;

    public static int numberTrialCollissions;
    public static int numberSceneCollissions;

    public static int numberTrialReentryGaze;
    public static int numberSceneReentryGaze;

    public static float distanceWaypoint1;
    public static float distanceWaypoint2;
    public static float distanceWaypoint3;

    //Vectors missing
    public static List<(Vector3, Vector3, Vector3)> rawDirectionVectors = new List<(Vector3, Vector3, Vector3)> { };
    public static Vector3 translationVec;
    public static Vector3 headDirVec;
    public static Vector3 gazeHeadDirVec;

    //Still not assigned
    public static List<(float, float, float, string, float, float)> rawStringData = new List<(float, float, float, string, float, float)> { };
    public static float positionPlayer;
    public static float positionHeadLocal;
    public static float rotationHeadLocal;
    //public static Vector3 directionGaze;
    public static string pointedObjectName;
    public static float horizontalAngle;
    public static float verticalAngle;

    //Questionnaires
    public static int physicalDemandQ;
    public static int presenceQ;


    public static int entriesCounter;
    public static List<int> entriesList = new List<int> { };

    public static List<(int, int)> dataPositionOrder = new List<(int, int)> { };

    public static List<(float, float)> dataHeadVsTranslationAngle = new List<(float, float)> { };
    public static List<(float, float)> dataEyeVsTranslationAngle = new List<(float, float)> { };
    public static List<(float, float)> dataEyeVsHeadAngle = new List<(float, float)> { };
}
