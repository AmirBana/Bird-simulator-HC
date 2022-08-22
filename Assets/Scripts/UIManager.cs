using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.gameStart = true;
        GameManager.Instance.startpanel.SetActive(false);
        GameManager.Instance.inGamePanel.SetActive(true);
    }
    public void Lost()
    {
        GameManager.Instance.LostGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
