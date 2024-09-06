using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialInfoSave : MonoBehaviour
{
    
    public void AssignParticipant(Dropdown numberParticipant)
    {
        
        Data_Exp3.participantNumber = int.Parse(numberParticipant.options[numberParticipant.value].text);
        Debug.Log("participan number was assigned: " + Data_Exp3.participantNumber);
    }

    public void AssignScenesOrder(Dropdown lsValue)
    {
        string lsString = lsValue.options[lsValue.value].text;
        Data_Exp3.lsNumberPerParticipant = lsString;

        //LSlist = new List<string> { };
        if (lsString == "LS1")
        {
            Debug.Log("LS1 was chosen");
            Data_Exp3.nameScenesList.Add("Head_D_Tutorial");
            Data_Exp3.nameScenesList.Add("Head_NoD_Tutorial");
            Data_Exp3.nameScenesList.Add("Eye_D_Tutorial");
            Data_Exp3.nameScenesList.Add("Eye_NoD_Tutorial");
        }
        else if (lsString == "LS2")
        {
            Debug.Log("LS2 was chosen");
            Data_Exp3.nameScenesList.Add("Head_NoD_Tutorial");
            Data_Exp3.nameScenesList.Add("Eye_D_Tutorial");
            Data_Exp3.nameScenesList.Add("Eye_NoD_Tutorial");
            Data_Exp3.nameScenesList.Add("Head_D_Tutorial");
        }
        else if (lsString == "LS3")
        {
            Debug.Log("LS3 was chosen");
            Data_Exp3.nameScenesList.Add("Eye_D_Tutorial");
            Data_Exp3.nameScenesList.Add("Eye_NoD_Tutorial");
            Data_Exp3.nameScenesList.Add("Head_D_Tutorial");
            Data_Exp3.nameScenesList.Add("Head_NoD_Tutorial");
        }
        else if (lsString == "LS4")
        {
            Debug.Log("LS4 was chosen");
            Data_Exp3.nameScenesList.Add("Eye_NoD_Tutorial");
            Data_Exp3.nameScenesList.Add("Head_D_Tutorial");
            Data_Exp3.nameScenesList.Add("Head_NoD_Tutorial");
            Data_Exp3.nameScenesList.Add("Eye_D_Tutorial");
        }
        else
        {
            Debug.Log("No LS value assigned");
        }

    }
}
