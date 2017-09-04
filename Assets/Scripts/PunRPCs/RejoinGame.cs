using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RejoinGame : Photon.MonoBehaviour
{
    public MainGame server;

    public void SendRejoinGame()
    {
        this.GetComponent<PhotonView>().RPC("RecieveRejoinGame", PhotonTargets.MasterClient, Global.currentSectionOfGame);
    }

    [PunRPC]
    public void RecieveRejoinGame(int rejoinNumber)
    {
        if (server != null)
        {
            Debug.Log("[PHOTON] Recieved reconnect attempt");

            switch (rejoinNumber)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
            }


        }
    }
}

