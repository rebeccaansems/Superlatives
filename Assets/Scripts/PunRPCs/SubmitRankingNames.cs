using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            int playerNum = int.Parse(data[data.Count() - 1]);
            data = data.Take(data.Count() - 1).ToArray();

            Debug.Log("[PHOTON] Recieved ranking names from player #" + playerNum);
            server.ScorePlayers(playerNum, data);
        }
    }
}
