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
    }

    [PunRPC]
    public void MostLikelyNameRecieved(string[] data)
    {
        if (server != null)
        {
            int playerNum = int.Parse(data[0]);

            Debug.Log("[PHOTON] Recieved most likely name " + data[1] + " from player #" + playerNum);

            server.AddPlayersMostLikelys(playerNum, data[1]);
        }
    }
}
