using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScorePlayers : MonoBehaviour
{
    public class Pair<T1, T2>
    {
        public T1 Name { get; private set; }
        public T2 Score { get; private set; }
        internal Pair(T1 first, T2 second)
        {
            Name = first;
            Score = second;
        }
    }

    private List<Pair<string, int>> correctPlayerRankingOrder;

    private List<Pair<string, int>> SortRankOrder(int roundNumber)
    {
        correctPlayerRankingOrder = new List<Pair<string, int>>();
        for (int i = 0; i < Global.allPlayers.Count; i++)
        {
            correctPlayerRankingOrder.Add(new Pair<string, int>(PhotonNetwork.otherPlayers[i].NickName,
                int.Parse(PhotonNetwork.otherPlayers[i].CustomProperties["PlayerAnswer" + roundNumber].ToString())));
        }

        correctPlayerRankingOrder = correctPlayerRankingOrder.OrderBy(x => x.Score).ToList();
        correctPlayerRankingOrder.Reverse();

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
                score += roundNumber + 1;
            }
        }
        return score;
    }

    public bool DidPlayerScore(int currentRankingOrder, string playerName)
    {
        return correctPlayerRankingOrder[currentRankingOrder].Name.Equals(playerName);
    }
}
