using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{   
    [SerializeField] private float spawnInterval;   // Ne sıklıkla nesne çıkarılacak
    [SerializeField] private ObjectPool objectPool = null;
    [SerializeField] private int poolValue = 0;
    void Start()
    {
        StartCoroutine(nameof(SpawnRoutine));
        spawnInterval = GameManager.gamemanagerInstance.spawnInterval;
    }
    private void Update()
    {
        spawnInterval = GameManager.gamemanagerInstance.spawnInterval; 
    }
    private IEnumerator SpawnRoutine()
    {        
        while (true)    // Sonsuz döngü 
        {
            float circleX = Random.Range(-1f, 3f);
            GameObject circleObj = objectPool.GetPooledObject(0);    // "ObjectPool" scriptinden yeni nesne çeker
            circleObj.transform.position = new Vector3(circleX, 1.6f, 100f);
            yield return new WaitForSeconds(spawnInterval); // Fonk çalışma süresi

            poolValue = Random.Range(1, 13);
            float blockX = Random.Range(-2f, 2f);
            GameObject newObj = objectPool.GetPooledObject(poolValue);
            if (poolValue == 1 || poolValue == 2 || poolValue == 5 || poolValue == 6)
            {
                newObj.transform.position = new Vector3(blockX, 0f, 100f);
            }
            else if (poolValue == 11 || poolValue == 12)
            {
                newObj.transform.position = new Vector3(0f, 1.5f, 100f);
            }
            else
            {
                newObj.transform.position = new Vector3(0f, 0f, 100f);
            }
            yield return new WaitForSeconds(spawnInterval); // Fonk çalışma süresi
        }
    }
}