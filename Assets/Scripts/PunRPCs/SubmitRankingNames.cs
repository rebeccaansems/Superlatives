using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitRankingNames : Photon.MonoBehaviour
{
    public bool isServer;

    public void SendRankingNames(string[] data)
    {
        this.GetComponent<PhotonView>().RPC("RankingNamesRecieved", PhotonTargets.MasterClient, data);
    }

    [PunRPC]
    public void RankingNamesRecieved(string[] data)
    {
        if (isServer)
        {
            Debug.Log("[PHOTON] Recieved ranking names from player");
        }
    }
}
