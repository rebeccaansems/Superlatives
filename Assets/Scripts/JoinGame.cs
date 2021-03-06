﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text.RegularExpressions;
using System;

public class JoinGame : MonoBehaviour
{
    public const string VERSION = "0.1";

    public Text infoText;
    public TMP_InputField roomCodeInput;

    private string joinRoomCode;
    private bool roomExists = false;

    void Start()
    {
        TouchScreenKeyboard.hideInput = true;
        PhotonNetwork.autoJoinLobby = true; 

        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(VERSION);
        }
    }

    public void BeginJoinGame()
    {
        joinRoomCode = roomCodeInput.text.ToUpper();

        RoomInfo[] rooms = PhotonNetwork.GetRoomList();

        infoText.color = Color.green;
        infoText.text = "Attempting to join: " + joinRoomCode;

        for (int i = 0; i < rooms.Length; i++)
        {
            Debug.Log(String.Equals(joinRoomCode, rooms[i].Name, StringComparison.InvariantCultureIgnoreCase));
            if (rooms[i].Name.Equals(joinRoomCode) && rooms[i].IsOpen)
            {
                Debug.Log("[PHOTON] Joined: " + joinRoomCode);
                PhotonNetwork.JoinRoom(joinRoomCode);
                roomExists = true;

                infoText.color = Color.green;
                infoText.text = "Joined: " + joinRoomCode;

                Global.currentSectionOfGame = 1;
                SceneManager.LoadScene("Controller01_CharacterSelect");
                break;
            }
        }

        if (!roomExists)
        {
            Debug.Log("[PHOTON] Failed to join: " + joinRoomCode);

            infoText.color = Color.red;
            infoText.text = "Room code " + joinRoomCode + " does not exist.";
        }
    }
}
