using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    int rankingRoundNumber = 1;
    int playerScore;
    public Text text;

    public void ScorePlayers(string[] playerRankings)
    {
        playerScore = this.GetComponent<ScorePlayers>().ScorePlayerRanking(rankingRoundNumber, playerRankings);
        text.text = playerScore.ToString();
    }
}
