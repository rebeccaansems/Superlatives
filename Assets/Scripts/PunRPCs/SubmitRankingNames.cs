using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubmitRankingNames : Photon.MonoBehaviour
{
    public MainGame server;

    public void SendRankingNames(List<string> data)
    {
        data.Add(PhotonNetwork.player.CustomProperties["JoinNumber"].ToString());
        this.GetComponent<PhotonView>().RPC("RankingNamesRecieved", PhotonTargets.MasterClient, data.ToArray());
    }

    [PunRPC]
    public void RankingNamesRecieved(string[] data)
    {
        if (server != null)
        {
            int playerNum = int.Parse(data[data.Count() - 1]);
            data = data.Take(data.Count() - 1).ToArray();
            Array.Reverse(data);

            Debug.Log("[PHOTON] Recieved ranking names from player #" + playerNum);

            server.ScorePlayersRanking(playerNum, data);
        }
    }
}
