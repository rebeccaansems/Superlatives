using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubmitMostLikelyNames : Photon.MonoBehaviour
{
    public MainGame server;

    public void SendMostLikelyName(string data)
    {
        this.GetComponent<PhotonView>().RPC("MostLikelyNameRecieved", PhotonTargets.MasterClient, 
            new string[2] { PhotonNetwork.player.CustomProperties["JoinNumber"].ToString(), data });

        Debug.Log("[PHOTON] Recieved most likely name " + data + " from player #" + PhotonNetwork.player.CustomProperties["JoinNumber"].ToString());
    }

    [PunRPC]
    public void RankingNamesRecieved(string[] data)
    {
        if (server != null)
        {
            int playerNum = int.Parse(data[data.Count() - 1]);
            data = data.Take(data.Count() - 1).ToArray();

            Debug.Log("[PHOTON] Recieved most likely name " + data[1] + " from player #" + playerNum);

            server.ScorePlayersMostLikely(playerNum, data[1]);
        }
    }
}
