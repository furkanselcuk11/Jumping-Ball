using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gamemanagerInstance;

    public bool gameStart;
    [SerializeField] private float timeValue = 0;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private int diamondScore = 0;
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
        diamondScore = 0;
        diamondText.text = diamondScore.ToString();
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
    }
    public void DiamondAdd()
    {
        diamondScore+=100;
        diamondText.text = diamondScore.ToString();
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
