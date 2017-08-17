using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Text text;
    public StartNextRound nextRound;

    private int playerScore, playersScored = 0;

    public void ScorePlayers(int playerNum, string[] playerRankings)
    {
        int playerScore = this.GetComponent<ScorePlayers>().ScorePlayerRanking(Global.currentRoundNumber, playerRankings);

        for (int i = 0; i < Global.allPlayers.Count; i++)
        {
            if (playerNum == int.Parse(Global.allPlayers[i].CustomProperties["JoinNumber"].ToString()))
            {
                Global.allPlayers[i].CustomProperties["Score"] = (int.Parse(Global.allPlayers[i].CustomProperties["Score"].ToString()) + playerScore).ToString();
                playersScored++;
                break;
            }
        }
    }

    private void Update()
    {
        if (playersScored == Global.allPlayers.Count)
        {
            StartNextRound();
            playersScored = 0;
        }
    }

    public void DisplayPlayersRankings()
    {
    }

    public void StartNextRound()
    {
        Global.currentRoundNumber++;
        nextRound.NextRound();
    }
}
