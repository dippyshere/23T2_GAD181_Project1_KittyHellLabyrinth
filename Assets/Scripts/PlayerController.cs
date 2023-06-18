using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Components
    [Header("Components")]
    private Rigidbody2D rb; // Rigidbody component
    
    // Movement variables
    [Header("Movement Settings")] // Creates a header attribute in the inspector
    [SerializeField] private float movementSpeed; // Controls how fast the player moves
    private float vertical;
    private float horizontal;

    // Health Variables
    [Header("Health Settings")]
    public bool isGameOver;
    [SerializeField] private int lives;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Assigns player's Rigidbody2D component
    }

    // Update is called once per frame
    void Update()
    {
        // Movement input
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed); // Move the player
    }
}
