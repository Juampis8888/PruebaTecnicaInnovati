using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{   
    public CameraController cameraController;

    public GameObject[] enemyPrefab;

    public float spawnInterval = 2f;

    public int maxEnemies = 10;

    private int enemiesSpawned = 0;

    public void CallSpawn()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() 
    {   
        Quaternion quat = Quaternion.Euler(0,0,0);
        float PosY= 0;
        while (enemiesSpawned < maxEnemies) 
        {   
            yield return new WaitForSeconds(spawnInterval);
            var value = Random.Range(0, enemyPrefab.Length);
            PosY= 0;
            if( value == enemyPrefab.Length-1)
            {
                quat = Quaternion.Euler(0,180,0);
                PosY = Random.Range(1f, 2f);
            }
            Vector3 spawnPosition = new Vector3(GetX(),PosY,transform.position.z);
            Instantiate(enemyPrefab[value], spawnPosition, quat);
            enemiesSpawned++;
        }
    }

    public void CancelSpawn()
    {
        StopCoroutine(SpawnEnemies());
    }

    public float GetX()
    { 
        return  cameraController.transformPlayerController.position.x + Random.Range(50,70); 
    }
}
