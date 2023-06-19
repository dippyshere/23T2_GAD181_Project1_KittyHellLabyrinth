using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGenerator : MonoBehaviour
{
    public Vector2Int mazeSize = new Vector2Int(10, 10); // Size of the maze (width, height)
    public Tilemap mazeTilemap; // Reference to the Tilemap component

    public TileBase wallTile; // The tile to represent walls
    public TileBase floorTile; // The tile to represent floors

    private int[,] maze; // 2D array to store the maze structure

    private void Start()
    {
        GenerateMaze();
        RenderMaze();
    }

    private void GenerateMaze()
    {
        // Initialize the maze structure
        maze = new int[mazeSize.x, mazeSize.y];

        // Perform maze generation algorithm (e.g., recursive backtracking, randomized Prim's algorithm, etc.)
        // Modify this part to implement the specific maze generation algorithm you prefer

        // Example: Fill the maze with walls
        for (int x = 0; x < mazeSize.x; x++)
        {
            for (int y = 0; y < mazeSize.y; y++)
            {
                maze[x, y] = 1; // 1 represents a wall
            }
        }
    }

    private void RenderMaze()
    {
        // Clear the tilemap
        mazeTilemap.ClearAllTiles();

        // Render the maze based on the maze structure
        for (int x = 0; x < mazeSize.x; x++)
        {
            for (int y = 0; y < mazeSize.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = maze[x, y] == 1 ? wallTile : floorTile;
                mazeTilemap.SetTile(tilePosition, tile);
            }
        }
    }
}
