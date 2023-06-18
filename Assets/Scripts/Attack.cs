using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 5f;
    public float distanceFromPlayer = 2f;

    private bool isCheckingForInput = false;
    private GameObject enemyCollision;

    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.isGameOver)
        {
            Rotate();
        }

        if (isCheckingForInput)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                enemyCollision.GetComponent<EnemyController>().HandleCollisionWithPlayer();
                GameObject.FindWithTag("Player").GetComponent<ScoreController>().timeAlive += 5f; // Increases player score upon enemy kill
            }
        }
    }

    private void Rotate()
    {
        // Get the mouse position in world space
        Vector2 mousePosition = GetMouseWorldPosition();

        // Calculate the direction from the player to the mouse position
        Vector2 directionToMouse = mousePosition - (Vector2)player.position;

        // Calculate the target angle based on the direction to the mouse position
        float targetAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        // Calculate the target position based on the mouse position and desired distance
        Vector2 targetPosition = (Vector2)player.position + directionToMouse.normalized * distanceFromPlayer;

        // Smoothly rotate the object towards the target angle
        float rotationStep = rotationSpeed * Time.deltaTime;
        float currentAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationStep);
        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);

        // Set the position of the object to the target position
        transform.position = targetPosition;
    }

    Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyCollision = collision.gameObject;
            isCheckingForInput = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyCollision = null;
            isCheckingForInput = false;
        }
    }
}
