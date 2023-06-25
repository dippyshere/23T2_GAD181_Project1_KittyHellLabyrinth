using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private Vector3 spawnLocation;
    private bool toSpawn = true;

    public GameObject enemyPrefab;
    public GameObject enemySpawnTestPrefab;
    
    private int wave = 1; // Starts at two as wave 1 is called in Start

    // Start is called before the first frame update
    void Start()
    { 
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (!GameObject.FindWithTag("Player").GetComponent<PlayerController>().isGameOver)
        {
            int enemiesToSpawn = Mathf.CeilToInt((2/3f * wave) + 1);
            
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                toSpawn = true;
                while (toSpawn)
                {
                    spawnLocation = new Vector3(Random.Range(-19, 24), Random.Range(-16, 14), 0);
                    EnemySpawnTest enemySpawnTest;
                    enemySpawnTest = Instantiate(enemySpawnTestPrefab, spawnLocation, Quaternion.identity).GetComponent<EnemySpawnTest>();
                    yield return 0; // Waits one frame
                    if (enemySpawnTest.safeToSpawn)
                    {
                        Debug.Log(enemySpawnTest.safeToSpawn);
                        Instantiate(enemyPrefab, spawnLocation, Quaternion.identity).GetComponent<EnemySpawnTest>();
                        toSpawn = false;
                    }
                }
            }

            wave++;
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }
}
