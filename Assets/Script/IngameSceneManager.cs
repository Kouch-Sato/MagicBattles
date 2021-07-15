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
        float clearTime = Time.time - startTime;
        int resultScore = CalculateResultScore((int)clearTime);

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

    int CalculateResultScore(int clearTime)
    {
        int resultScore = 0;
        if (clearTime < 20)
        {
            resultScore = 3;
        }
        else if (clearTime < 40)
        {
            resultScore = 2;
        }
        else if (clearTime < 60)
        {
            resultScore = 1;
        }

        return resultScore;
    }
}
