using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 1;   // Ne sıklıkla nesne çıkarılacak
    [SerializeField] private ObjectPool objectPool = null;
    [SerializeField] private int poolValue = 0;
    void Start()
    {
        StartCoroutine(nameof(SpawnRoutine));
        //StartCoroutine(nameof(SpawnCircleRoutine));
    }
    private IEnumerator SpawnRoutine()
    {
        
        while (true)    // Sonsuz döngü 
        {
            poolValue = Random.Range(0, 7);
            GameObject newObj = objectPool.GetPooledObject(poolValue);    // "ObjectPool" scriptinden yeni nesne çeker   
            newObj.transform.position = new Vector3(0f, 0f, 100f);   // Gelen yeni nesnenin pozisyonu ayarlar
            if (poolValue == 6)
            {
                newObj.transform.position = new Vector3(1f, 1.6f, 100f);
            }
            else
            {
                newObj.transform.position = new Vector3(0f, 0f, 100f);   // Gelen yeni nesnenin pozisyonu ayarlar
            }
            yield return new WaitForSeconds(spawnInterval); // Fonk çalışma süresi
        }
    }
    private IEnumerator SpawnCircleRoutine()
    {

        while (true)    // Sonsuz döngü 
        {
            GameObject newObj = objectPool.GetPooledObject(6);    // "ObjectPool" scriptinden yeni nesne çeker 
            newObj.transform.position = new Vector3(1f, 1.6f, 100f);  // Gelen yeni nesnenin pozisyonu ayarlar

            yield return new WaitForSeconds(4); // Fonk çalışma süresi
        }
    }
}