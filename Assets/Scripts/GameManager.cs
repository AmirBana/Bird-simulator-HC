using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public int initAmmo;
    [SerializeField]
    [HideInInspector]public bool gameStart, gamefinish, gameOver;

    [Header("UI Elements")]
    [SerializeField] GameObject startpanel;
    [SerializeField] GameObject inGamePanel;
    [SerializeField] GameObject endpanel;
    [SerializeField] Slider ammoSlider;
    [SerializeField] TextMeshProUGUI ammoSize;
    void Start()
    {
        ammo = 0;
        gameOver = gamefinish = gameStart = false;
        ammoSlider.minValue = 0;
        ammoSlider.maxValue = maxAmmo;
        Ammo(initAmmo);
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
    public void Ammo(int amount)
    {
        ammo += amount;
        ammoSlider.value = ammo;
        ammoSize.text = ammo.ToString();
    }
}
