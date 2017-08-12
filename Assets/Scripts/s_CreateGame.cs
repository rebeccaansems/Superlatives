using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class s_CreateGame : MonoBehaviour
{
    public const string VERSION = "0.1";


    public Text roomCodeText;

    public string roomCode { get; private set; }

    private bool playerJoinedRoom = true;
    private int previousNumberPlayers = 1;

    void Start()
    {
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
        SceneManager.LoadScene("Server01_GameStarted");
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
                if (!s_global.allPlayers.Contains(PhotonNetwork.otherPlayers[i]))
                {
                    s_global.allPlayers.Add(PhotonNetwork.otherPlayers[i]);

                    ExitGames.Client.Photon.Hashtable playerInfo = new ExitGames.Client.Photon.Hashtable();
                    playerInfo.Add("JoinNumber", i);
                    PhotonNetwork.otherPlayers[i].SetCustomProperties(playerInfo);
                    Debug.Log("[PHOTON] Added new player with join ID: " + PhotonNetwork.otherPlayers[i].CustomProperties["JoinNumber"].ToString());
                }
            }
        }
    }
}
