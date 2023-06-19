using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5f;
    public Transform target;
    public GameObject deathParticles;

    private Pathfinding pathfinding;
    private Vector3Int[] path;
    private int currentPathIndex = 0;

    private void Awake()
    {
        pathfinding = GetComponent<Pathfinding>();
    }

    private void Start()
    {
        Vector3Int startPosition = Vector3Int.FloorToInt(transform.position);
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        CalculatePath(startPosition, target);
    }

    private void Update()
    {
        if (path == null || currentPathIndex >= path.Length)
            return;

        Vector3 targetPosition = pathfinding.tilemap.GetCellCenterWorld(path[currentPathIndex]);

        // Move the enemy towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the enemy has reached the current target position
        if (transform.position == targetPosition)
        {
            currentPathIndex++;

            // Check if the enemy has reached the final target position
            if (currentPathIndex >= path.Length)
            {
                // Handle reaching the target (e.g., attack, destroy, etc.)
                HandleTargetReached();
            }
        }
    }

    private void HandleTargetReached()
    {
        // Get the player GameObject (assuming it has a "Player" tag)
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Retrieve the PlayerController component from the player GameObject
        PlayerController playerController = player.GetComponent<PlayerController>();

        // Check if the enemy has collided with the player
        if (playerController != null)
        {
            CircleCollider2D playerCollider = playerController.GetComponent<CircleCollider2D>();
            CircleCollider2D enemyCollider = GetComponent<CircleCollider2D>();

            // Check if the player's collider overlaps with the enemy's collider
            if (playerCollider.IsTouching(enemyCollider))
            {
                // Handle the collision (e.g., apply damage to the player)
                HandleCollisionWithPlayer();
            }
            else
            {
                // Calculate a new path to the player's current position
                Vector3Int startPosition = Vector3Int.FloorToInt(transform.position);
                CalculatePath(startPosition, playerController.transform);
            }
        }
    }



    public void HandleCollisionWithPlayer()
    {
        // Implement the logic for what happens when the enemy collides with the player
        // For example, you could apply damage to the player, trigger a game over, etc.
        
        //Debug.Log("Player collided with enemy!");
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }



    private void CalculatePath(Vector3Int startPosition, Transform targetTransform)
    {
        Vector3Int targetPosition = Vector3Int.FloorToInt(targetTransform.position);
        CalculatePath(startPosition, targetPosition);
    }

    private void CalculatePath(Vector3Int startPosition, Vector3Int targetPosition)
    {
        path = pathfinding.FindPath(startPosition, targetPosition);
        currentPathIndex = 0;
    }
}
