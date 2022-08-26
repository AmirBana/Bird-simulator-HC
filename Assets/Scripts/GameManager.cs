using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;
using System.IO;
using DG.Tweening;
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
    public int allHumans;
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
     [SerializeField] private TextMeshProUGUI thisLevelTxt;
     [SerializeField] private TextMeshProUGUI nextLevelTxt;
     [SerializeField] private TextMeshProUGUI calculatedCOins, poopedMen;
     [SerializeField] private Slider progressSlider;
     [SerializeField] Image winFillImg;
     [SerializeField] Image[] stars;
     [SerializeField] Sprite starEmpty, starFull;
     [SerializeField] Color32 trophyEmpty, trophyFull;
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
        thisLevelTxt.text = currentLevel.ToString();
        nextLevelTxt.text = (currentLevel+1).ToString();
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
        CalculateWinCoins();
        finalCoin += coin;
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

    public void LevelProgress(float amount)
    {
        progressSlider.value = amount;
    }

    public void CalculateWinCoins()
    {
        poopedMen.text = score + "/" + allHumans;
        float humanPercentGotted = (float)score / allHumans;
        float easetime = humanPercentGotted * 5f;
        int multiplier = 1;
        if (humanPercentGotted < 0.25f) multiplier = 1;
        else if (humanPercentGotted < 0.5f) multiplier = 2;
        else if (humanPercentGotted < 0.75f) multiplier = 3;
        else if (humanPercentGotted < 1f) multiplier = 4;
        else if (humanPercentGotted == 1) multiplier = 5;
        coin *= multiplier;
        var fillWin = winFillImg.DOFillAmount(humanPercentGotted, easetime).SetEase(Ease.OutSine);
   /*     fillWin.onUpdate = () =>
        {
            if(winFillImg.fillAmount >= 0.25 && stars[0].sprite != starFull)
            {
                print(1);
                stars[0].sprite = starFull;
            }
            if(winFillImg.fillAmount >= 0.5 && stars[1].sprite != starFull)
            {
                if ( stars[0].sprite == starFull)
                    stars[1].sprite = starFull;
            }
            if(winFillImg.fillAmount != 0.75 && stars[2].sprite != starFull)
            {
                if(stars[1].sprite == starFull && stars[0].sprite == starFull)
                    stars[2].sprite = starFull;
            }
            if(winFillImg.fillAmount == 1)
            {
                if (stars[1].sprite == starFull && stars[0].sprite == starFull && stars[2].sprite == starFull)
                    stars[3].color = trophyFull;
            }
        };*/
        fillWin.onComplete = () =>
        {
            calculatedCOins.text = '+'+coin.ToString();
            if (winFillImg.fillAmount >= 0.25)
            {
                stars[0].sprite = starFull;
            }
            if (winFillImg.fillAmount >= 0.5)
            {
                    stars[1].sprite = starFull;
            }
            if (winFillImg.fillAmount >= 0.75)
            {
                    stars[2].sprite = starFull;
            }
            if (winFillImg.fillAmount == 1)
            {
                    stars[3].color = trophyFull;
            }
            calculatedCOins.DOScale(1, 2f).SetEase(Ease.OutBack);
        };
    }
    void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
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