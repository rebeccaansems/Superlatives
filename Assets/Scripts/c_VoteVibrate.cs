using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class c_VoteVibrate : MonoBehaviour
{
    public GameObject voteBlock, voteBlockHeader;
    public Sprite emptyTickBox, tickedBox;
    public Image[] allTickBoxes;

    public g_Vibrate gVib;

    private bool[] votedPlayers;
    private int currentVote;

    private void Awake()
    {
        votedPlayers = new bool[PhotonNetwork.room.PlayerCount - 1];
    }

    private void Start()
    {
        LoadPlayers();
    }

    private void VotePlayer(int playerVoteNum)
    {
        if (playerVoteNum == currentVote)
        {
            if (votedPlayers[playerVoteNum])
            {
                allTickBoxes[playerVoteNum].sprite = emptyTickBox;
            }
            else
            {
                allTickBoxes[playerVoteNum].sprite = tickedBox;
            }

            votedPlayers[playerVoteNum] = !votedPlayers[playerVoteNum];
        }
        else
        {
            if (votedPlayers[playerVoteNum])
            {
                allTickBoxes[playerVoteNum].sprite = emptyTickBox;
                allTickBoxes[currentVote].sprite = tickedBox;
            }
            else
            {
                allTickBoxes[playerVoteNum].sprite = tickedBox;
                allTickBoxes[currentVote].sprite = emptyTickBox;
                votedPlayers[currentVote] = false;
            }

            votedPlayers[playerVoteNum] = true;
        }

        currentVote = playerVoteNum;
    }
    
    public void SendVote()
    {
        Debug.Log("[PHOTON] Player sent vibrate vote: " + currentVote);
        gVib.SendVibrate(currentVote);
    }

    private void LoadPlayers()
    {
        allTickBoxes = new Image[PhotonNetwork.room.PlayerCount - 1];
        for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
        {
            if (PhotonNetwork.playerList[i].CustomProperties["JoinNumber"] != null)
            {
                int playerNum = int.Parse(PhotonNetwork.playerList[i].CustomProperties["JoinNumber"].ToString());
                if (playerNum != -1 && !PhotonNetwork.playerList[i].NickName.Equals(""))
                {
                    GameObject newVoteBlock = Instantiate(voteBlock, voteBlockHeader.transform);
                    int charIndex = int.Parse(PhotonNetwork.playerList[i].CustomProperties["CharacterArtNum"].ToString());

                    newVoteBlock.transform.GetChild(0).GetComponent<Text>().text = PhotonNetwork.playerList[i].NickName;
                    newVoteBlock.transform.GetChild(1).GetComponent<Text>().text = GetComponent<c_PossibleCharacterInfo>().characterSubs[charIndex];
                    newVoteBlock.transform.GetChild(2).GetComponent<Image>().sprite = GetComponent<c_PossibleCharacterInfo>().characterHeadshot[charIndex];

                    newVoteBlock.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => { VotePlayer(playerNum); });

                    votedPlayers[playerNum] = false;
                    allTickBoxes[playerNum] = newVoteBlock.transform.GetChild(3).GetComponent<Image>();
                }
            }
        }
    }
}
