using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for Toggle component

public class Survey : MonoBehaviour
{
    public List<GameObject> qRepliesList = new List<GameObject>();
    public void AssignToggleValues()
    {
        Data_Exp3.question1 = CheckToggles(qRepliesList[0]);
        Data_Exp3.question2 = CheckToggles(qRepliesList[1]);
        Data_Exp3.question3 = CheckToggles(qRepliesList[2]);
        Data_Exp3.question4 = CheckToggles(qRepliesList[3]);
        Data_Exp3.question5 = CheckToggles(qRepliesList[4]);
        Data_Exp3.question6 = CheckToggles(qRepliesList[5]);
        Data_Exp3.question7 = CheckToggles(qRepliesList[6]);
        Data_Exp3.question8 = CheckToggles(qRepliesList[7]);
        Data_Exp3.question9 = CheckToggles(qRepliesList[8]);
        Data_Exp3.question10 = CheckToggles(qRepliesList[9]);
        Data_Exp3.question11 = CheckToggles(qRepliesList[10]);
        Data_Exp3.question12 = CheckToggles(qRepliesList[11]);
        Data_Exp3.question13 = CheckToggles(qRepliesList[12]);
        Data_Exp3.question14 = CheckToggles(qRepliesList[13]);
        Data_Exp3.question15 = CheckToggles(qRepliesList[14]);
        Data_Exp3.question16 = CheckToggles(qRepliesList[15]);
        Data_Exp3.question17 = CheckToggles(qRepliesList[16]);
        Data_Exp3.question18 = CheckToggles(qRepliesList[17]);
        Data_Exp3.question19 = CheckToggles(qRepliesList[18]);
        Data_Exp3.question20 = CheckToggles(qRepliesList[19]);
        Data_Exp3.question21 = CheckToggles(qRepliesList[20]);
        Data_Exp3.question22 = CheckToggles(qRepliesList[21]);
        Data_Exp3.question23 = CheckToggles(qRepliesList[22]);
        Data_Exp3.question24 = CheckToggles(qRepliesList[23]);
        Data_Exp3.question25 = CheckToggles(qRepliesList[24]);
        Data_Exp3.question26 = CheckToggles(qRepliesList[25]);
        Data_Exp3.question27 = CheckToggles(qRepliesList[26]);
        Data_Exp3.question28 = CheckToggles(qRepliesList[27]);
        Data_Exp3.question29 = CheckToggles(qRepliesList[28]);
        Data_Exp3.question30 = CheckToggles(qRepliesList[29]);
        Data_Exp3.question31 = CheckToggles(qRepliesList[30]);
        Data_Exp3.question32 = CheckToggles(qRepliesList[31]);
        Data_Exp3.question33 = CheckToggles(qRepliesList[32]);
        Data_Exp3.question34 = CheckToggles(qRepliesList[33]);
        Data_Exp3.question35 = CheckToggles(qRepliesList[34]);

    }
    // Function to check toggles and assign the value
    private float CheckToggles(GameObject togglesObject)
    {
        float questionNum = 0;
        // Get all Toggle components in the object
        Toggle[] toggles = togglesObject.GetComponentsInChildren<Toggle>();

        // Iterate over all toggles to check their state
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn) // Check if the toggle is on
            {
                questionNum = (i + 1); // Assign the toggle index + 1 to ads1
                Debug.Log("Toggle " + (i + 1) + " is on. Value assigned to ads1: " + questionNum);
            }
        }
        return questionNum;
    }
}
