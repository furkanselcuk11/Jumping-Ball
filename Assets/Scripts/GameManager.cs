using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    [SerializeField] private MoneySO moneyType = null;    // Scriptable Objects eriþir 
    [SerializeField] private BallSO ballType = null;    // Scriptable Objects eriþir 

    public static GameManager gamemanagerInstance;
    [Space]
    [Header("Game Controller")]
    public bool gameStart;
    [SerializeField] private GameObject balls;
    [SerializeField] private float timeValue = 0;
    [SerializeField] private int meter = 0;
    public float spawnInterval; // Obsactle Doğma süresi
    [SerializeField] private ParticleSystem airEffect;
    [SerializeField] private ParticleSystem diamondAddEffect;
    public ParticleSystem diedEffect;         
    [Space]
    [Header("UI Controller")]    
    [Header("GameStartPanel Controller")]
    [SerializeField] private GameObject GameStartPanel;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI diamondStartText;
    [Header("GameRunTimePanel Controller")]
    [SerializeField] private GameObject GameRunTimePanel;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private TextMeshProUGUI diamondText;
    [SerializeField] private TextMeshProUGUI meterText;

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
        diamondText.text = moneyType.totalMoney.ToString();
        diamondStartText.text = moneyType.totalMoney.ToString();
        GameStartPanel.SetActive(true);
        diamondAddEffect.GetComponent<ParticleSystem>().Pause();
    }
    
    void Update()
    {
        if (gameStart)
        {
            timeValue += Time.deltaTime;
            
        }
        else if(!gameStart && GameStartPanel)
        {
            timeValue = 0;
        }
        DisplayTime(timeValue);
        SpawnInterval(timeValue);
    }
    public void StartTheGame()
    {
        gameStart = true;
        AudioController.audioControllerInstance.Play("BGSound"); // Ses çalışır
        StartCoroutine(nameof(MeterCounter));
        airEffect.GetComponent<ParticleSystem>().Play();        
        GameObject.Find("Spawner").gameObject.GetComponent<Spawner>().enabled = true;
        GameStartPanel.SetActive(false);
        restartButton.SetActive(false);
        GameRunTimePanel.SetActive(true);        
    }
    public void DiamondAdd()
    {
        moneyType.totalMoney += 10;
        diamondText.text = moneyType.totalMoney.ToString();
        diamondAddEffect.GetComponent<ParticleSystem>().Play();
        diamondAddEffect.GetComponent<Renderer>().material = balls.gameObject.transform.GetChild(ballType.selectedBall).gameObject.GetComponent<Renderer>().material;
    }
    private void SpawnInterval(float timeToValue)
    {
        var airEffectMain = airEffect.main;
        if (timeValue<30)
        {
            spawnInterval = 2f;
            airEffectMain.simulationSpeed = 3f;
        }
        else if(timeValue>=30 && timeValue<50)
        {
            spawnInterval = 1.8f;
            airEffectMain.simulationSpeed = 4f;
            SkyBoxContoller.skyBoxContollerInstance.SkyBoxChange(1);
        }
        else if (timeValue >=50 && timeValue < 80)
        {
            spawnInterval = 1.6f;
            airEffectMain.simulationSpeed = 6f;
            SkyBoxContoller.skyBoxContollerInstance.SkyBoxChange(2);
        }
        else if (timeValue >= 80 && timeValue < 100)
        {
            spawnInterval = 1.4f;
            airEffectMain.simulationSpeed = 8f;
            SkyBoxContoller.skyBoxContollerInstance.SkyBoxChange(3);
        }
        else if (timeValue >= 100 && timeValue < 120)
        {
            spawnInterval = 1.2f;
            airEffectMain.simulationSpeed = 10f;
            SkyBoxContoller.skyBoxContollerInstance.SkyBoxChange(4);
        }
        else if(timeValue>=120)
        {
            spawnInterval = 1f;
            airEffectMain.simulationSpeed = 12f;
            SkyBoxContoller.skyBoxContollerInstance.SkyBoxChange(5);
        }
    }
    public void UpdateUIText()
    {
        diamondStartText.text = moneyType.totalMoney.ToString();
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
    public void GameOver()
    {
        gameStart = false;
        StopCoroutine(nameof(MeterCounter));
        Destroy(GameObject.Find("Spawner").gameObject);
        BlockAndCirclePause();
        airEffect.GetComponent<ParticleSystem>().Pause();
        diamondAddEffect.GetComponent<ParticleSystem>().Pause();
        restartButton.SetActive(true);      
    }
    public void GameExit()
    {
        Application.Quit();
    }
    private void BlockAndCirclePause()
    {
        GameObject[] Circles = GameObject.FindGameObjectsWithTag("Circle");
        GameObject[] Blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (var item in Circles)
        {
            item.GetComponent<Circle>().speed = 0;
        }
        
        foreach (var item in Blocks)
        {
            item.GetComponent<Block>().speed = 0;
        }
    }
    public void RestartGame()
    {
        GameRunTimePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator MeterCounter()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(1);
            meter += 10;
            meterText.text = meter.ToString() + " m";
        }
              
    }
}
