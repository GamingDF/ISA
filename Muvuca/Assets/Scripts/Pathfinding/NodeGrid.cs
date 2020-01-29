using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NodeGrid : MonoBehaviour{
	public LayerMask unwalkableMask;
	public bool displayGridGizmos;

	public Vector2 gridWorldSize;
	public float nodeRadius;
	float nodeDiameter;
	int gridSizeX, gridSizeY;
	Node[,] grid;
	public int MaxSize {
		get {
			return gridSizeX * gridSizeY;
		}
	}

	public List<Node> pathA;
	public Vector3[] pathS;

	public void LoadGrid() {
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - new Vector3(gridWorldSize.x/2, gridWorldSize.y / 2, 0);

		for(int i = 0; i < gridSizeX; i++) {
			for(int j = 0; j < gridSizeY; j++) {
				Vector3 worldPos = worldBottomLeft + new Vector3(i*nodeDiameter + nodeRadius, j * nodeDiameter + nodeRadius, 0);
				bool walkable = !(Physics2D.OverlapCircle(worldPos, nodeRadius, unwalkableMask));
				grid[i, j] = new Node(i, j, worldPos, walkable);
			}
		}
	}

	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();
		for(int x = -1; x <= 1; x++) {
			for(int y = -1; y <= 1; y++) {
				if(x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX, checkY]);
				}
			}
		}

		return neighbours;
	}

	public Node GetNodeFromWorldPoint(Vector3 worldPos) {
		if(gameObject == null){
			return null;
		}

		float percentX = Mathf.Clamp01((worldPos.x - transform.position.x + gridWorldSize.x/2) / gridWorldSize.x);
		float percentY = Mathf.Clamp01((worldPos.y - transform.position.y + gridWorldSize.y/2) / gridWorldSize.y);

		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);

		return grid[x, y];
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

		if(grid != null && displayGridGizmos) {
			foreach(Node n in grid) {
				Gizmos.color = (n.walkable) ? Color.white : Color.red;

				if(pathA != null) {
					foreach(Node m in pathA) {
						if(n == m) {
							Gizmos.color = Color.black;
							if(pathS != null) {
								foreach(Vector3 v in pathS) {
									if(n == GetNodeFromWorldPoint(v)) {
										Gizmos.color = Color.green;
									}
								}
							}
						}
					}
				}
				Gizmos.DrawCube(n.worldPos, new Vector3(nodeDiameter-0.01f, nodeDiameter - 0.01f, 0.1f));
			}
		}
	}
}
