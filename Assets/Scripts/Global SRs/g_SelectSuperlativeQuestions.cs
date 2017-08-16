﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_SelectSuperlativeQuestions : Photon.MonoBehaviour
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
            c_SuperlativeQuestions.rankingRoundQuestions = new List<string>();
            c_SuperlativeQuestions.rankingRoundQuestions.Add(data[0]);
            c_SuperlativeQuestions.rankingRoundQuestions.Add(data[1]);
            c_SuperlativeQuestions.rankingRoundQuestions.Add(data[2]);
            c_SuperlativeQuestions.rankingRoundQuestions.Add(data[3]);
        }
    }
}
