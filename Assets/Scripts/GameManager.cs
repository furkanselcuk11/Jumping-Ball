using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] private MoneySO moneyType = null;    // Scriptable Objects eriþir 

    public static GameManager gamemanagerInstance;
    [Space]
    [Header("Game Controller")]
    public bool gameStart;
    [SerializeField] private float timeValue = 0;
    [Space]
    [Header("UI Controller")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private int diamondScore;
    [SerializeField] private TextMeshProUGUI diamondText;

    public float spawnInterval;

    private void Awake()
    {
        if (gamemanagerInstance == null)
        {
            gamemanagerInstance = this;
        }
    }
    void Start()
    {
        gameStart = false;
        spawnInterval = 2;
        diamondScore = moneyType.totalMoney;
        diamondText.text = moneyType.totalMoney.ToString();
    }
    
    void Update()
    {
        if (gameStart)
        {
            timeValue += Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }
        DisplayTime(timeValue);
        SpawnInterval(timeValue);
    }
    public void DiamondAdd()
    {
        moneyType.totalMoney += 10;
        diamondText.text = moneyType.totalMoney.ToString();
    }
    private void SpawnInterval(float timeToValue)
    {
        if (timeValue<30)
        {
            spawnInterval = 2f;
        }
        else if(timeValue>=30 && timeValue<50)
        {
            spawnInterval = 1.8f;
        }
        else if (timeValue >=50 && timeValue < 80)
        {
            spawnInterval = 1.6f;
        }
        else if (timeValue >= 80 && timeValue < 100)
        {
            spawnInterval = 1.4f;
        }
        else if (timeValue >= 100 && timeValue < 120)
        {
            spawnInterval = 1.2f;
        }
        else if(timeValue>=120)
        {
            spawnInterval = 1f;
        }
    }
    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void RestartGame()
    {
        gameStart = false;
        GameObject.Find("Spawner").gameObject.GetComponent<Spawner>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
