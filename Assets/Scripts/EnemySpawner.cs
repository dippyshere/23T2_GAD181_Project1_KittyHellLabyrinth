using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private Vector3 spawnLocation;
    [SerializeField] private int enemiesToSpawnStart;
    [SerializeField] private int enemiesToSpawn = 2;

    public GameObject enemyPrefab;
    
    private int wave = 2; // Starts at two as wave 1 is called in Start

    // Start is called before the first frame update
    void Start()
    { 
        for (int i = 0; i < enemiesToSpawnStart; i++)
        {
            spawnLocation = new Vector3(Random.Range(-19, 24), Random.Range(-16, 14), 0);
            Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
        }
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (!GameObject.FindWithTag("Player").GetComponent<PlayerController>().isGameOver)
        {
            enemiesToSpawn = (int)Mathf.Round((2/3 * wave) + 2);
            
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                spawnLocation = new Vector3(Random.Range(-19, 24), Random.Range(-16, 14), 0);
                Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
            }

            wave++;
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }
}
