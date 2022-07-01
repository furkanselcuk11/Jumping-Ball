using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float speed = 20f;
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
            var diedParticle = Instantiate(GameManager.gamemanagerInstance.diedEffect, other.gameObject.transform.position, Quaternion.identity);
            diedParticle.GetComponent<Renderer>().material = other.gameObject.transform.GetComponent<Renderer>().material;
            Destroy(other.gameObject);
            Destroy(other.transform.parent.gameObject.transform.parent.GetChild(3).gameObject);
            Destroy(diedParticle,1);            
            GameManager.gamemanagerInstance.GameOver();
        }
    }
}
