using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static List<PhotonPlayer> allPlayers = new List<PhotonPlayer>();
    public static int currentRoundNumber = 0;
}
