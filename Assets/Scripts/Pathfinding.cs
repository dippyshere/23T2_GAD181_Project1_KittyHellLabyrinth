using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    private class Node
    {
        public Vector3Int position;
        public int gCost;
        public int hCost;
        public int fCost;
        public Node parent;
    }

    public int maxPathfindingIterations = 100;

    private Tilemap tilemap;
    private Vector3Int[] directions = {
        new Vector3Int(1, 0, 0),   // Right
        new Vector3Int(-1, 0, 0),  // Left
        new Vector3Int(0, 1, 0),   // Up
        new Vector3Int(0, -1, 0)   // Down
    };

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public Vector3Int[] FindPath(Vector3Int startTile, Vector3Int targetTile)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        HashSet<Vector3Int> visitedTiles = new HashSet<Vector3Int>();
        List<Node> openList = new List<Node>();

        Node startNode = new Node
        {
            position = startTile,
            gCost = 0,
            hCost = CalculateHeuristic(startTile, targetTile),
            fCost = 0,
            parent = null
        };

        openList.Add(startNode);

        int iterations = 0;
        while (openList.Count > 0 && iterations < maxPathfindingIterations)
        {
            Node currentNode = GetLowestFCostNode(openList);
            openList.Remove(currentNode);

            if (currentNode.position == targetTile)
            {
                return RetracePath(startNode, currentNode);
            }

            visitedTiles.Add(currentNode.position);

            foreach (Vector3Int direction in directions)
            {
                Vector3Int neighborPos = currentNode.position + direction;

                if (!tilemap.HasTile(neighborPos) || visitedTiles.Contains(neighborPos))
                {
                    continue;
                }

                int newGCost = currentNode.gCost + 1;

                Node neighborNode = new Node
                {
                    position = neighborPos,
                    gCost = newGCost,
                    hCost = CalculateHeuristic(neighborPos, targetTile),
                    fCost = newGCost + CalculateHeuristic(neighborPos, targetTile),
                    parent = currentNode
                };

                openList.Add(neighborNode);
            }

            iterations++;
        }

        // No path found
        return null;
    }

    private int CalculateHeuristic(Vector3Int from, Vector3Int to)
    {
        return Mathf.Abs(from.x - to.x) + Mathf.Abs(from.y - to.y);
    }

    private Node GetLowestFCostNode(List<Node> nodeList)
    {
        Node lowestFCostNode = nodeList[0];
        for (int i = 1; i < nodeList.Count; i++)
        {
            if (nodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = nodeList[i];
            }
        }
        return lowestFCostNode;
    }

    private Vector3Int[] RetracePath(Node startNode, Node endNode)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        Node currentNode = endNode;

        while (currentNode != null && currentNode != startNode)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path.ToArray();
    }
}
