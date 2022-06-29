using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.Translate(-transform.forward * (Time.deltaTime * speed));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("GameOver");
            AudioController.audioControllerInstance.Play("GameOverSound"); // Ses çalýþýr
            GameManager.gamemanagerInstance.RestartGame();
        }
    }
}
