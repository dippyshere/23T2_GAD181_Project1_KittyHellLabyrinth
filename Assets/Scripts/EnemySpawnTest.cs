using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTest : MonoBehaviour
{
    public bool safeToSpawn = true;
    public float radius = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Blocking spawn due to being inside a trigger");
        safeToSpawn = false;
    }

    private void Start()
    {
        if (!safeToSpawn)
        {
            // scuffed but should work i suppose
            // Check if this transform is inside the radius of the center transform
            bool isInsideRadius = IsTransformInsideRadius(transform, GameObject.FindWithTag("Player").GetComponent<Transform>(), radius);

            // Do something based on the result
            if (isInsideRadius)
            {
                safeToSpawn = false;
                Debug.Log("Blocking spawn due to being inside player safe zone");
            }
        }

        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return 1;
        Destroy(gameObject);
    }

    private bool IsTransformInsideRadius(Transform transformToCheck, Transform center, float radius)
    {
        // Calculate the distance between the two transforms
        float distance = Vector3.Distance(transformToCheck.position, center.position);

        // Check if the distance is less than or equal to the radius
        return distance <= radius;
    }
}
