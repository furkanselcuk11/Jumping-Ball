using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    [SerializeField] private MoneySO moneyType = null;    // Scriptable Objects eriþir 

    public static GameManager gamemanagerInstance;
    [Space]
    [Header("Game Controller")]
    public bool gameStart;
    [SerializeField] private GameObject balls;
    [SerializeField] private float timeValue = 0;
    public float spawnInterval;
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
    [SerializeField] private TextMeshProUGUI diamondText;


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
        airEffect.GetComponent<ParticleSystem>().Play();        
        GameObject.Find("Spawner").gameObject.GetComponent<Spawner>().enabled = true;
        GameStartPanel.SetActive(false);
        GameRunTimePanel.SetActive(true);        
    }
    public void DiamondAdd()
    {
        moneyType.totalMoney += 10;
        diamondText.text = moneyType.totalMoney.ToString();
        diamondAddEffect.GetComponent<ParticleSystem>().Play();
        diamondAddEffect.GetComponent<Renderer>().material = balls.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material;
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
        }
        else if (timeValue >=50 && timeValue < 80)
        {
            spawnInterval = 1.6f;
            airEffectMain.simulationSpeed = 6f;
        }
        else if (timeValue >= 80 && timeValue < 100)
        {
            spawnInterval = 1.4f;
            airEffectMain.simulationSpeed = 8f;
        }
        else if (timeValue >= 100 && timeValue < 120)
        {
            spawnInterval = 1.2f;
            airEffectMain.simulationSpeed = 10f;
        }
        else if(timeValue>=120)
        {
            spawnInterval = 1f;
            airEffectMain.simulationSpeed = 12f;
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
    public void GameOver()
    {
        gameStart = false;
        Destroy(GameObject.Find("Spawner").gameObject);
        BlockAndCirclePause();
        airEffect.GetComponent<ParticleSystem>().Pause();
        diamondAddEffect.GetComponent<ParticleSystem>().Pause();
        

        StartCoroutine(nameof(RestartGame));        
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
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);
        GameRunTimePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
