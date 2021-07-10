using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameSceneManager : MonoBehaviour
{
    public int enemyCount;
    public int stageLevel;
    GameObject resultText;
    public bool isPlayerDie;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        resultText = GameObject.Find("ResultText");
        resultText.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount <= 0)
        {
            ClearStage();
            Invoke("LoadLobbyScene", 3.0f);
        }

        if (isPlayerDie)
        {
            FailStage();
            Invoke("LoadLobbyScene", 3.0f);
        }

    }

    void ClearStage()
    {
        int currentPlayerLevel = PlayerPrefs.GetInt("PLAYER_LEVEL", 0);
        int currentStageScore = PlayerPrefs.GetInt($"SCORE_{stageLevel}", 0);
        int resultScore = CalculateResultScore();

        if (stageLevel > currentPlayerLevel)
        {
            PlayerPrefs.SetInt("PLAYER_LEVEL", stageLevel);
        }
        if (resultScore > currentStageScore)
        {
            PlayerPrefs.SetInt($"SCORE_{stageLevel}", resultScore);
        }
        PlayerPrefs.Save();
        
        resultText.SetActive (true);
        resultText.GetComponent<Text>().text = "Game Clear!!";
    }

    void FailStage()
    {
        resultText.SetActive (true);
        resultText.GetComponent<Text>().text = "Defeted";
    }

    void LoadLobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }

    int CalculateResultScore()
    {
        // スコアの計算方法を後日実装
        int result = 2;
        return result;
    }
}
