using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private Vector3 spawnLocation;
    [SerializeField] private int enemiesToSpawn;

    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    { 
        for (int i = 0; i < enemiesToSpawn; i++)
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
            spawnLocation = new Vector3(Random.Range(-19, 24), Random.Range(-16, 14), 0);
            Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2, 10));
        }
    }
}
