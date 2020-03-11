using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public int gridX, gridY;
    public Vector3 worldPos;
    public bool walkable;
    public Node parent;

    public int gCost;
    public int hCost;
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    public int heapIndex;

    public Node(int _gridX, int _gridY, Vector3 _worldPos, bool _walkable) {
        gridX = _gridX;
        gridY = _gridY;
        worldPos = _worldPos;
        walkable = _walkable;
    }

    public int CompareTo(Node node) {
        int compare = fCost.CompareTo(node.fCost);

        if (compare == 0) {
            compare = hCost.CompareTo(node.hCost);
        }

        return -compare;
    }
}
