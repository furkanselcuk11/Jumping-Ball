using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private BallSO ballType = null;    // Scriptable Objects eriþir 
    [SerializeField] private MoneySO moneyType = null;    // Scriptable Objects eriþir 

    [SerializeField] private int currentBallIndex;
    [SerializeField] private GameObject[] ballModels;
    [SerializeField] private Button[] buyButtons;
    void Start()
    {
        BallUpdate();
    }
    void Update()
    {
        
    }
    public void ChangeBall(int newBall)
    {
        ballModels[currentBallIndex].SetActive(false);
        ballModels[newBall].SetActive(true);
        ballType.selectedBall = newBall;
    }
    private void BallUpdate()
    {
        currentBallIndex = ballType.selectedBall;
        foreach (GameObject ball in ballModels)
        {
            ball.SetActive(false);
        }
        ballModels[currentBallIndex].SetActive(true);
    }
    public void UpdateButtons()
    {        
        for (int i = 0; i < ballType.balls.Length; i++)
        {
            if (ballType.balls[i].isUnlocked)
            {
                buyButtons[i].gameObject.SetActive(false);  // Eğer Ball alınmış (isUnlocked) ise satın alma tuşu pasif olacak
                buyButtons[i].transform.parent.GetComponent<Button>().interactable = true;  // Eğer Ball alınmış (isUnlocked) ise  ball seçilebilir olacak
            }
            else
            {
                buyButtons[i].gameObject.SetActive(true);   // Eğer Ball alınmamış (isUnlocked) ise satın alma tuşu aktif olacak
                buyButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "BUY " + ballType.balls[i].price;    // Satın alınacak ball fiyatı
                buyButtons[i].transform.parent.GetComponent<Button>().interactable = false;       // Eğer Ball alınmamış (isUnlocked)ise  ball seçilemez olacak    
                
            }
        }
    }
    public void BallBuy(int newBall)
    {
        if (moneyType.totalMoney >= ballType.balls[newBall].price)
        {
            ballType.balls[newBall].isUnlocked = true;
            buyButtons[newBall].gameObject.SetActive(false);  // Eğer Ball alınmış (isUnlocked) ise satın alma tuşu pasif olacak
            buyButtons[newBall].transform.parent.GetComponent<Button>().interactable = true;  // Eğer Ball alınmış (isUnlocked) ise  ball seçilebilir olacak
            moneyType.totalMoney -= ballType.balls[newBall].price;
            GameManager.gamemanagerInstance.UpdateUIText();
        }
        else
        {
            ballType.balls[newBall].isUnlocked = false;
        }
    }
}
