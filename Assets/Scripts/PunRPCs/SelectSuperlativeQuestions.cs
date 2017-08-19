using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSuperlativeQuestions : Photon.MonoBehaviour
{
    public bool isClient;

    public void SendSuperlativeQuestion(string[] data)
    {

        this.GetComponent<PhotonView>().RPC("SuperlativeQuestionsRecieved", PhotonTargets.AllBufferedViaServer, data);
    }

    [PunRPC]
    public void SuperlativeQuestionsRecieved(string[] data)
    {
        if (isClient)
        {
            Debug.Log("[PHOTON] Recieved superlative questions");
            if(Global.rankingRoundQuestions == null)
            {
                Global.rankingRoundQuestions = new List<string>();
                Global.rankingRoundQuestions.Add(data[0]);
                Global.rankingRoundQuestions.Add(data[1]);
                Global.rankingRoundQuestions.Add(data[2]);
                Global.rankingRoundQuestions.Add(data[3]);
            }
        }
    }
}
