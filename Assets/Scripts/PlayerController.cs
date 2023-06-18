using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Sprite elements
    [Header("Sprite Elements")]
    public SpriteRenderer leftEar;
    public SpriteRenderer rightEar;
    public SpriteRenderer eyes;
    public SpriteRenderer mouth;
    public Sprite eyePain;
    public Sprite mouthPain;
    public Animator Animator;
    public GameObject ear;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

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

        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed); // Move the player
    }

    public void takeDamage()
    {
        lives -= 1;
        switch (lives)
        {
            case 0:
                // Game Over
                isGameOver = true;
                Time.timeScale = 0f;
                break;
            case 1:
                // Change sprite to 0 lifes
                rightEar.enabled = false;
                Instantiate(ear, transform.position, Quaternion.identity);
                break;
            case 2:
                // Change sprite to 1 live
                leftEar.enabled = false;
                Animator.SetTrigger("hurt");
                Instantiate(ear, transform.position, Quaternion.identity);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !this.gameObject.CompareTag("Sword"))
        {
            takeDamage();
            collision.gameObject.GetComponent<EnemyController>().HandleCollisionWithPlayer();
        }
    }
}
