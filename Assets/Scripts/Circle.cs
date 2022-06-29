using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Circle : MonoBehaviour
{
    public float speed = 20f;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(transform.forward * (Time.deltaTime * speed));
    }
}
