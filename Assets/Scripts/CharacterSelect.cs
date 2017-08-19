using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public InputField playerName;
    public GameObject[] playerQuestions;
    public GameObject submitCharPanel, submitPlayerInfoButton, startGamePanel,
        nameBlockHeader, nameBlock, startGameButton, questionPanelParent;

    public GameStart gameStart;

    private List<Text> playerAnswerTexts;

    private int previousNumPlayers = 1;
    private bool[] hasBeenAdded = new bool[12];

    public void Start()
    {
        playerAnswerTexts = new List<Text>();
        StartCoroutine(SetupQuestions());
    }

    private IEnumerator SetupQuestions()
    {
        while (Global.rankingRoundQuestions == null)
        {
            yield return null;
        }

        for (int i = 0; i < 4; i++)
        {
            GameObject newQuestion = Instantiate(playerQuestions[0], questionPanelParent.transform);

            Text questionText = newQuestion.transform.GetChild(0).transform.GetComponent<Text>();
            questionText.text = Global.rankingRoundQuestions[i];

            Text answerText = newQuestion.transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<Text>();
            playerAnswerTexts.Add(answerText);
        }
        Debug.Log(Global.rankingRoundQuestions[0] + " " + Global.rankingRoundQuestions[1]);
    }

    public void SubmitPlayerInfo()
    {
        ExitGames.Client.Photon.Hashtable playerInfo = new ExitGames.Client.Photon.Hashtable();
        playerInfo.Add("PlayerAnswer0", playerAnswerTexts[0].text);
        playerInfo.Add("PlayerAnswer1", playerAnswerTexts[1].text);
        playerInfo.Add("PlayerAnswer2", playerAnswerTexts[2].text);
        playerInfo.Add("PlayerAnswer3", playerAnswerTexts[3].text);
        playerInfo.Add("Score", 0);
        PhotonNetwork.player.SetCustomProperties(playerInfo);

        PhotonNetwork.playerName = playerName.text;
        submitCharPanel.SetActive(false);
        startGamePanel.SetActive(true);

        if (PhotonNetwork.player.ID == 2)
        {
            Debug.Log("[PHOTON] Player is first to join");
            startGameButton.SetActive(true);
        }
        else
        {
            Debug.Log("[PHOTON] Player is not first to join");
            startGameButton.SetActive(false);
        }
    }

    public void StartGame()
    {
        gameStart.SendGameHasStarted();
        SceneManager.LoadScene("Controller02_RankingRound");
    }

    private void Update()
    {
        if (startGamePanel.GetActive())
        {
            for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
            {
                if (PhotonNetwork.playerList[i].CustomProperties["JoinNumber"] != null)
                {
                    int playerNum = int.Parse(PhotonNetwork.playerList[i].CustomProperties["JoinNumber"].ToString());
                    if (playerNum != -1 && !hasBeenAdded[playerNum] && !PhotonNetwork.playerList[i].NickName.Equals(""))
                    {
                        hasBeenAdded[playerNum] = true;
                        GameObject newNameBlock = Instantiate(nameBlock, nameBlockHeader.transform);
                        Global.allPlayers.Add(PhotonNetwork.playerList[i]);

                        newNameBlock.transform.GetChild(0).GetComponent<Text>().text = PhotonNetwork.playerList[i].NickName;
                        newNameBlock.transform.GetChild(1).GetComponent<Image>().sprite = GetComponent<PossibleCharacterInfo>().characterPictures[0];
                        previousNumPlayers = PhotonNetwork.room.PlayerCount;
                    }
                }
            }

            startGameButton.GetComponent<Button>().interactable = (PhotonNetwork.room.PlayerCount - 1 == Global.allPlayers.Count);
        }
        else
        {
            submitPlayerInfoButton.GetComponent<Button>().interactable = !playerName.text.Equals("") &&
                !(playerAnswerTexts[0].text.Equals("") || playerAnswerTexts[1].text.Equals("") ||
                playerAnswerTexts[2].text.Equals("") || playerAnswerTexts[3].text.Equals(""));
        }
    }
}
