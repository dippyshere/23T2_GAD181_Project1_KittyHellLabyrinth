using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    public Transform target;  // The player's position or the destination the enemy is trying to reach
    public Tilemap tilemap;  // Reference to the Tilemap component representing the maze

    public Pathfinding pathfinding;  // Reference to a Pathfinding script implementing the A* algorithm

    private void Update()
    {
        if (pathfinding != null)
        {
            // Calculate the path from the enemy's current position to the target position
            Vector3Int startTile = tilemap.WorldToCell(transform.position);
            Vector3Int targetTile = tilemap.WorldToCell(target.position);
            Vector3Int[] path = pathfinding.FindPath(startTile, targetTile);

            Debug.Log(path);

            // Move the enemy along the calculated path
            if (path != null && path.Length > 0)
            {
                Debug.Log(path[0]);
                Vector3 targetPosition = tilemap.GetCellCenterWorld(path[0]);
                Vector2 movementDirection = (targetPosition - transform.position).normalized;
                // Apply movement to the Rigidbody2D
                // You can use methods like AddForce, velocity, or transform.Translate to move the enemy
            }
        }
    }
}
