using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRequestManager : MonoBehaviour {
	private Queue<PathRequest> pathRequestQueue;/*= new Queue<PathRequest>()*/
	private PathRequest currentPathRequest;

	public static PathRequestManager instance;
	Pathfinding pathfinding;

	bool isProcessing;

	void Awake() {
		instance = this;
		pathRequestQueue = new Queue<PathRequest>();
		pathfinding = GetComponent<Pathfinding>();
	}

	public void RequestPath(Vector3 pathStart, Vector3 pathEnd, NodeGrid grid, Action<Vector3[], bool> callback) {
		if(grid != null) {
			grid.LoadGrid();
			PathRequest newRequest = new PathRequest(pathStart, pathEnd, grid, callback);
			pathRequestQueue.Enqueue(newRequest);
			TryProcessNext();
		}
	}

	void TryProcessNext() {
		if(!isProcessing && pathRequestQueue.Count > 0) {
			currentPathRequest = pathRequestQueue.Dequeue();
			isProcessing = true;
			instance.pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd, currentPathRequest.grid);
		}
	}

	public void FinishProcessingPath(Vector3[] path, bool success) {
		currentPathRequest.callback(path, success);
		isProcessing = false;
		TryProcessNext();
	}

	struct PathRequest {
		public Vector3 pathStart;
		public Vector3 pathEnd;
		public NodeGrid grid;
		public Action<Vector3[], bool> callback;

		public PathRequest(Vector3 _pathStart, Vector3 _pathEnd, NodeGrid _grid, Action<Vector3[], bool> _callback) {
			pathStart = _pathStart;
			pathEnd = _pathEnd;
			grid = _grid;
			callback = _callback;
		}
	}
}
