using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTitleManager : MonoBehaviour
{
    public GameObject canvas;
    void Start()
    {
        Invoke("LoadLobbyScene", 4.0f);
    }

    void Update()
    {
        var pos = canvas.transform.position;
        var speed = 0.2f;
        canvas.transform.position = new Vector3(pos.x, pos.y, pos.z - speed);
    }

    void LoadLobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }
}
