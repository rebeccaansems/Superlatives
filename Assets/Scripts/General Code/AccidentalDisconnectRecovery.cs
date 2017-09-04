using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccidentalDisconnectRecovery : MonoBehaviour
{
    private void OnApplicationPause(bool paused)
    {
        if (!paused)
        {
            Debug.Log("ATTEMPTING RECOVERY: " + Global.currentSectionOfGame);
            switch (Global.currentSectionOfGame)
            {
                case 0:
                    break;
                case 1:
                    SceneManager.LoadScene("Controller00_JoinGame");
                    break;
                case 2:
                    SceneManager.LoadScene("Controller00_JoinGame");
                    break;
                case 3:
                    SceneManager.LoadScene("Controller00_JoinGame");
                    break;
                case 4:
                    SceneManager.LoadScene("Controller02_RankingRound");
                    break;
                case 5:
                    SceneManager.LoadScene("Controller03_PickingScreen");
                    break;
                case 6:
                    SceneManager.LoadScene("Controller00_JoinGame");
                    break;
                case 7:
                    SceneManager.LoadScene("Controller00_JoinGame");
                    break;
                case 8:
                    SceneManager.LoadScene("Controller00_JoinGame");
                    break;
                case 9:
                    SceneManager.LoadScene("Controller00_JoinGame");
                    break;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            Debug.Log("SIMULATING PAUSED GAME");
            OnApplicationPause(false);
        }
    }
}
