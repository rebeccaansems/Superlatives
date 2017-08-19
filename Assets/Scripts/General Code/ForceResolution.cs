using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForceResolution : MonoBehaviour
{

    void Awake()
    {
        if (Debug.isDebugBuild)
        {
            if (SceneManager.GetSceneByBuildIndex(0).name.Contains("Server"))
            {
                Screen.SetResolution(1280 / 2, 720 / 2, false);
            }
            else
            {
                Screen.SetResolution(600 / 2, 1024 / 2, false);
            }
        }
    }
}
