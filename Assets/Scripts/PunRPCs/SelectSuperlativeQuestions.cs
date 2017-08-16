using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSuperlativeQuestions : Photon.MonoBehaviour
{
    public bool isClient;

    public void SendSuperlativeQuestion(string[] data)
    {
        this.GetComponent<PhotonView>().RPC("SuperlativeQuestionsRecieved", PhotonTargets.AllBuffered, data);
    }

    [PunRPC]
    public void SuperlativeQuestionsRecieved(string[] data)
    {
        if (isClient)
        {
            Debug.Log("[PHOTON] Recieved superlative questions");
            SuperlativeQuestions.rankingRoundQuestions = new List<string>();
            SuperlativeQuestions.rankingRoundQuestions.Add(data[0]);
            SuperlativeQuestions.rankingRoundQuestions.Add(data[1]);
            SuperlativeQuestions.rankingRoundQuestions.Add(data[2]);
            SuperlativeQuestions.rankingRoundQuestions.Add(data[3]);
        }
    }
}
