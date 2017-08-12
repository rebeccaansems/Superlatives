using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_Vibrate : MonoBehaviour
{
    public s_DealWithVotes server;

    public void SendVibrate(int playerNum)
    {
        this.GetComponent<PhotonView>().RPC("Vibrate", PhotonNetwork.masterClient, playerNum.ToString());
    }

    [PunRPC]
    public void Vibrate(string playerNum)
    {
        if(server != null)
        {
            server.sendVibrations(int.Parse(playerNum));
        }
    }
}
