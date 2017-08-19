using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public StartNextRound nextRound;
    public GameObject currentModePanel, showingScorePanel, playerRankingPanel, playerRankingPanelParent, playerScoringBlock;
    public Text currentQuestion, currentScore;
    public AnimationClip animatePanelIn;

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

        Array.Reverse(playerRankings);

        Global.playerRankGuesses[playerNum] = new List<string>();
        for (int i = 0; i < playerRankings.Length; i++)
        {
            Global.playerRankGuesses[playerNum].Add(playerRankings[i]);
        }
    }

    private void Update()
    {
        if (playersScored == Global.allPlayers.Count)
        {
            DisplayPlayersRankings();
            playersScored = 0;
        }
    }

    public void DisplayPlayersRankings()
    {
        currentModePanel.SetActive(false);
        showingScorePanel.SetActive(true);

        currentQuestion.text = Global.rankingRoundQuestions[Global.currentRoundNumber];

        StartCoroutine(ShowPlayerScores());
    }

    private IEnumerator ShowPlayerScores()
    {
        for (int i = 0; i < Global.allPlayers.Count; i++)
        {
            GameObject newPlayerScore = Instantiate(playerRankingPanel);
            newPlayerScore.GetComponent<Animation>().Stop();

            newPlayerScore.transform.SetParent(playerRankingPanelParent.transform, false);
            newPlayerScore.transform.GetChild(1).GetComponent<Text>().text = Global.allPlayers[i].NickName;

            Transform playerScoringParent = newPlayerScore.transform.GetChild(2).GetChild(0).transform;
            for (int j = 0; j < Global.allPlayers.Count; j++)
            {
                GameObject newPlayerScoreBlock = Instantiate(playerScoringBlock, playerScoringParent);
                newPlayerScoreBlock.transform.GetChild(1).GetComponent<Text>().text = Global.playerRankGuesses[i][j];
                newPlayerScoreBlock.transform.GetChild(2).GetComponent<Image>().sprite = this.GetComponent<PossibleCharacterInfo>().characterPictures[0];
            }

            yield return new WaitForSeconds(2);

            newPlayerScore.GetComponent<Animator>().SetBool("PlayAnimIn", true);

            yield return new WaitForSeconds(8);
        }

        yield return new WaitForSeconds(1);
    }

    public void StartNextRound()
    {
        Global.currentRoundNumber++;
        nextRound.NextRound();
    }
}
