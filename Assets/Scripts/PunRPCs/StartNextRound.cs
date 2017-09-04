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
            if (roundNumber == 0 && Global.currentRoundNumber != 0)
            {
                Global.isRankingRound = false;
            }
            Global.currentRoundNumber = roundNumber;

            Debug.Log("[PHOTON] Started next round: " + Global.currentRoundNumber + " RR: " + Global.isRankingRound);

            if (Global.isRankingRound)
            {
                SceneManager.LoadScene("Controller02_RankingRound");
            }
            else
            {
                SceneManager.LoadScene("Controller03_PickingScreen");
            }
        }
    }
}
