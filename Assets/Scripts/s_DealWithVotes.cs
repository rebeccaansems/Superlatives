using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_DealWithVotes : MonoBehaviour
{
    public g_VibratePhoneFlashScreen sendVibrate;

    public void sendVibrations(int playerNum)
    {
        List<int> vibbedPlayers = new List<int>();
        vibbedPlayers.Add(playerNum);

        for (int i = 0; i < s_global.allPlayers.Count / 3 - 1; i++)
        {
            int vibPlayerNum = Random.Range(0, s_global.allPlayers.Count - 1);
            while (vibbedPlayers.Contains(vibPlayerNum))
            {
                vibPlayerNum = Random.Range(0, s_global.allPlayers.Count - 1);
            }
        }

        for (int i = 0; i < vibbedPlayers.Count; i++)
        {
            Debug.Log("[GAME] Sending vibrations to player: " + i);
            sendVibrate.SendVibrateFlashToPhone(s_global.allPlayers[i]);
        }
    }
}
