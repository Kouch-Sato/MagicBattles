using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    Color lockedButtonColor = new Color(147f / 255f, 147f / 255f, 147f / 255f, 255f/255f);
    Color lockedButtonTextColor = new Color(144f / 255f, 107f / 255f, 40f / 255f, 255f/255f);
    void Start()
    {
        // PlayerPrefs.SetInt("PLAYER_LEVEL", 3);
        // PlayerPrefs.Save();

        int playerLevel = PlayerPrefs.GetInt("PLAYER_LEVEL", 0);

        SetLobbyStageButton(playerLevel);
    }

    void SetLobbyStageButton(int playerLevel)
    {
        GameObject[] lobbyStageButtons = GameObject.FindGameObjectsWithTag("LobbyStageButton");
        foreach(GameObject button in lobbyStageButtons)
        {
            int targetStageLevel = button.GetComponent<LobbyButtonManager>().targetStageLevel;
            int stageScore = PlayerPrefs.GetInt($"SCORE_{targetStageLevel}", 0);

            if (targetStageLevel == 11)
            {
                if (CanUnlockSecretStage())
                {
                    GameObject.Find("SpecialStageButton").SetActive(true);
                    GameObject.Find("SpecialStageDummy").SetActive(false);
                }
                else
                {
                    GameObject.Find("SpecialStageButton").SetActive(false);
                    GameObject.Find("SpecialStageDummy").SetActive(true);
                }
            }
            else if (targetStageLevel <= playerLevel)
            {
                button.transform.Find($"Star_{stageScore}").gameObject.SetActive(true);
            }
            else if (targetStageLevel == playerLevel + 1)
            {
                button.transform.Find("Unlock").gameObject.SetActive(true);
            }
            else
            {
                button.GetComponent<Image>().color = lockedButtonColor;
                button.transform.Find("Text_Normal").GetComponent<Text>().color = lockedButtonTextColor;
                button.transform.Find("Text_Pressed").GetComponent<Text>().color = lockedButtonTextColor;
                button.GetComponent<Button>().interactable = false;
                button.transform.Find("Lock").gameObject.SetActive(true);
            }
        }
    }

    bool CanUnlockSecretStage()
    {
        for (int i=1; i <= 10; i++)
        {
            if (PlayerPrefs.GetInt($"SCORE_{i}", 0) != 3)
            {
                return false;
            }
        }
        return true;
    }
}
