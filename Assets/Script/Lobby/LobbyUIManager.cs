using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    Color unlockedStageColor = new Color(224f / 255f, 255f / 255f, 255f / 255f, 255f/255f);
    Color lockedStageColor = new Color(169f / 255f, 169f / 255f, 169f / 255f, 255f/255f);
    void Start()
    {
        // PlayerPrefs.SetInt("PLAYER_LEVEL", 1);
        // PlayerPrefs.Save();
        // Debug.Log(PlayerPrefs.GetInt("PLAYER_LEVEL", 0));

        int playerLevel = PlayerPrefs.GetInt("PLAYER_LEVEL", 0);

        GameObject[] lobbyStageButtons = GameObject.FindGameObjectsWithTag("LobbyStageButton");
        foreach(GameObject button in lobbyStageButtons)
        {
            int targetStageLevel = button.GetComponent<LobbyButtonManager>().targetStageLevel;
            if (targetStageLevel <= playerLevel)
            {
                button.GetComponent<Image>().color = unlockedStageColor;
            }
            else if (targetStageLevel == playerLevel + 1)
            {
                button.GetComponent<Image>().color = unlockedStageColor;
                button.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
            }
            else
            {
                button.GetComponent<Image>().color = lockedStageColor;
                button.GetComponent<Button>().interactable = false;
            }
        }

        GameObject playerLevelText = GameObject.Find("PlayerLevelText");
        playerLevelText.GetComponent<Text>().text = "レベル: " + playerLevel.ToString();
    }
}
