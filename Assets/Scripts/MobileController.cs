using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    Rigidbody rb;
    private Touch _touch;   // Ekrana dokunmayý algýlama
    public bool tap;  // Sürüklemenin baþlanýp baþlanmadýðý

    [SerializeField] private float jumpSpeed = 30f;    // Player hareket h�z�
    [SerializeField] private bool isGround;    // // Player default kayd�rma mesafesi
    //[SerializeField] private bool isMoving; // Hareket ediyor mu


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGround = true;
        tap = false;
    }
    private void Update()
    {
        MoveInput();    // Player hareket kontrol�
    }
    private void FixedUpdate()
    {
        //transform.Translate(0, 0, 10f * Time.fixedDeltaTime);     
        
    }

    void MoveInput()
    {
        if (Input.touchCount > 0) // Ekrana dokunulmuþsa
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)   // Dokunma baþladyýsa
            {
                tap = true;    // Dokunma / sürüklenme baþlanmýþtýr
            }
        }
        if (tap)   // Oyuncu sürükleme iþlemi yaptýysa
        {
            if (_touch.phase == TouchPhase.Ended)
            {
                // Eðer oyuncu karakteri hareket ettirmeyi bitirdiyse                
                tap = false;   // Sürükleme yapmýyor
            }
        }
        if (tap && isGround)
        {
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
            isGround = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("isGround"))
        {
            isGround = true;
        }
    }
}