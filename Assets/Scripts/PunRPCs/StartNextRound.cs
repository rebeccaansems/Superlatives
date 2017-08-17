using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNextRound : Photon.MonoBehaviour
{
    public bool isClient;

    public void NextRound()
    {
        this.GetComponent<PhotonView>().RPC("BeginNextRound", PhotonTargets.Others, Global.currentRoundNumber);
    }

    [PunRPC]
    public void BeginNextRound(int roundNumber)
    {
        if (isClient)
        {
            Debug.Log("[PHOTON] Started next round: " + roundNumber);
            Global.currentRoundNumber = roundNumber;
            SceneManager.LoadScene("Controller02_RankingRound");
        }
    }
}
