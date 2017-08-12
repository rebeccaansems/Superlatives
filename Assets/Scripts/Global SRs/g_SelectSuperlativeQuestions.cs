using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_SelectSuperlativeQuestions : Photon.MonoBehaviour
{
    public bool isClient;

    public void SendSuperlativeQuestion(string data)
    {
        this.GetComponent<PhotonView>().RPC("SuperlativeQuestionsRecieved", PhotonTargets.AllBuffered, "sup " + data);
    }

    [PunRPC]
    public void SuperlativeQuestionsRecieved(string data)
    {
        if (isClient)
        {
            Debug.Log("[PHOTON] Recieved superlative questions");
            gc_SuperlativeQuestions.round1Questions = new List<string>();
            gc_SuperlativeQuestions.round1Questions.Add(data);
            gc_SuperlativeQuestions.round1Questions.Add("Question2");
            gc_SuperlativeQuestions.round1Questions.Add("Question3");
            gc_SuperlativeQuestions.round1Questions.Add("Question4");
        }
    }
}
