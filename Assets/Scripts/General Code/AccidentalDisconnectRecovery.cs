using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccidentalDisconnectRecovery : MonoBehaviour
{
    private void OnApplicationFocus(bool paused)
    {
        if (paused)
        {
            Debug.Log("ATTEMPTING RECOVERY: " + Global.currentSectionOfGame);
            switch (Global.currentSectionOfGame)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    SceneManager.LoadScene("Controller02_RankingRound");
                    break;
                case 5:
                    SceneManager.LoadScene("Controller03_PickingScreen");
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            Debug.Log("SIMULATING PAUSED GAME");
            OnApplicationFocus(false);
        }
    }
}
