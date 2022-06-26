using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 1;   // Ne sıklıkla nesne çıkarılacak
    [SerializeField] private ObjectPool objectPool = null;
    void Start()
    {
        StartCoroutine(nameof(SpawnRoutine));
    }
    private IEnumerator SpawnRoutine()
    {
        //int counter = 0;
        while (true)    // Sonsuz döngü 
        {
            //GameObject newObj = objectPool.GetPooledObject(counter++ % 2);    // "ObjectPool" scriptinden yeni nesne çeker            
            GameObject newObj = objectPool.GetPooledObject();    // "ObjectPool" scriptinden yeni nesne çeker            

            newObj.transform.position = new Vector3(0f,0f,100f);   // Gelen yeni nesnenin pozisyonu sıfırlar

            yield return new WaitForSeconds(spawnInterval); // Fonk çalýþma süresi
        }
    }
}