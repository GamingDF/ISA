using UnityEngine;
using System.Collections;
using System;

public class NPCMovement : MonoBehaviour {
	[SerializeField] NodeGrid _grid = null;
	[SerializeField] Rigidbody2D _rb = null;
	[SerializeField] float _speed = 0;

	Action<Vector3[], bool> _pathfindingCallback;
	Vector3[] _path = null;
	GameObject _pathTarget = null;
	bool _stop = false;
	bool _doPath = false;
	int _i = 0;

	void Start() {
		_pathfindingCallback += ReceivedPath;

		_rb.constraints = RigidbodyConstraints2D.FreezeAll;
	}

	public void MoveToPosition(Vector3 target) {
		PathRequestManager.instance.RequestPath(transform.position, target, _grid, _pathfindingCallback);
	}

	void FixedUpdate() {
		if (_doPath) {
			float epsilon = _speed * Time.fixedDeltaTime;
			if ((transform.position - _path[_i]).magnitude < epsilon) {
				_i++;
			}

			if (_i >= _path.Length) {
				_stop = true;
			}
			else {
				Vector3 target = _path[_i] - transform.position;
				_rb.velocity = target.normalized * _speed;
			}
		}

		if (_stop) {
			_rb.velocity = Vector2.zero;
			_doPath = false;
			_i = 0;
			if (_pathTarget) {
				_pathTarget = null;
			}

			_rb.constraints = RigidbodyConstraints2D.FreezeAll;
			_stop = false;
		}
	}

	void ReceivedPath(Vector3[] path, bool success) {
		_path = path;
		if (success) {
			_doPath = true;
			_rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
		}
		else {
			_doPath = false;
			_stop = true;
		}
	}
}
