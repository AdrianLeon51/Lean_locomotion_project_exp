using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneChange : MonoBehaviour
{
    // Name of the scene to load when the A button is pressed
    [SerializeField] private string nextSceneName;
    [SerializeField] private Tasks_Exp_3 tasks_Exp_3;
    
    private List<string> LSlist;

    void Update()
    {
        // Check if the A button is pressed on the right controller
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        
        // Check if the scene name is not empty or null
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            tasks_Exp_3.WriteAndApplicationQuit();
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not specified.");
        }
    }

    

    public void NextLSScene()
    {
        if (Data_Exp3.nameScenesList.Count != 0)
        {
            string nextScene = Data_Exp3.nameScenesList[0];
            Data_Exp3.nameScenesList.RemoveAt(0);
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        
    }
}
