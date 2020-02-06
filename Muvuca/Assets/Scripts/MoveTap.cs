using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveTap : MonoBehaviour {
	[SerializeField] NodeGrid _grid = null;
	[SerializeField] Rigidbody2D _rb = null;
	[SerializeField] float _speed = 0;
	Action<Vector3[], bool> _pathfindingCallback;
	Vector3[] _path = null;
	bool _newPath = false;
	bool _doMovement = false;

	void Awake() {
		_pathfindingCallback += ReceivedPath;
	}

	void Update() {
		if (TapInput.Instance.IsSingle) {
			PathRequestManager.instance.RequestPath(transform.position, TapInput.Instance.WorldPosition, _grid, _pathfindingCallback);
		}
	}

	void FixedUpdate() {
		if (_newPath) {
			if (_path.Length > 0) {
				_doMovement = true;
			}
			else {
				_doMovement = false;
				_rb.velocity = Vector2.zero;
			}
		}

		if (_doMovement) {
			// move between points.
		}
	}

	void ReceivedPath(Vector3[] path, bool success) {
		_path = path;
		_newPath = true;
	}

	void MoveOnPath() {

	}
}
