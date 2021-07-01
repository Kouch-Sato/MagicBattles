using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LobbySceneManager : MonoBehaviour
{
    public void OnClickStageButton()
    {
        // SceneManager.LoadScene(button.GetComponent<LobbyButtonManager>().targetStageName);
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
}
