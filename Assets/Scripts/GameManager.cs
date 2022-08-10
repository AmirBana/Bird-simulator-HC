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
    public int ammo;
    public int maxAmmo;
    public int initAmmo;
    [SerializeField]
    [HideInInspector]public bool gameStart, gamefinish, gameOver;
    [Header("UI Elements")]
    [SerializeField] GameObject startpanel;
    [SerializeField] GameObject inGamePanel;
    [SerializeField] GameObject lostPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] Slider ammoSlider;
    [SerializeField] TextMeshProUGUI ammoSize; 
    void Start()
    {
        GameAnalytics.Initialize();
        ammo = 0;
        gameOver = false;
        gamefinish = false;
        gameStart = false;
        ammoSlider.minValue = 0;
        ammoSlider.maxValue = maxAmmo;
        Ammo(initAmmo);
        startpanel.SetActive(true);
        inGamePanel.SetActive(false);
        lostPanel.SetActive(false);
        winPanel.SetActive(false);
        print(SceneManager.sceneCount-1 + " :now hole: " + (SceneManager.GetActiveScene().buildIndex));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        gameStart = true;
        startpanel.SetActive(false);
        inGamePanel.SetActive(true);
        
    }
    public void Ammo(int amount)
    {
        ammo += amount;
        ammoSlider.value = ammo;
        ammoSize.text = ammo.ToString();
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
    }
    public void Lost()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "FailLevel", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Win()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "CompleteLevel", SceneManager.GetActiveScene().buildIndex+1);
        if(3 == (SceneManager.GetActiveScene().buildIndex))
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}