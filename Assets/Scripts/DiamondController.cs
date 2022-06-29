using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 100f;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Rotate(0f, 0f, turnSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Elmas");
            GameManager.gamemanagerInstance.DiamondAdd();
            // Efekt ekle
            AudioController.audioControllerInstance.Play("DiamondSound"); // Ses çalýþýr
        }
    }
}
