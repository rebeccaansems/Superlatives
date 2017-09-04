using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScorePlayers : MonoBehaviour
{
    public class Pair<T1, T2>
    {
        public T1 Name { get; private set; }
        public T2 Score { get; set; }
        internal Pair(T1 first, T2 second)
        {
            Name = first;
            Score = second;
        }
    }

    public List<Pair<string, int>> playerMostLikelyVotes;

    private List<Pair<string, int>> correctPlayerRankingOrder;

    private List<Pair<string, int>> SortRankOrder(int roundNumber)
    {
        correctPlayerRankingOrder = new List<Pair<string, int>>();
        for (int i = 0; i < Global.allPlayers.Count; i++)
        {
            correctPlayerRankingOrder.Add(new Pair<string, int>(PhotonNetwork.otherPlayers[i].NickName,
                int.Parse(PhotonNetwork.otherPlayers[i].CustomProperties["PlayerAnswer" + roundNumber].ToString())));
        }

        correctPlayerRankingOrder = correctPlayerRankingOrder.OrderByDescending(x => x.Score).ToList();

        return correctPlayerRankingOrder;
    }

    public int ScorePlayerRanking(int roundNumber, string[] playerRankings)
    {
        List<Pair<string, int>> pairList = SortRankOrder(roundNumber);
        int score = 0;

        for (int i = 0; i < Global.allPlayers.Count; i++)
        {
            if (pairList[i].Name.Equals(playerRankings[i]))
            {
                score++;
            }
        }

        return score;
    }

    public void AddPlayersMostLikelys(string chosenPlayerName)
    {
        if (playerMostLikelyVotes == null)
        {
            SetupMostLikelyLists();
        }

        for (int i = 0; i < Global.allPlayers.Count; i++)
        {
            if (playerMostLikelyVotes[i].Name.Equals(chosenPlayerName))
            {
                playerMostLikelyVotes[i].Score += 1;
            }
        }
        playerMostLikelyVotes = playerMostLikelyVotes.OrderByDescending(x => x.Score).ToList();
    }

    public int ScorePlayerMostLikely(string chosenPlayerName)
    {
        for (int i = 0; i < Global.allPlayers.Count; i++)
        {
            if (playerMostLikelyVotes[i].Name.Equals(chosenPlayerName))
            {
                return playerMostLikelyVotes[i].Score;
            }
        }
        return 0;
    }

    private void SetupMostLikelyLists()
    {
        playerMostLikelyVotes = new List<Pair<string, int>>();
        for (int i = 0; i < Global.allPlayers.Count; i++)
        {
            playerMostLikelyVotes.Add(new Pair<string, int>(Global.allPlayers[i].NickName, 0));
        }
    }

    public bool DidPlayerScore(int currentRankingOrder, string playerName)
    {
        return correctPlayerRankingOrder[currentRankingOrder].Name.Equals(playerName);
    }
}
