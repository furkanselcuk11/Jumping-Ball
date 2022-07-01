using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float  _turnSpeed=500f;
    void Start()
    {
        
    }
    
    void Update()
    {
        if(GameManager.gamemanagerInstance.gameStart)
            transform.Rotate(_turnSpeed * Time.deltaTime, 0f, 0f);
    }
}
