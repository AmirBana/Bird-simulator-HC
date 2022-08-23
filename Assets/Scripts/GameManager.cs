using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;
using System.IO;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
   
    //main script
    [Header("Game Elements")]
    public int score;
    public int coin;
    public static int buildIndex;
    public int finalCoin=0;
    public bool isReset = false;
    [SerializeField] int scoreAmount;
    public int health;
    public int currentLevel = 0;
    SaveData data;
    [HideInInspector]public bool gameStart, gamefinish, gameOver;

    [Header("UI Elements")]
     public GameObject startpanel;
     public GameObject inGamePanel;
     public GameObject lostPanel;
     public GameObject winPanel;
     public GameObject pausePanel;
     public GameObject resetPanel;
     public Image[] hearts;
     [SerializeField] TextMeshProUGUI coinSize;
     [SerializeField] TextMeshProUGUI ScoreTxt;
     [SerializeField] TextMeshProUGUI finalScore;
     [SerializeField] TextMeshProUGUI homeCoins;
     [SerializeField] TextMeshProUGUI homeCurrentLevel;
    void Start()
    {
        data = new SaveData();
        LoadGame();
        GameAnalytics.Initialize();
        coin = 0;
        finalCoin = data.coin;
        currentLevel = data.currentLevel;
        homeCoins.text = finalCoin.ToString();
        homeCurrentLevel.text = currentLevel.ToString();
        gameOver = false;
        gamefinish = false;
        gameStart = false;
        score = 0;
        Coin(0);
        if (!PlayerPrefs.HasKey("isReset") || PlayerPrefs.GetInt("isReset")==0)
        {
            startpanel.SetActive(true);
            resetPanel.SetActive(false);
        }
        else
        {
            startpanel.SetActive(false);
            resetPanel.SetActive(true);
            PlayerPrefs.SetInt("isReset", 0);
        }
        inGamePanel.SetActive(false);
        lostPanel.SetActive(false);
        winPanel.SetActive(false);
        print("final coins:" + finalCoin);
        print(SceneManager.sceneCountInBuildSettings - 1 + " :now hole: " + (SceneManager.GetActiveScene().buildIndex));//todo remove
        print(Application.persistentDataPath);
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
        finalCoin += (coin+score);
        print("final coin after finish:" + finalCoin);
        SaveGame();
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
    void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
            print(data);
        }
    }
    public void SaveGame()
    {
        data = new SaveData();
        data.coin = finalCoin;
        data.currentLevel = currentLevel + 1;
        if (SceneManager.sceneCountInBuildSettings-1 == SceneManager.GetActiveScene().buildIndex)
            data.sceneIndex = 1;
        else
            data.sceneIndex = SceneManager.GetActiveScene().buildIndex+1;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json",json);
    }
}
public enum Side
{
    left,
    right,
    center
}