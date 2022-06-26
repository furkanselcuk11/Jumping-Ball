using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.Translate(-transform.forward * (Time.deltaTime * 20));
    }
}
