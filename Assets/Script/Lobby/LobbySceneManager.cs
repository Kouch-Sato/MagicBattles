using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LobbySceneManager : MonoBehaviour
{
    public void OnClickStageButton()
    {
        var  buttonObject = EventSystem.current.currentSelectedGameObject;
        var sceneName = buttonObject.GetComponent<LobbyButtonManager>().targetStageName;
        SceneManager.LoadScene(sceneName);
    }

    public void OnClickResetButton()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("Lobby");
    }

    public void OnClickCompleteButton()
    {
        PlayerPrefs.SetInt("PLAYER_LEVEL", 10);
        for (int i=1; i <= 10; i++)
        {
            PlayerPrefs.SetInt($"SCORE_{i}", 3);
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene("Lobby");
    }
}
