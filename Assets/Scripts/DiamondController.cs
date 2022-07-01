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
            GameManager.gamemanagerInstance.DiamondAdd();            
            AudioController.audioControllerInstance.Play("DiamondSound"); // Ses çalýþýr
            this.StartCoroutine(nameof(IsActive));
        }
    }

    IEnumerator IsActive()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
