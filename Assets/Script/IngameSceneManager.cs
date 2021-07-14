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
    GameObject outgameUICanvas;
    public bool isPlayerDie;
    float startTime;
    bool gameFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        outgameUICanvas = GameObject.Find("OutgameUICanvas");
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameFinished)
        {
            if (enemyCount <= 0)
            {
                Invoke("ClearStage", 3.0f);
                Invoke("LoadLobbyScene", 8.0f);
            }

            if (isPlayerDie)
            {
                Invoke("FailStage", 3.0f);
                Invoke("LoadLobbyScene", 8.0f);
            }
        }
    }

    void ClearStage()
    {
        gameFinished = true;
        GameObject.Find("PlayerUICanvas").SetActive(false);

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

        GameObject clearPanel = outgameUICanvas.transform.Find("ClearPanel").gameObject;
        clearPanel.SetActive(true);
        clearPanel.transform.Find("WoodenShield").Find($"Star_{resultScore}").gameObject.SetActive(true);
        clearPanel.transform.Find("StageLevelRibbon").Find("Text").GetComponent<Text>().text = $"Stage {stageLevel}";
    }

    void FailStage()
    {
        gameFinished = true;
        GameObject.Find("PlayerUICanvas").SetActive(false);
        outgameUICanvas.transform.Find("FailPanel").gameObject.SetActive(true);
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
