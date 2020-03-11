using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
	private PathRequestManager requestManager;

	void Awake() {
		requestManager = GetComponent<PathRequestManager>();
	}

	public void StartFindPath(Vector3 startPos, Vector3 targetPos, NodeGrid grid) {
		StartCoroutine(FindPath(startPos, targetPos, grid));
	}

	IEnumerator FindPath(Vector3 startPos, Vector3 targetPos, NodeGrid grid) {
		Vector3[] waypoints = new Vector3[0];
		bool pathSucces = false;

		if(grid != null){
			Node startNode = grid.GetNodeFromWorldPoint(startPos);
			Node targetNode = grid.GetNodeFromWorldPoint(targetPos);

			if(/*startNode.walkable && */targetNode.walkable) {
				NodeHeap openSet = new NodeHeap(grid.MaxSize);
				HashSet<Node> closedSet = new HashSet<Node>();
				openSet.Add(startNode);

				while(openSet.Count > 0) {
					Node currentNode = openSet.RemoveFirst();
					closedSet.Add(currentNode);

					if(currentNode == targetNode) {
						pathSucces = true;
						break;
					}

					foreach(Node neighbour in grid.GetNeighbours(currentNode)) {
						if(!neighbour.walkable || closedSet.Contains(neighbour))
							continue;

						int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

						if(newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
							neighbour.gCost = newCostToNeighbour;
							neighbour.hCost = GetDistance(neighbour, targetNode);
							neighbour.parent = currentNode;

							if(!openSet.Contains(neighbour))
								openSet.Add(neighbour);
							else
								openSet.UpdateItem(neighbour);
						}
					}
				}
			}

			yield return null;

			if(pathSucces) {
				waypoints = RetracePath(startNode, targetNode, grid);
				pathSucces = (waypoints.Length > 0);
			}

			requestManager.FinishProcessingPath(waypoints, pathSucces);
		}
	}

	Vector3[] RetracePath(Node startNode, Node endNode, NodeGrid grid) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		if(startNode == endNode) {
			Vector3[] vec = new Vector3[1];
			vec[0] = endNode.worldPos;
			return vec;
		}

		while(currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}

		grid.pathA = path;

		Vector3[] waypoints = SimplifyPath(path);

		grid.pathS = waypoints;

		Array.Reverse(waypoints);

		return waypoints;
	}

	Vector3[] SimplifyPath(List<Node> path) {
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;

		waypoints.Add(path[0].worldPos);
		for(int i = 1; i < path.Count; i++) {
			Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX, path[i-1].gridY - path[i].gridY);

			if(directionNew != directionOld) {
				waypoints.Add(path[i].worldPos);
			}
			directionOld = directionNew;
		}

		return waypoints.ToArray();
	}

	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if(dstX > dstY)
			return 14*dstY + 10*(dstX-dstY);
		return 14*dstX + 10*(dstY-dstX);
	}

	private void OnDestroy(){
        StopCoroutine("FindPath");
    }
}
