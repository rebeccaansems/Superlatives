using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingRound : MonoBehaviour
{
    public Text currentQuestion;
    public GameObject nameBlockHeader, nameBlock;
    public SubmitRankingNames submitRanking;

    private void Start()
    {
        currentQuestion.text = SuperlativeQuestions.rankingRoundQuestions[SuperlativeQuestions.currentRound];

        for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
        {
            if (PhotonNetwork.playerList[i].CustomProperties["JoinNumber"] != null)
            {
                int playerNum = int.Parse(PhotonNetwork.playerList[i].CustomProperties["JoinNumber"].ToString());
                if (playerNum != -1 && !PhotonNetwork.playerList[i].NickName.Equals(""))
                {
                    GameObject newNameBlock = Instantiate(nameBlock, nameBlockHeader.transform);
                    Global.allPlayers.Add(PhotonNetwork.playerList[i]);

                    newNameBlock.transform.GetChild(0).GetComponent<Text>().text = PhotonNetwork.playerList[i].NickName;
                    newNameBlock.transform.GetChild(1).GetComponent<Image>().sprite = GetComponent<PossibleCharacterInfo>().characterPictures[0];
                }
            }
        }
    }

    public void SubmitRankingOrder()
    {
        List<string> playerRankingOrder = new List<string>();

        for (int i = 0; i < Global.allPlayers.Count/2; i++)
        {
            playerRankingOrder.Add(nameBlockHeader.transform.GetChild(i).GetChild(0).GetComponent<Text>().text);
        }

        string[] playerRanking = playerRankingOrder.ToArray();
        submitRanking.SendRankingNames(playerRanking);
    }
}
