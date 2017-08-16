using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScorePlayers : MonoBehaviour
{
    public class Pair<T1, T2>
    {
        public T1 First { get; private set; }
        public T2 Second { get; private set; }
        internal Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }
    }

    private List<Pair<string, int>> pairList = new List<ScorePlayers.Pair<string, int>>();

    public void SortRankOrder(int questionNumber)
    {
        for (int i = 0; i < Global.allPlayers.Count; i++)
        {
            pairList.Add(new Pair<string, int>(PhotonNetwork.otherPlayers[i].NickName,
                int.Parse(PhotonNetwork.otherPlayers[i].CustomProperties["PlayerAnswer" + questionNumber].ToString())));
        }

        pairList = pairList.OrderBy(x => x.Second).ToList();
    }

    public void ScorePlayerRanking(string[] playerRankings)
    {
    }
}
