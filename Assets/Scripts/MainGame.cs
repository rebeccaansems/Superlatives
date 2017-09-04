using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public StartNextRound nextRound;
    public GameObject currentModePanel, showingScorePanel, playerRankingPanel, playerMostLikelyPanel, playerPanelParent, playerScoringBlock;
    public Text currentQuestion;
    public AnimationClip animatePanelIn;
    public Sprite rightSprite, wrongSprite;

    private int playersScored = 0;
    private List<Animator> rightWrongAnimators;

    public void ScorePlayersRanking(int playerNum, string[] playerRankings)
    {
        int playerScore = this.GetComponent<ScorePlayers>().ScorePlayerRanking(Global.currentRoundNumber, playerRankings);

        for (int i = 0; i < Global.allPlayers.Count; i++)
        {
            if (playerNum == int.Parse(Global.allPlayers[i].CustomProperties["JoinNumber"].ToString()))
            {
                Global.allPlayers[i].CustomProperties["PrevScore"] = Global.allPlayers[i].CustomProperties["Score"].ToString();
                Global.allPlayers[i].CustomProperties["Score"] = (int.Parse(Global.allPlayers[i].CustomProperties["Score"].ToString()) + playerScore).ToString();
                playersScored++;
                break;
            }
        }

        Global.playerRankGuesses[playerNum] = new List<string>();
        for (int i = 0; i < playerRankings.Length; i++)
        {
            Global.playerRankGuesses[playerNum].Add(playerRankings[i]);
        }
    }

    public void AddPlayersMostLikelys(int submittedPlayerNum, string chosenPlayerName)
    {
        this.GetComponent<ScorePlayers>().AddPlayersMostLikelys(chosenPlayerName);
        Global.allPlayers[submittedPlayerNum].CustomProperties["PlayerMostLikelyVote"] = chosenPlayerName;
        playersScored++;
    }

    private void ScorePlayersMostLikely()
    {
        for (int i = 0; i < Global.allPlayers.Count; i++)
        {
            int playerScore = this.GetComponent<ScorePlayers>().ScorePlayerMostLikely(Global.allPlayers[i].CustomProperties["PlayerMostLikelyVote"].ToString());

            Global.allPlayers[i].CustomProperties["PrevScore"] = Global.allPlayers[i].CustomProperties["Score"].ToString();
            Global.allPlayers[i].CustomProperties["Score"] = (int.Parse(Global.allPlayers[i].CustomProperties["Score"].ToString()) + playerScore).ToString();
        }
    }

    private void Update()
    {
        if (playersScored == Global.allPlayers.Count && Global.isRankingRound)
        {
            DisplayPlayersRankings();
            playersScored = 0;
        }
        else if (playersScored == Global.allPlayers.Count && !Global.isRankingRound)
        {
            ScorePlayersMostLikely();
            DisplayPlayersMostLikely();
            playersScored = 0;
        }
    }

    private void DisplayPlayersRankings()
    {
        currentModePanel.SetActive(false);
        showingScorePanel.SetActive(true);

        currentQuestion.text = Global.rankingRoundQuestions[Global.currentRoundNumber];

        StartCoroutine(ShowPlayerScoresRanking());
    }

    private void DisplayPlayersMostLikely()
    {
        currentModePanel.SetActive(false);
        showingScorePanel.SetActive(true);

        currentQuestion.text = Global.mostLikelyQuestions[Global.currentRoundNumber];

        StartCoroutine(ShowPlayerScoresMostLikely());
    }

    private IEnumerator ShowPlayerScoresRanking()
    {
        GameObject oldPlayerScore = null;
        for (int i = 0; i < Global.allPlayers.Count; i++)
        {
            GameObject newPlayerScore = Instantiate(playerRankingPanel);
            newPlayerScore.transform.SetParent(playerPanelParent.transform, false);
            newPlayerScore.transform.GetChild(1).GetComponent<Text>().text = Global.allPlayers[i].NickName;

            Transform playerScoringParent = newPlayerScore.transform.GetChild(2).GetChild(0).transform;
            rightWrongAnimators = new List<Animator>();
            for (int j = 0; j < Global.allPlayers.Count; j++)
            {
                GameObject newPlayerScoreBlock = Instantiate(playerScoringBlock, playerScoringParent);
                newPlayerScoreBlock.transform.GetChild(1).GetComponent<Text>().text = Global.playerRankGuesses[i][j];
                newPlayerScoreBlock.transform.GetChild(2).GetComponent<Image>().sprite = this.GetComponent<PossibleCharacterInfo>().characterPictures[0];
                newPlayerScoreBlock.transform.GetChild(4).GetComponent<Text>().text = "";

                rightWrongAnimators.Add(newPlayerScoreBlock.GetComponentInChildren<Animator>());
            }

            yield return new WaitForSeconds(2);

            newPlayerScore.GetComponent<Animator>().SetBool("PlayAnimIn", true);

            yield return new WaitForSeconds(1);

            if (oldPlayerScore != null)
            {
                Destroy(oldPlayerScore.gameObject);
            }

            for (int j = 0; j < Global.allPlayers.Count; j++)
            {
                if (GetComponent<ScorePlayers>().DidPlayerScore(j, Global.playerRankGuesses[i][j]))
                {
                    rightWrongAnimators[j].transform.gameObject.GetComponent<Image>().sprite = rightSprite;
                    rightWrongAnimators[j].SetInteger("PlayAnimIn", 1);
                }
                else
                {
                    rightWrongAnimators[j].transform.gameObject.GetComponent<Image>().sprite = wrongSprite;
                    rightWrongAnimators[j].SetInteger("PlayAnimIn", 2);
                }

                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(1);
            oldPlayerScore = newPlayerScore;
        }

        yield return new WaitForSeconds(1);

        StartCoroutine("ShowRankingCorrectOrder");
    }

    private IEnumerator ShowRankingCorrectOrder()
    {
        GameObject newPlayerScore = Instantiate(playerRankingPanel);
        newPlayerScore.transform.SetParent(playerPanelParent.transform, false);
        newPlayerScore.transform.GetChild(1).GetComponent<Text>().text = "Correct Order";

        Transform playerScoringParent = newPlayerScore.transform.GetChild(2).GetChild(0).transform;
        for (int j = 0; j < Global.allPlayers.Count; j++)
        {
            GameObject newPlayerScoreBlock = Instantiate(playerScoringBlock, playerScoringParent);
            newPlayerScoreBlock.transform.GetChild(1).GetComponent<Text>().text = this.GetComponent<ScorePlayers>().correctPlayerRankingOrder[j].Name;
            newPlayerScoreBlock.transform.GetChild(2).GetComponent<Image>().sprite = this.GetComponent<PossibleCharacterInfo>().characterPictures[0];
            newPlayerScoreBlock.transform.GetChild(4).GetComponent<Text>().text = this.GetComponent<ScorePlayers>().correctPlayerRankingOrder[j].Score.ToString();

            rightWrongAnimators.Add(newPlayerScoreBlock.GetComponentInChildren<Animator>());
        }

        yield return new WaitForSeconds(2);

        newPlayerScore.GetComponent<Animator>().SetBool("PlayAnimIn", true);

        yield return new WaitForSeconds(4);

        StartCoroutine("ShowAllPlayersScores");
    }

    private IEnumerator ShowPlayerScoresMostLikely()
    {
        GameObject newPlayerScore = Instantiate(playerMostLikelyPanel);
        newPlayerScore.transform.SetParent(playerPanelParent.transform, false);

        Transform playerScoringParent = newPlayerScore.transform.GetChild(2).GetChild(0).transform;
        for (int j = 0; j < Global.allPlayers.Count; j++)
        {
            GameObject newPlayerScoreBlock = Instantiate(playerScoringBlock, playerScoringParent);
            newPlayerScoreBlock.transform.GetChild(1).GetComponent<Text>().text = this.GetComponent<ScorePlayers>().playerMostLikelyVotes[j].Name;
            newPlayerScoreBlock.transform.GetChild(2).GetComponent<Image>().sprite = this.GetComponent<PossibleCharacterInfo>().characterPictures[0];
            newPlayerScoreBlock.transform.GetChild(4).GetComponent<Text>().text = this.GetComponent<ScorePlayers>().playerMostLikelyVotes[j].Score.ToString();
        }

        yield return new WaitForSeconds(2);

        newPlayerScore.GetComponent<Animator>().SetBool("PlayAnimIn", true);

        yield return new WaitForSeconds(7);

        StartCoroutine("ShowAllPlayersScores");
    }

    private IEnumerator ShowAllPlayersScores()
    {
        if (Global.currentRoundNumber == 3)
        {
            List<ScorePlayers.Pair<string, int>> playersInScoreOrder = new List<ScorePlayers.Pair<string, int>>();

            for (int j = 0; j < Global.allPlayers.Count; j++)
            {
                playersInScoreOrder.Add(new ScorePlayers.Pair<string, int>(Global.allPlayers[j].NickName, int.Parse(Global.allPlayers[j].CustomProperties["Score"].ToString())));
            }
            playersInScoreOrder = playersInScoreOrder.OrderByDescending(x => x.Score).ToList();

            GameObject newPlayerScore = Instantiate(playerMostLikelyPanel);
            newPlayerScore.transform.GetChild(1).GetComponent<Text>().text = "Total Scores";
            newPlayerScore.transform.SetParent(playerPanelParent.transform, false);

            Transform playerScoringParent = newPlayerScore.transform.GetChild(2).GetChild(0).transform;
            for (int j = 0; j < Global.allPlayers.Count; j++)
            {
                GameObject newPlayerScoreBlock = Instantiate(playerScoringBlock, playerScoringParent);
                newPlayerScoreBlock.transform.GetChild(1).GetComponent<Text>().text = playersInScoreOrder[j].Name;
                newPlayerScoreBlock.transform.GetChild(2).GetComponent<Image>().sprite = this.GetComponent<PossibleCharacterInfo>().characterPictures[0];
                newPlayerScoreBlock.transform.GetChild(4).GetComponent<Text>().text = playersInScoreOrder[j].Score.ToString();
            }

            yield return new WaitForSeconds(2);

            newPlayerScore.GetComponent<Animator>().SetBool("PlayAnimIn", true);

            yield return new WaitForSeconds(7);
        }
        
        StartNextRound();
    }

    public void StartNextRound()
    {
        Global.currentRoundNumber++;

        if (Global.currentRoundNumber == 4)
        {
            Global.isRankingRound = false;
            Global.currentRoundNumber = 0;
        }

        nextRound.NextRound();

        this.GetComponent<ScorePlayers>().playerMostLikelyVotes = null;

        currentModePanel.SetActive(true);
        showingScorePanel.SetActive(false);

        if (Global.isRankingRound)
        {
            currentModePanel.transform.GetChild(1).GetComponent<Text>().text = "RANKING";
        }
        else
        {
            currentModePanel.transform.GetChild(1).GetComponent<Text>().text = "PICKING";
        }

        foreach (Transform child in playerPanelParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
