using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTest : MonoBehaviour
{
    public bool safeToSpawn = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        safeToSpawn = false;
    }
    private void Start()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return 1;
        Destroy(gameObject);
    }
}
