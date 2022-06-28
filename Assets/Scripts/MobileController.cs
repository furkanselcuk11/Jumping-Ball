using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    Rigidbody rb;
    [Space]
    [Header ("Tap Controller")]
    private Touch _touch;   // Ekrana dokunma algılama
    public bool right;  // Sola Dokunma 
    public bool left;  // Sağa Dokunma 
    public bool up;  // Yukarı Dokunma 
    public bool tap;  // Yukarı Dokunma 
    public float distance = 50;

    [Space]
    [Header("Player Controller")]
    [SerializeField] private float jumpSpeed = 30f;    // Player hareket h�z�
    [SerializeField] private float speed = 5f;    // Player hareket h�z�
    [SerializeField] private float defaultSwipe = 4f;    // Player default kaydýrma mesafesi
    [SerializeField] private bool isGround;    // // Player default kayd�rma mesafesi
    //[SerializeField] private bool isMoving; // Hareket ediyor mu


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tap = false;
        isGround = true;
    }
    private void Update()
    {
        SwipeControl(); // Kaydırma kontrolu
        //MoveInput();    // Player hareket kontrol
    }
    private void FixedUpdate()
    {
        MoveInput();    // Player hareket kontrol
    }
    void SwipeControl()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.deltaPosition.magnitude > distance)
            {
                float X = _touch.deltaPosition.x;
                float Y = _touch.deltaPosition.y;
                if (Mathf.Abs(X) > Mathf.Abs(Y))
                {
                    // Sağ - Sol
                    if (X < 0)
                    {
                        // Sol
                        right = false;
                        left = true;
                        up = false;
                        tap = true;
                    }
                    else
                    {
                        // Sağ
                        right = true;
                        left = false;
                        up = false;
                        tap = true;
                    }
                }
                else
                {
                    if (Y > 30)
                    {
                        // Yukarı
                        right = false;
                        left = false;
                        up = true;
                        tap = true;
                    }
                }
                
            }
        }
        if (_touch.phase == TouchPhase.Ended)
        {
            right = false;
            left = false;
            up = false;
            tap = false;
        }
    }
    void MoveInput()
    {
        float moveX = transform.position.x; // Player objesinin x pozisyonun de?erini al?r 
        if (right & isGround)
        {
            //rb.velocity = new Vector3(1, 0, 0) * speed * Time.deltaTime;
            moveX = Mathf.Clamp(moveX + 1 * speed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);    // Pozisyon sýnýrlandýrýlmasý koyulacaksa
        }
        if (left & isGround)
        {
            //rb.velocity = new Vector3(-1, 0, 0) * speed * Time.deltaTime;
            moveX = Mathf.Clamp(moveX - 1 * speed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);    // Pozisyon sýnýrlandýrýlmasý koyulacaksa
        }
        if (up & isGround)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
            isGround = false;
        }
        if(!tap & isGround)
        {
            rb.velocity = Vector3.zero; // Eğer hareket edilmediyse Player objesi sabit kalsın
        }
        transform.position = new Vector3(moveX, transform.position.y, transform.position.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("isGround"))
        {
            isGround = true;
            up = false;
        }
    }
}