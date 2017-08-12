using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class c_CharacterSelect : MonoBehaviour
{
    public InputField playerName;
    public GameObject[] playerQuestions;
    public GameObject submitCharPanel, submitPlayerInfoButton, startGamePanel,
        nameBlockHeader, nameBlock, startGameButton, questionPanelParent;

    public g_GameStart gameStart;

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
        Debug.Log("[GAME] 213");
        while (gc_SuperlativeQuestions.round1Questions == null)
        {
            yield return null;
        }
        Debug.Log("[GAME] 325325");

        for (int i = 0; i < 4; i++)
        {
            GameObject newQuestion = Instantiate(playerQuestions[0], questionPanelParent.transform);

            Text questionText = newQuestion.transform.GetChild(0).transform.GetComponent<Text>();
            questionText.text = gc_SuperlativeQuestions.round1Questions[i];

            Text answerText = newQuestion.transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<Text>();
            playerAnswerTexts.Add(answerText);
        }
    }

    public void SubmitPlayerInfo()
    {
        ExitGames.Client.Photon.Hashtable playerInfo = new ExitGames.Client.Photon.Hashtable();
        playerInfo.Add("PlayerAnswer1", playerAnswerTexts[0].text);
        playerInfo.Add("PlayerAnswer2", playerAnswerTexts[1].text);
        playerInfo.Add("PlayerAnswer3", playerAnswerTexts[2].text);
        playerInfo.Add("PlayerAnswer4", playerAnswerTexts[3].text);
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
        SceneManager.LoadScene("Controller02_SendVibrate");
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
                        s_global.allPlayers.Add(PhotonNetwork.playerList[i]);

                        newNameBlock.transform.GetChild(0).GetComponent<Text>().text = PhotonNetwork.playerList[i].NickName;
                        newNameBlock.transform.GetChild(1).GetComponent<Image>().sprite = GetComponent<c_PossibleCharacterInfo>().characterPictures[0];
                        previousNumPlayers = PhotonNetwork.room.PlayerCount;
                    }
                }
            }

            startGameButton.GetComponent<Button>().interactable = (PhotonNetwork.room.PlayerCount - 1 == s_global.allPlayers.Count);
        }
        else
        {
            submitPlayerInfoButton.GetComponent<Button>().interactable = !playerName.text.Equals("") &&
                !(playerAnswerTexts[0].text.Equals("") || playerAnswerTexts[1].text.Equals("") ||
                playerAnswerTexts[2].text.Equals("") || playerAnswerTexts[3].text.Equals(""));
        }
    }
}
