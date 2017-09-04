using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MostLikelyRound : MonoBehaviour
{
    public TMP_Text currentQuestion;
    public GameObject nameBlockHeader, nameBlock;
    public SubmitMostLikelyNames submitMostLikely;

    private GameObject selectedObject = null;

    private void Start()
    {
        currentQuestion.text = Global.rankingRoundQuestions[Global.currentRoundNumber];

        for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
        {
            if (PhotonNetwork.playerList[i].CustomProperties["JoinNumber"] != null)
            {
                int playerNum = int.Parse(PhotonNetwork.playerList[i].CustomProperties["JoinNumber"].ToString());
                if (playerNum != -1 && !PhotonNetwork.playerList[i].NickName.Equals(""))
                {
                    GameObject newNameBlock = Instantiate(nameBlock, nameBlockHeader.transform);
                    newNameBlock.name = PhotonNetwork.playerList[i].NickName;

                    newNameBlock.GetComponent<Button>().onClick.AddListener(delegate () { SelectElement(newNameBlock); });
                    newNameBlock.transform.GetChild(0).GetComponent<TMP_Text>().text = PhotonNetwork.playerList[i].NickName;
                    newNameBlock.transform.GetChild(1).GetComponent<Image>().sprite = GetComponent<PossibleCharacterInfo>().characterPictures[0];

                    if (playerNum == 0)
                    {
                        newNameBlock.transform.GetChild(2).gameObject.SetActive(true);
                        selectedObject = newNameBlock;
                    }
                }
            }
        }
    }

    private void SelectElement(GameObject self)
    {
        if (self != selectedObject)
        {
            selectedObject.transform.GetChild(2).gameObject.SetActive(false);
            
            self.transform.GetChild(2).gameObject.SetActive(true);
            selectedObject = self;
        }
    }

    public void SubmitMostLikely()
    {
        submitMostLikely.SendMostLikelyName(selectedObject.transform.GetChild(0).GetComponent<TMP_Text>().text);
        SceneManager.LoadScene("Controller04_WaitingScreen");
    }
}
