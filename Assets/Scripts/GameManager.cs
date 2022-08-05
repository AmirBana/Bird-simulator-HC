using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(Instance);
        DontDestroyOnLoad(gameObject);
    }
    //main script
    [Header("Game Elements")]
    public int ammo;
    public int maxAmmo;
    [HideInInspector]public bool gameStart, gamefinish, gameOver;

    [Header("UI Elements")]
    [SerializeField] GameObject startpanel;
    [SerializeField] GameObject inGamePanel;
    [SerializeField] GameObject endpanel;
    void Start()
    {
        gameOver = gamefinish = gameStart = false;
        startpanel.SetActive(true);
        inGamePanel.SetActive(false);
        endpanel.SetActive(false);
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
}
