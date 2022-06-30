using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private BallSO ballType = null;    // Scriptable Objects eriþir 

    [SerializeField] private int currentBallIndex;
    [SerializeField] private GameObject[] ballModels;
    void Start()
    {   
        currentBallIndex = ballType.selectedBall;
        foreach (GameObject ball in ballModels)
        {
            ball.SetActive(false);
        }
        ballModels[currentBallIndex].SetActive(true);
    }
    void Update()
    {
        
    }
}
