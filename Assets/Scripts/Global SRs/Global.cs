using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public const int MaxPlayers = 10;

    public static List<PhotonPlayer> allPlayers = new List<PhotonPlayer>();
    public static List<string> rankingRoundQuestions;
    public static List<string> mostLikelyQuestions;
    public static List<string>[] playerRankGuesses = new List<string>[MaxPlayers];
    public static List<string>[] playerMostLikely = new List<string>[MaxPlayers];

    public static int currentRoundNumber = 0;
    public static bool isRankingRound = false;
}
