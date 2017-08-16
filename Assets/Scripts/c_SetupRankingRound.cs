using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class c_SetupRankingRound : MonoBehaviour
{
    public Text currentQuestion;
    public GameObject nameBlockHeader, nameBlock;

    void Start()
    {
        currentQuestion.text = c_SuperlativeQuestions.rankingRoundQuestions[c_SuperlativeQuestions.currentRound];

        for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
        {
            if (PhotonNetwork.playerList[i].CustomProperties["JoinNumber"] != null)
            {
                int playerNum = int.Parse(PhotonNetwork.playerList[i].CustomProperties["JoinNumber"].ToString());
                if (playerNum != -1 && !PhotonNetwork.playerList[i].NickName.Equals(""))
                {
                    GameObject newNameBlock = Instantiate(nameBlock, nameBlockHeader.transform);
                    s_global.allPlayers.Add(PhotonNetwork.playerList[i]);

                    newNameBlock.transform.GetChild(0).GetComponent<Text>().text = PhotonNetwork.playerList[i].NickName;
                    newNameBlock.transform.GetChild(1).GetComponent<Image>().sprite = GetComponent<c_PossibleCharacterInfo>().characterPictures[0];
                }
            }
        }
    }

    void Update()
    {

    }
}
