using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{    
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(-Vector3.forward * (Time.deltaTime * 20));
    }
}
