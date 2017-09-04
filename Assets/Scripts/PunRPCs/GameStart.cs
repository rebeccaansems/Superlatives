using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : Photon.MonoBehaviour
{
    public CreateGame server;

    public void SendGameHasStarted()
    {
        this.GetComponent<PhotonView>().RPC("GameHasStarted", PhotonTargets.Others);
    }

    [PunRPC]
    public void GameHasStarted()
    {
        if (server != null)
        {
            server.GameHasStarted();
        }
        else
        {
            SceneManager.LoadScene("Controller03_PickingScreen");
        }
    }
}
