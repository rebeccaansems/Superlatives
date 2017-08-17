using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Text text;

    private int rankingRoundNumber = 1;
    private int playerScore;

    public void ScorePlayers(int playerNum, string[] playerRankings)
    {
        int playerScore = this.GetComponent<ScorePlayers>().ScorePlayerRanking(rankingRoundNumber, playerRankings);

        for (int i = 0; i < Global.allPlayers.Count; i++)
        {
            if (playerNum == int.Parse(Global.allPlayers[i].CustomProperties["JoinNumber"].ToString()))
            {
                Global.allPlayers[i].CustomProperties["Score"] = (int.Parse(Global.allPlayers[i].CustomProperties["Score"].ToString()) + playerScore).ToString();
                break;
            }
        }
    }
}
