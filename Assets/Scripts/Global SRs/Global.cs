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
    public static int currentSectionOfGame = 0;
    /// <summary>
    /// 00 - Started game
    /// 01 - Joined game, loaded into character select
    /// 02 - Submitted character, is first to join
    /// 03 - Submitted character, is not first to join
    /// 04 - Started game, ranking round
    /// 05 - Started game, most liklely round
    /// 06 - Submitted guess for ranking round
    /// 07 - Submitted guess for most liklely round
    /// 08 - 
    /// 09 - 
    /// </summary>
    public static bool isRankingRound = true;
}
