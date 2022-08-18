using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
   
    //main script
    [Header("Game Elements")]
    public int score;
    public int coin;
    [SerializeField] int scoreAmount;
    public int health;
    [HideInInspector]public bool gameStart, gamefinish, gameOver;

    [Header("UI Elements")]
     public GameObject startpanel;
     public GameObject inGamePanel;
     public GameObject lostPanel;
     public GameObject winPanel;
     public Image[] hearts;
    [SerializeField] TextMeshProUGUI coinSize;
    [SerializeField] TextMeshProUGUI ScoreTxt;
    [SerializeField] TextMeshProUGUI finalScore;
    void Start()
    {
        GameAnalytics.Initialize();
        coin = 0;
        gameOver = false;
        gamefinish = false;
        gameStart = false;
        score = 0;
        Coin(0);
        startpanel.SetActive(true);
        inGamePanel.SetActive(false);
        lostPanel.SetActive(false);
        winPanel.SetActive(false);
        print(SceneManager.sceneCount-1 + " :now hole: " + (SceneManager.GetActiveScene().buildIndex));
    }
    public void Coin(int amount)
    {
        coin += amount;
        coinSize.text = coin.ToString();
    }
    public void Heart()
    {
        hearts[health].color = new Color32(255, 255, 255, 95);
    }
    public void GameOver()
    {
        inGamePanel.SetActive(false);
        lostPanel.SetActive(true);
    }
    public void GameWin()
    {
        inGamePanel.SetActive(false);
        winPanel.SetActive(true);
        finalScore.text = score.ToString();
    }
    public void LostGame()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "FailLevel", SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OnScoreChange(int kind)
    {
        score += (scoreAmount * kind);
        ScoreTxt.text = score.ToString();
    }
    public void WinGame()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "CompleteLevel", SceneManager.GetActiveScene().buildIndex + 1);
        //todo add coin calc
    }
}
public enum Side
{
    left,
    right,
    center
}