using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Projectile variables
    public float moveSpeed;

    // References
    private Transform target;
    private Rigidbody2D rb;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        Vector3 direction = target.position - transform.position; // Calculates direction of player from enemy
        rb.velocity = new Vector2(direction.x, direction.y).normalized * moveSpeed; // Makes the projectile move in direction of the player, at the same speed regardless of player distance
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword") || collision.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
    }
}
