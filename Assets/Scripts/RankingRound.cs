using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingRound : MonoBehaviour
{
    public Text currentQuestion;
    public GameObject nameBlockHeader, nameBlock;
    public SubmitRankingNames submitRanking;
    public UIElementDragger uiElement;

    private void Start()
    {
        currentQuestion.text = Global.rankingRoundQuestions[Global.currentRoundNumber];

        for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
        {
            if (PhotonNetwork.playerList[i].CustomProperties["JoinNumber"] != null)
            {
                int playerNum = int.Parse(PhotonNetwork.playerList[i].CustomProperties["JoinNumber"].ToString());
                if (playerNum != -1 && !PhotonNetwork.playerList[i].NickName.Equals(""))
                {
                    GameObject newNameBlock = Instantiate(nameBlock, nameBlockHeader.transform);
                    newNameBlock.name = PhotonNetwork.playerList[i].NickName;

                    newNameBlock.transform.GetChild(1).GetComponent<Text>().text = PhotonNetwork.playerList[i].NickName;
                    newNameBlock.transform.GetChild(2).GetComponent<Image>().sprite = GetComponent<PossibleCharacterInfo>().characterPictures[0];
                }
            }
        }
    }

    public void SubmitRankingOrder()
    {
        submitRanking.SendRankingNames(uiElement.GetPlayerListOrder().ToArray());
        SceneManager.LoadScene("Controller03_WaitingScreen");
    }
}
