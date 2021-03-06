﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateGame : MonoBehaviour
{
    public const string VERSION = "0.1";

    public TextAsset json;
    public Text roomCodeText;
    public SelectSuperlativeQuestions selectSuperlativeQuestions;

    public string roomCode { get; private set; }

    private SuperlativeQuestionsList SQL;

    private bool playerJoinedRoom = true;
    private int previousNumberPlayers = 1;
    private int[] rankingQuestions = new int[4];

    void Start()
    {
        SQL = new SuperlativeQuestionsList(json.text);

        roomCode = getRandomWord();
        roomCodeText.text = roomCode;

        PhotonNetwork.autoJoinLobby = false;

        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(VERSION);
        }
    }

    private string getRandomWord()
    {
        string possibleLetters = "QWERTYUIOPASDFGHJKLZXCVBNM";
        string word = "";
        word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        //word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        //word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        //word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        //word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        //word += possibleLetters[Random.Range(0, possibleLetters.Length)];

        return word;
    }

    public void GameHasStarted()
    {
        Debug.Log("[PHOTON] Game has started");

        PhotonNetwork.room.IsOpen = false;
        SceneManager.LoadScene("Server01_MainGame");
    }

    private void Update()
    {
        if (PhotonNetwork.connectionStateDetailed.ToString().Equals("ConnectedToMaster") && playerJoinedRoom)
        {
            Debug.Log("[PHOTON] Room Created: " + roomCode);
            PhotonNetwork.JoinOrCreateRoom(roomCode, new RoomOptions() { MaxPlayers = 12, PlayerTtl = 600000 }, TypedLobby.Default);
            PhotonNetwork.playerName = "";

            ExitGames.Client.Photon.Hashtable playerInfo = new ExitGames.Client.Photon.Hashtable();
            playerInfo.Add("JoinNumber", -1);
            PhotonNetwork.player.SetCustomProperties(playerInfo);

            playerJoinedRoom = false;
        }

        //give players join numbers
        if (PhotonNetwork.inRoom && PhotonNetwork.room.PlayerCount > previousNumberPlayers)
        {
            previousNumberPlayers = PhotonNetwork.room.PlayerCount;
            for (int i = 0; i < PhotonNetwork.otherPlayers.Length; i++)
            {
                if (!Global.allPlayers.Contains(PhotonNetwork.otherPlayers[i]))
                {
                    Global.allPlayers.Add(PhotonNetwork.otherPlayers[i]);

                    ExitGames.Client.Photon.Hashtable playerInfo = new ExitGames.Client.Photon.Hashtable();
                    playerInfo.Add("JoinNumber", i);
                    PhotonNetwork.otherPlayers[i].SetCustomProperties(playerInfo);
                    Debug.Log("[PHOTON] Added new player with join ID: " + PhotonNetwork.otherPlayers[i].CustomProperties["JoinNumber"].ToString());

                    SendQuestions();
                }
            }
        }
    }

    private void SendQuestions()
    {
        if (Global.rankingRoundQuestions == null)
        {
            List<int> questionLocations = new List<int>();
            int currentNum = -1;
            while (questionLocations.Count < 4)
            {
                currentNum = Random.Range(0, SQL.rankingQuestions.Count);
                if (!questionLocations.Contains(currentNum))
                {
                    questionLocations.Add(currentNum);
                }
            }

            Global.rankingRoundQuestions = new List<string>();
            Global.rankingRoundQuestions.Add(SQL.rankingQuestions[questionLocations[0]].superlativeQuestion);
            Global.rankingRoundQuestions.Add(SQL.rankingQuestions[questionLocations[1]].superlativeQuestion);
            Global.rankingRoundQuestions.Add(SQL.rankingQuestions[questionLocations[2]].superlativeQuestion);
            Global.rankingRoundQuestions.Add(SQL.rankingQuestions[questionLocations[3]].superlativeQuestion);


            questionLocations = new List<int>();
            currentNum = -1;
            while (questionLocations.Count < 4)
            {
                currentNum = Random.Range(0, SQL.rankingQuestions.Count);
                if (!questionLocations.Contains(currentNum))
                {
                    questionLocations.Add(currentNum);
                }
            }

            Global.mostLikelyQuestions = new List<string>();
            Global.mostLikelyQuestions.Add(SQL.mostLikelyQuestions[questionLocations[0]].superlativeQuestion);
            Global.mostLikelyQuestions.Add(SQL.mostLikelyQuestions[questionLocations[1]].superlativeQuestion);
            Global.mostLikelyQuestions.Add(SQL.mostLikelyQuestions[questionLocations[2]].superlativeQuestion);
            Global.mostLikelyQuestions.Add(SQL.mostLikelyQuestions[questionLocations[3]].superlativeQuestion);
        }

        selectSuperlativeQuestions.SendSuperlativeQuestion(new string[8]
            { Global.rankingRoundQuestions[0], Global.rankingRoundQuestions[1], Global.rankingRoundQuestions[2], Global.rankingRoundQuestions[3],
            Global.mostLikelyQuestions[0],Global.mostLikelyQuestions[1],Global.mostLikelyQuestions[2],Global.mostLikelyQuestions[3] });
    }
}
