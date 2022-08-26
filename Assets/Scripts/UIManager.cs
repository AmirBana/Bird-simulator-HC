using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public void Home_StartGame()
    {
        GameManager.Instance.gameStart = true;
        GameManager.Instance.startpanel.SetActive(false);
        string x = GameManager.Instance.allHumans.ToString();
        print("all humans:"+x);
        GameManager.Instance.inGamePanel.SetActive(true);
    }
    public void Reset_StartGame()
    {
        GameManager.Instance.gameStart = true;
        GameManager.Instance.resetPanel.SetActive(false);
        GameManager.Instance.inGamePanel.SetActive(true);
    }
    public void Lost_Home()
    {
        PlayerPrefs.SetInt("isReset", 0);
        GameManager.Instance.LostGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Lost_Reset()
    {
        PlayerPrefs.SetInt("isReset", 1);
        if (Time.timeScale == 0) Time.timeScale = 1;
        GameManager.Instance.LostGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void pause_Continue()
    {
        GameManager.Instance.pausePanel.SetActive(false);
        GameManager.Instance.inGamePanel.SetActive(true);
        Time.timeScale = 1;
    }
    public void Game_Pause()
    {
        Time.timeScale = 0;
        GameManager.Instance.inGamePanel.SetActive(false);
        GameManager.Instance.pausePanel.SetActive(true);
    }
    public void Lost_ShowAds()
    {

    }
    public void Win()
    {
        GameManager.Instance.WinGame();
        if (SceneManager.sceneCountInBuildSettings-1 == (SceneManager.GetActiveScene().buildIndex))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
