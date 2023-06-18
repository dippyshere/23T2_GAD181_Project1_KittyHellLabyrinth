using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{
    public Tilemap tilemap;
    public Transform target;
    public bool includeDiagonals = false;

    private Dictionary<Vector3Int, Node> nodes = new Dictionary<Vector3Int, Node>();

    private void Awake()
    {
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();

        // Generate nodes for each walkable tile in the tilemap
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] tiles = tilemap.GetTilesBlock(bounds);

        foreach (var pos in bounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos))
            {
                Node node = new Node(pos);
                nodes.Add(pos, node);
            }
        }
        Debug.Log("Generated " + nodes.Count + " nodes.");
    }

    public Vector3Int[] FindPath(Vector3Int startPosition, Transform targetTransform)
    {
        Vector3Int targetPosition = Vector3Int.FloorToInt(targetTransform.position);
        return FindPath(startPosition, targetPosition);
    }

    public Vector3Int[] FindPath(Vector3Int startPosition, Vector3Int targetPosition)
    {
        // Check if the start and target positions are valid
        if (!nodes.ContainsKey(startPosition) || !nodes.ContainsKey(targetPosition))
        {
            Debug.LogWarning("Invalid start or target position for pathfinding.");
            Debug.LogWarning("Start: " + startPosition + " Target: " + targetPosition);
            return null;
        }

        // Reset the state of the nodes
        foreach (var node in nodes.Values)
        {
            node.Reset();
        }

        // Get the start and target nodes
        Node startNode = nodes[startPosition];
        Node targetNode = nodes[targetPosition];

        // Create the open and closed lists
        List<Node> openList = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        // Add the start node to the open list
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // Get the node with the lowest F cost from the open list
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            // Remove the current node from the open list and add it to the closed set
            openList.Remove(currentNode);
            closedSet.Add(currentNode);

            // Check if we've reached the target node
            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            // Get the neighboring nodes
            List<Node> neighbors = GetNeighbors(currentNode.position);

            foreach (var neighbor in neighbors)
            {
                // Skip if the neighbor is in the closed set or is not walkable
                if (closedSet.Contains(neighbor) || !neighbor.walkable)
                    continue;

                // Calculate the cost to move to the neighbor
                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);

                // Check if the new cost is lower or if the neighbor is not in the open list
                if (newMovementCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
                {
                    // Update the neighbor's costs
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    // Add the neighbor to the open list if it's not already there
                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }

        // No valid path found
        Debug.LogWarning("No path found.");
        return null;
    }

    private List<Node> GetNeighbors(Vector3Int position)
    {
        List<Node> neighbors = new List<Node>();

        Vector3Int[] directions = includeDiagonals
            ? new Vector3Int[] { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right, Vector3Int.up + Vector3Int.left, Vector3Int.up + Vector3Int.right, Vector3Int.down + Vector3Int.left, Vector3Int.down + Vector3Int.right }
            : new Vector3Int[] { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };

        foreach (var direction in directions)
        {
            Vector3Int neighborPosition = position + direction;
            if (nodes.ContainsKey(neighborPosition))
            {
                neighbors.Add(nodes[neighborPosition]);
            }
        }

        return neighbors;
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.position.x - nodeB.position.x);
        int distanceY = Mathf.Abs(nodeA.position.y - nodeB.position.y);

        return distanceX + distanceY;
    }

    private Vector3Int[] RetracePath(Node startNode, Node endNode)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path.ToArray();
    }

    private class Node
    {
        public Vector3Int position;
        public bool walkable = true;
        public int gCost;
        public int hCost;
        public Node parent;

        public int fCost => gCost + hCost;

        public Node(Vector3Int position)
        {
            this.position = position;
        }

        public void Reset()
        {
            gCost = int.MaxValue;
            hCost = 0;
            parent = null;
        }
    }
}
