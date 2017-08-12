using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_GameStart : Photon.MonoBehaviour
{
    public s_CreateGame server;

    public void SendGameHasStarted()
    {
        this.GetComponent<PhotonView>().RPC("GameHasStarted", PhotonNetwork.masterClient);
    }

    [PunRPC]
    public void GameHasStarted()
    {
        if(server != null)
        {
            server.GameHasStarted();
        }
    }
}
