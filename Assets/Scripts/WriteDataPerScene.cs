using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WriteDataPerScene : MonoBehaviour
{
    private void Awake()
    {
        DeleteDataScene();
    }
    private void Start()
    {
        //DataScene();
    }
    public void DataScene()
    {
        //This function is meant to be called only at the end of the scene
        //
        Debug.Log("DataScene function has been called");

        string tStamp = DateTime.Now.ToString();
        int pNumber = Data_Exp3.participantNumber;
        string lsNumber = Data_Exp3.lsNumberPerParticipant;
        int ordScene = Data_Exp3.numberVariableOrder;
        int totalNTry = Data_Exp3.numberFinalNTry;
        string sceneName = Data_Exp3.sceneName;
        float timeStart = Data_Exp3.timeSceneStart;
        float timeScene = Data_Exp3.timeScene;
        float timeExp = Data_Exp3.timeExperiment;
        float distScene = Data_Exp3.distanceScene;
        int numCollisions = Data_Exp3.numberSceneCollissions;
        int numReentryGaze = Data_Exp3.numberSceneReentryGaze;
        WriteDataScene(tStamp,pNumber, lsNumber, ordScene,totalNTry,sceneName,timeStart,timeScene,timeExp,distScene,numCollisions,numReentryGaze);
        
    }

    public void DataNTry(int i)
    {
        string tStamp = DateTime.Now.ToString();
        int pNumber = Data_Exp3.participantNumber;
        string lsNumber = Data_Exp3.lsNumberPerParticipant;
        int ordScene = Data_Exp3.numberVariableOrder;
        string sceneName = Data_Exp3.sceneName;
        int currentNTry = Data_Exp3.numberNTryList[i] -1;
        string obstacleName = Data_Exp3.currentObsNameList[i];
        float timeStart = Data_Exp3.timeNTryStartList[i];
        float timeEnd = Data_Exp3.timeNTryEndList[i];
        float distScene = Data_Exp3.distanceNTryList[i];
        int numCollissionsNTry = Data_Exp3.numberNTryCollissionsList[i];
        int numReentryGazeNTry = Data_Exp3.numberNTryReentryGazeList[i];
        float timeWP1L = Data_Exp3.distanceWaypoint1LList[i].Item1;
        float distWP1L = Data_Exp3.distanceWaypoint1LList[i].Item2;
        float timeWP2L = Data_Exp3.distanceWaypoint2LList[i].Item1;
        float distWP2L = Data_Exp3.distanceWaypoint2LList[i].Item2;
        float timeWP3L = Data_Exp3.distanceWaypoint3LList[i].Item1;
        float distWP3L = Data_Exp3.distanceWaypoint3LList[i].Item2;
        float timeWP1R = Data_Exp3.distanceWaypoint1RList[i].Item1;
        float distWP1R = Data_Exp3.distanceWaypoint1RList[i].Item2;
        float timeWP2R = Data_Exp3.distanceWaypoint2RList[i].Item1;
        float distWP2R = Data_Exp3.distanceWaypoint2RList[i].Item2;
        float timeWP3R = Data_Exp3.distanceWaypoint3RList[i].Item1;
        float distWP3R = Data_Exp3.distanceWaypoint3RList[i].Item2;
        WriteDataNTry(tStamp, pNumber, lsNumber, ordScene, sceneName, currentNTry, obstacleName, timeStart, timeEnd
        , distScene, numCollissionsNTry, numReentryGazeNTry, timeWP1L, distWP1L, timeWP2L, distWP2L, timeWP3L, distWP3L, timeWP1R, distWP1R, timeWP2R, distWP2R, timeWP3R, distWP3R);



    }

    public void DataDirections(int i)
    {
        string tStamp = DateTime.Now.ToString();
        int pNumber = Data_Exp3.participantNumber;
        string lsNumber = Data_Exp3.lsNumberPerParticipant;
        int ordScene = Data_Exp3.numberVariableOrder;
        string sceneName = Data_Exp3.sceneName;
        int currentNTry = Data_Exp3.rawPositionVector[i].Item1;
        float timePosition = Data_Exp3.rawPositionVector[i].Item2;
        float positionX = Data_Exp3.rawPositionVector[i].Item3.x;
        float positionY = Data_Exp3.rawPositionVector[i].Item3.y;
        float positionZ = Data_Exp3.rawPositionVector[i].Item3.z;
        float transX = Data_Exp3.rawTranslationVector[i].Item3.x;
        float transY = Data_Exp3.rawTranslationVector[i].Item3.y;
        float transZ = Data_Exp3.rawTranslationVector[i].Item3.z;
        float headForwX = Data_Exp3.rawHeadDirVector[i].Item3.x;
        float headForwY = Data_Exp3.rawHeadDirVector[i].Item3.y;
        float headForwZ = Data_Exp3.rawHeadDirVector[i].Item3.z;
        float gazeForwX = Data_Exp3.rawGazeHeadDirVector[i].Item3.x;
        float gazeForwY = Data_Exp3.rawGazeHeadDirVector[i].Item3.y;
        float gazeForwZ = Data_Exp3.rawGazeHeadDirVector[i].Item3.z;
        float gazeRotX = Data_Exp3.rawGazeHeadRotVector[i].Item3.eulerAngles.x;
        float gazeRotY = Data_Exp3.rawGazeHeadRotVector[i].Item3.eulerAngles.y;
        float gazeRotZ = Data_Exp3.rawGazeHeadRotVector[i].Item3.eulerAngles.z;
        //List<(float, Vector3)> playerPositions = new List<(float, Vector3)> { Data_Exp3.rawPositionVector };
        Debug.Log("Data Direction: " + tStamp + pNumber + ordScene + sceneName + currentNTry + timePosition + positionX + positionY + positionZ);
        WriteDataDirections(tStamp, pNumber, lsNumber, ordScene, sceneName, currentNTry, timePosition, positionX, positionY, positionZ, transX, transY, transZ, headForwX, headForwY, headForwZ, gazeForwX, gazeForwY, gazeForwZ, gazeRotX, gazeRotY, gazeRotZ);
    }

    public void DataQuestionnaire()
    {
        string tStamp = DateTime.Now.ToString();
        int pNumber = Data_Exp3.participantNumber;
        string lsNumber = Data_Exp3.lsNumberPerParticipant;
        int ordScene = Data_Exp3.numberVariableOrder;
        string sceneName = Data_Exp3.sceneName;
        float Q1 = Data_Exp3.question1;
        float Q2 = Data_Exp3.question2;
        float Q3 = Data_Exp3.question3;
        float Q4 = Data_Exp3.question4;
        float Q5 = Data_Exp3.question5;
        float Q6 = Data_Exp3.question6;
        float Q7 = Data_Exp3.question7;
        float Q8 = Data_Exp3.question8;
        float Q9 = Data_Exp3.question9;
        float Q10 = Data_Exp3.question10;
        float Q11 = Data_Exp3.question11;
        float Q12 = Data_Exp3.question12;
        float Q13 = Data_Exp3.question13;
        float Q14 = Data_Exp3.question14;
        float Q15 = Data_Exp3.question15;
        float Q16 = Data_Exp3.question16;
        float Q17 = Data_Exp3.question17;
        float Q18 = Data_Exp3.question18;
        float Q19 = Data_Exp3.question19;
        float Q20 = Data_Exp3.question20;
        float Q21 = Data_Exp3.question21;
        float Q22 = Data_Exp3.question22;
        float Q23 = Data_Exp3.question23;
        float Q24 = Data_Exp3.question24;
        float Q25 = Data_Exp3.question25;
        float Q26 = Data_Exp3.question26;
        float Q27 = Data_Exp3.question27;
        float Q28 = Data_Exp3.question28;
        float Q29 = Data_Exp3.question29;
        float Q30 = Data_Exp3.question30;
        float Q31 = Data_Exp3.question31;
        float Q32 = Data_Exp3.question32;
        WriteDataQuestionnaire(tStamp, pNumber, lsNumber, ordScene, sceneName, Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11, Q12, Q13, Q14, Q15, Q16, Q17, Q18, Q19, Q20, Q21, Q22, Q23, Q24, Q25, Q26, Q27, Q28, Q29, Q30, Q31, Q32);

    }

    public void DataHeadset(int i)
    {
        string tStamp = DateTime.Now.ToString();
        int pNumber = Data_Exp3.participantNumber;
        string lsNumber = Data_Exp3.lsNumberPerParticipant;
        int ordScene = Data_Exp3.numberVariableOrder;
        string sceneName = Data_Exp3.sceneName;
        int currentNTry = Data_Exp3.rawPositionVector[i].Item1;
        float timePosition = Data_Exp3.rawPositionVector[i].Item2;
        float localPosX = Data_Exp3.rawLocalHeadPosList[i].Item3.x;
        float localPosY = Data_Exp3.rawLocalHeadPosList[i].Item3.y;
        float localPosZ = Data_Exp3.rawLocalHeadPosList[i].Item3.z;
        float localRotX = Data_Exp3.rawLocalHeadRotList[i].Item3.eulerAngles.x;
        float localRotY = Data_Exp3.rawLocalHeadRotList[i].Item3.eulerAngles.y;
        float localRotZ = Data_Exp3.rawLocalHeadRotList[i].Item3.eulerAngles.z;
        string gazeObjName = Data_Exp3.rawGazeObjNameList[i].Item3;
        float localBodyPosX = Data_Exp3.rawLocalBodyPosList[i].Item3.x;
        float localBodyPosY = Data_Exp3.rawLocalBodyPosList[i].Item3.y;
        float localBodyPosZ = Data_Exp3.rawLocalBodyPosList[i].Item3.z;
        float localBodyRotX = Data_Exp3.rawLocalBodyRotList[i].Item3.eulerAngles.x;
        float localBodyRotY = Data_Exp3.rawLocalBodyRotList[i].Item3.eulerAngles.y;
        float localBodyRotZ = Data_Exp3.rawLocalBodyRotList[i].Item3.eulerAngles.z;
        WriteDataHeadset(tStamp, pNumber, lsNumber, ordScene, sceneName, currentNTry, timePosition, localPosX, localPosY, localPosZ, localRotX, localRotY, localRotZ, gazeObjName, localBodyPosX, localBodyPosY, localBodyPosZ, localBodyRotX, localBodyRotY, localBodyRotZ);
    }

    private void WriteDataScene( string tStamp, int pNumber, string lsNumber, int ordScene, int totalNTry, string sceneName, float timeStart, float timeScene,
        float timeExp, float distScene, int numCollissions, int numReentryGaze)
    {
        //Date, P Number, LSnumber, Order Scene, T.NumberNTry, Scene Name, TimeStart, TimeScene, TimeExp, DistanceScene, NumberCollissions, NumReentryGaze
        List<(string, int, string, int, int, string, float, float, float, float, int, int)> listToWrite = new List<(string, int, string, int, int, string, float, float, float, float, int, int)> 
        {
            (tStamp, pNumber, lsNumber, ordScene, totalNTry, sceneName, timeStart, timeScene, timeExp, distScene, numCollissions, numReentryGaze)
        };

        string fileName = "DataScene_" + Data_Exp3.participantNumber;

        // Define the base file path
        string baseFilePath = "";
        string filePath = baseFilePath;
        #if UNITY_EDITOR
                baseFilePath = Application.dataPath + "/" + fileName + ".csv";
                filePath = baseFilePath;

        #else
                baseFilePath = Application.persistentDataPath + "/" + fileName + ".csv";
                filePath = baseFilePath;
        #endif


        // Create or append to the CSV file
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            

            // Write the data
            foreach (var tuple in listToWrite)
            {
                // Write headers if provided and the file is new
                if (!File.Exists(filePath))
                {
                    string headers = "Date, P Number, Order Scene, T.NumberNTry, Scene Name, TimeStart, TimeScene, TimeExp, DistanceScene, NumberCollissions, NumReentryGaze";
                    writer.WriteLine(headers);
                }
                // Write each tuple as a line in the CSV file
                writer.WriteLine(string.Join(",", tuple));  
            }
        }

        Debug.Log($"Tuple data has been written to: {filePath}");
    }

    private void WriteDataNTry(string tStamp, int pNumber, string lsNumber, int ordScene, string sceneName, int currentNTry, string obstacleName, float timeStart, float timeEnd
        , float distScene, int numCollissionsNTry, int numReentryGazeNTry, float timeWP1L, float distWP1L, float timeWP2L, float distWP2L, float timeWP3L, float distWP3L, float timeWP1R, float distWP1R, float timeWP2R, float distWP2R, float timeWP3R, float distWP3R)
    {
        //Date, P Number, Order Scene, Scene Name, #NTry, obstacleName, TimeStartNTry, TimeEndNTry, DistanceNTry, #CollissionsNTry, #ReentryGazeNTry, TimeWP1, DistWP1, TimeWP2, DistWP2, TimeWP3, DistWP3, TimeWP1R, DistWP1R, TimeWP2R, DistWP2R, TimeWP3R, DistWP3R
        List<(string, int, string, int, string, int, string, float, float, float, int, int, float, float, float, float, float, float, float, float, float, float, float, float)> listToWrite = new List<(string, int, string, int, string, int, string, float, float, float, int, int, float, float, float, float, float, float, float, float, float, float, float, float)>
        {
            (tStamp, pNumber, lsNumber, ordScene, sceneName, currentNTry, obstacleName, timeStart, timeEnd, distScene, numCollissionsNTry, numReentryGazeNTry, timeWP1L, distWP1L, timeWP2L, distWP2L, timeWP3L, distWP3L, timeWP1R, distWP1R, timeWP2R, distWP2R, timeWP3R, distWP3R)
        };

        string fileName = "DataNTry_" + pNumber;

        // Define the base file path
        string baseFilePath = "";
        string filePath = baseFilePath;
        #if UNITY_EDITOR
                baseFilePath = Application.dataPath + "/" + fileName + ".csv";
                filePath = baseFilePath;

        #else
                baseFilePath = Application.persistentDataPath + "/" + fileName + ".csv";
                filePath = baseFilePath;
        #endif


        // Create or append to the CSV file
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {


            // Write the data
            foreach (var tuple in listToWrite)
            {
                // Write headers if provided and the file is new
                if (!File.Exists(filePath))
                {
                    string headers = "Date, P Number, Order Scene, Scene Name, #NTry, obstacleName, TimeStartNTry, TimeEndNTry, DistanceNTry, #CollissionsNTry, #ReentryGazeNTry, TimeWP1, DistWP1, TimeWP2, DistWP2, TimeWP3, DistWP3, TimeWP1R, DistWP1R, TimeWP2R, DistWP2R, TimeWP3R, DistWP3R";
                    writer.WriteLine(headers);
                }
                // Write each tuple as a line in the CSV file
                writer.WriteLine(string.Join(",", tuple));
            }
        }

        Debug.Log($"Tuple data has been written to: {filePath}");
    }

    void WriteDataDirections(string tStamp, int pNumber, string lsNumber, int ordScene, string sceneName, int currentNTry, float timePosition, float positionX, float positionY, float positionZ, 
        float transX, float transY, float transZ, float headForwX, float headForwY, float headForwZ, float gazeForwX, float gazeForwY, float gazeForwZ, float gazeRotX, float gazeRotY, float gazeRotZ)
    {

        //Date, P Number, Order Scene, Scene Name, TimePosition, PositionX, PositionY, PositionZ
        List<(string, int, string, int, string, int, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float)> listToWrite = new List<(string, int, string, int, string, int, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float)>
        {
            (tStamp, pNumber, lsNumber, ordScene, sceneName, currentNTry, timePosition, positionX, positionY, positionZ, transX, transY, transZ, headForwX, headForwY, headForwZ, gazeForwX, gazeForwY, gazeForwZ, gazeRotX, gazeRotY, gazeRotZ)
        };

        string fileName = "PlayerPosition_" + Data_Exp3.participantNumber;
        // Define the base file path
        string baseFilePath = "";
        string filePath = baseFilePath;
        #if UNITY_EDITOR
                baseFilePath = Application.dataPath + "/" + fileName + ".csv";
                filePath = baseFilePath;

        #else
                baseFilePath = Application.persistentDataPath + "/" + fileName + ".csv";
                filePath = baseFilePath;
        #endif

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
                
            // Write the data
            foreach (var tuple in listToWrite)
            {
                // Write headers if provided and the file is new
                //if (!File.Exists(filePath))
                //{
                //    string headers = "Date, P Number, Order Scene, Scene Name, CurrentNTry, Time Pos, X, Y,Z, Time trans, X, Y, Z";
                //    writer.WriteLine(headers);
                //}

                // Write each tuple as a line in the CSV file
                Debug.Log("raw position vector called counter 1");
                writer.WriteLine(string.Join(",", tuple));
                Debug.Log("raw position vector called counter 2");
            }
        }

        Debug.Log($"Player positions have been written to: {filePath}");
    }

    void WriteDataHeadset(string tStamp, int pNumber, string lsNumber, int ordScene, string sceneName, int currentNTry, float timePosition, float localPosX, float localPosY, float localPosZ,
        float localRotX, float localRotY, float localRotZ, string gazeObjName, float bodyDirX, float bodyDirY, float bodyDirZ, float bodyRotX, float bodyRotY, float bodyRotZ)
    {

        //Date, P Number, Order Scene, Scene Name, NumNTry, TimePosition, LocalHeadPosX, LocalHeadPosY, LocalHeadPosZ, LocalHeadRotX, LocalHeadRotY, LocalHeadRotZ, GazeObjName, BodyDirX, BodyDirY, BodyDirZ, BodyRotX, BodyRotY, BodyRotZ
        List<(string, int, string, int, string, int, float, float, float, float, float, float, float, string, float, float, float, float, float, float)> listToWrite = new List<(string, int, string, int, string, int, float, float, float, float, float, float, float, string, float, float, float, float, float, float)>
        {
            (tStamp, pNumber, lsNumber, ordScene, sceneName, currentNTry, timePosition, localPosX, localPosY, localPosZ, localRotX, localRotY, localRotZ, gazeObjName, bodyDirX, bodyDirY, bodyDirZ, bodyRotX, bodyRotY, bodyRotZ)
        };

        string fileName = "HeadsetData_" + Data_Exp3.participantNumber;
        // Define the base file path
        string baseFilePath = "";
        string filePath = baseFilePath;
        #if UNITY_EDITOR
                baseFilePath = Application.dataPath + "/" + fileName + ".csv";
                filePath = baseFilePath;

        #else
                baseFilePath = Application.persistentDataPath + "/" + fileName + ".csv";
                filePath = baseFilePath;
        #endif

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {

            // Write the data
            foreach (var tuple in listToWrite)
            {
                // Write headers if provided and the file is new
                //if (!File.Exists(filePath))
                //{
                //    string headers = "Date, P Number, Order Scene, Scene Name, CurrentNTry, Time Pos, X, Y,Z, Time trans, X, Y, Z";
                //    writer.WriteLine(headers);
                //}

                // Write each tuple as a line in the CSV file
                Debug.Log("raw position vector called counter 1");
                writer.WriteLine(string.Join(",", tuple));
                Debug.Log("raw position vector called counter 2");
            }
        }

        Debug.Log($"Headset data has been written to: {filePath}");
    }

    void WriteDataQuestionnaire(string tStamp, int pNumber, string lsNumber, int ordScene, string sceneName, float Q1, float Q2, float Q3, float Q4, float Q5, float Q6, float Q7, float Q8, float Q9, float Q10, 
        float Q11, float Q12, float Q13, float Q14, float Q15, float Q16, float Q17, float Q18, float Q19, float Q20, 
        float Q21, float Q22, float Q23, float Q24, float Q25, float Q26, float Q27, float Q28, float Q29, float Q30, float Q31, float Q32)
    {

        //Date, P Number, LS#, Order Scene, Scene Name, Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11, Q12, Q13, Q14, Q15, Q16, Q17, Q18, Q19, Q20, Q21, Q22, Q23, Q24, Q25, Q26, Q27, Q28, Q29, Q30, Q31, Q32
        List<(string, int, string, int, string, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float)> 
            listToWrite = new List<(string, int, string, int, string, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float)>
        {
            (tStamp, pNumber, lsNumber, ordScene, sceneName, Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11, Q12, Q13, Q14, Q15, Q16, Q17, Q18, Q19, Q20, Q21, Q22, Q23, Q24, Q25, Q26, Q27, Q28, Q29, Q30, Q31, Q32)
        };

        string fileName = "QuestionnaireData_" + Data_Exp3.participantNumber;
        // Define the base file path
        string baseFilePath = "";
        string filePath = baseFilePath;
        #if UNITY_EDITOR
                baseFilePath = Application.dataPath + "/" + fileName + ".csv";
                filePath = baseFilePath;

        #else
                baseFilePath = Application.persistentDataPath + "/" + fileName + ".csv";
                filePath = baseFilePath;
        #endif

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {

            // Write the data
            foreach (var tuple in listToWrite)
            {
                // Write headers if provided and the file is new
                //if (!File.Exists(filePath))
                //{
                //    string headers = "Date, P Number, Order Scene, Scene Name, CurrentNTry, Time Pos, X, Y,Z, Time trans, X, Y, Z";
                //    writer.WriteLine(headers);
                //}

                // Write each tuple as a line in the CSV file
                Debug.Log("raw position vector called counter 1");
                writer.WriteLine(string.Join(",", tuple));
                Debug.Log("raw position vector called counter 2");
            }
        }

        Debug.Log($"Questionnaire has been written to: {filePath}");
    }

    public void DeleteDataScene()
    {
        //string tStamp = DateTime.Now.ToString();
        //int pNumber = Data_Exp3.participantNumber;
        //int ordScene = Data_Exp3.numberVariableOrder;
        Data_Exp3.numberFinalNTry = 0;
        Data_Exp3.sceneName = "";
        Data_Exp3.timeSceneStart = 0;
        Data_Exp3.timeScene = 0;
        Data_Exp3.timeExperiment = 0;
        Data_Exp3.distanceScene = 0;
        Data_Exp3.numberSceneCollissions = 0;
        Data_Exp3.numberSceneReentryGaze = 0;

        Data_Exp3.numberNTryReentryGaze = 0;
        Data_Exp3.numberNTryReentryGazeList.Clear();
        Data_Exp3.numberNTry = 0;
        Data_Exp3.numberNTryList.Clear();
        Data_Exp3.currentObsName = "";
        Data_Exp3.currentObsNameList.Clear();
        Data_Exp3.timeNTryStart = 0;
        Data_Exp3.timeNTryStartList.Clear();
        Data_Exp3.timeNTryEnd = 0;
        Data_Exp3.timeNTryEndList.Clear();
        //Data_Exp3.distanceNTry = 0;
        Data_Exp3.distanceNTryList.Clear();
        Data_Exp3.numberNTryCollissions = 0;
        Data_Exp3.numberNTryCollissionsList.Clear();
        Data_Exp3.distanceWaypoint1L = 0;
        Data_Exp3.distanceWaypoint1LList.Clear();
        Data_Exp3.distanceWaypoint2L = 0;
        Data_Exp3.distanceWaypoint2LList.Clear();
        Data_Exp3.distanceWaypoint3L = 0;
        Data_Exp3.distanceWaypoint3LList.Clear();
        Data_Exp3.distanceWaypoint1R = 0;
        Data_Exp3.distanceWaypoint1RList.Clear();
        Data_Exp3.distanceWaypoint2R = 0;
        Data_Exp3.distanceWaypoint2RList.Clear();
        Data_Exp3.distanceWaypoint3R = 0;
        Data_Exp3.distanceWaypoint3RList.Clear();

        Data_Exp3.rawPositionVector.Clear();
        Data_Exp3.rawTranslationVector.Clear();
        Data_Exp3.rawHeadDirVector.Clear();
        Data_Exp3.rawGazeHeadDirVector.Clear();
        Data_Exp3.rawGazeHeadRotVector.Clear();

        Data_Exp3.rawLocalHeadPosList.Clear();
        Data_Exp3.rawLocalHeadRotList.Clear();
        Data_Exp3.rawGazeObjNameList.Clear();
        Data_Exp3.rawLocalBodyPosList.Clear();
        Data_Exp3.rawLocalBodyRotList.Clear();

    }
}
