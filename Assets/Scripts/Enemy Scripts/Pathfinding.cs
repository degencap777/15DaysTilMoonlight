﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;
using System;

public class Pathfinding : MonoBehaviour
{
    Grid grid;
    public int count;
    List<Node> pathFound;
    PathRequestManager requestManager;

    // Use this for initialization
    void Awake()
    {
        grid = GetComponent<Grid>();
        requestManager = GetComponent<PathRequestManager>();
    }

    public void StartFindPath(Vector2 startPos, Vector2 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }
    public IEnumerator FindPath(Vector2 startPos, Vector2 targetPos)
    {
        Vector2[] waypoints = new Vector2[0];
        bool pathSuccess = false;
        //Stopwatch sw = new Stopwatch();
        //sw.Start();
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        // int counter = 0;
        // while (!targetNode.walkable)
        // {
        //     counter++;
        //     targetNode = grid.NodeFromWorldPointAlternate(targetPos, counter);
        // }
        // while(!startNode.walkable){
        //     counter++;
        //     startNode = grid.NodeFromWorldPointAlternate(startPos, counter);
        // }

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                //sw.Stop();
                //print("Path found: " + sw.ElapsedMilliseconds + " ms");
                pathSuccess = true;
                waypoints = RetracePath(startNode, targetNode);

                break;
            }
            foreach (Node neighbor in grid.GetNeighbors(currentNode))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                {
                    continue;
                }

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                    else
                    {
                        openSet.UpdateItem(neighbor);
                    }
                }
            }
        }
        yield return null;
        //requestManager.FinishedProcessingPath(waypoints);
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        // if (pathSuccess)
        // {

        // }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
        //return pathFound;
    }

    Vector2[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        //Vector2[] waypoints = SimplifyPath(path);
        // waypoints.Reverse();
        Vector2[] waypoints = SimplifyPath(path);
        //waypoints.ToArray();
        Array.Reverse(waypoints);
        return waypoints;
        //path.Reverse();

        //grid.path = path;
        //return path;
    }

    Vector2[] SimplifyPath(List<Node> path)
    {
        List<Vector2> waypoints = new List<Vector2>();
        Vector2 directionOld = Vector2.zero;
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i-1].worldPosition);
            }
            directionOld = directionNew;
        }
        // for (int i = 1; i < path.Count; i++)
        // {
        //     waypoints.Add(path[i - 1].worldPosition);
        // }
        // //Vector2[] waypoints = SimplifyPath(path);
        // // waypoints.Reverse();
        // waypoints.ToArray();
        return waypoints.ToArray();
    }
    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
