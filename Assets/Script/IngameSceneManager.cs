using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class IngameSceneManager : MonoBehaviour
{
    public int enemyCount;
    public int stageLevel;
    GameObject resultText;
    public bool isPlayerDie;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        resultText = GameObject.Find("ResultText");
        resultText.SetActive (false);
        startTime = Time.time;
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
        int playerLastHP = GameObject.FindWithTag("Player").GetComponent<PlayerManager>().HP;
        float clearTime = Time.time - startTime;
        int resultScore = CalculateResultScore(playerLastHP, (int)clearTime);

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

    int CalculateResultScore(int playerLastHP, int clearTime)
    {
        int resultScore = 0;
        if (clearTime < 30)
        {
            resultScore = 3;
        }
        else if (clearTime < 60)
        {
            resultScore = 2;
        }
        else if (clearTime < 90)
        {
            resultScore = 1;
        }

        int penaltyScore = 0;
        if (playerLastHP < 1000)
        {
            penaltyScore = 1;
        }
        if (playerLastHP < 600)
        {
            penaltyScore = 2;
        }
        if (playerLastHP < 300)
        {
            penaltyScore = 3;
        }

        return Math.Max(resultScore - penaltyScore, 0);
    }
}
