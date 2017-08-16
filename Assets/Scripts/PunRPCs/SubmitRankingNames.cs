using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitRankingNames : Photon.MonoBehaviour
{
    public MainGame server;

    public void SendRankingNames(string[] data)
    {
        this.GetComponent<PhotonView>().RPC("RankingNamesRecieved", PhotonTargets.MasterClient, data);
    }

    [PunRPC]
    public void RankingNamesRecieved(string[] data)
    {
        if (server != null)
        {
            Debug.Log("[PHOTON] Recieved ranking names from player");
            server.ScorePlayers(data);
        }
    }
}
