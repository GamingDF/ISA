using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveTap : MonoBehaviour {
	[SerializeField] NodeGrid _grid = null;
	[SerializeField] Rigidbody2D _rb = null;
	[SerializeField] float _speed = 0;
	float _epsilon = 0;
	Action<Vector3[], bool> _pathfindingCallback;
	Vector3[] _path = null;
	bool _doMovement = false;
	bool _forceStop = false;
	int _i = 0;

	void Awake() {
		_pathfindingCallback += ReceivedPath;
		_epsilon = _speed * Time.fixedDeltaTime;
	}

	void Update() {
		if (TapInput.Instance.IsSingle) {
			PathRequestManager.instance.RequestPath(transform.position, TapInput.Instance.WorldPosition, _grid, _pathfindingCallback);
		}

		_forceStop = AnalogInput.Instance.IsActive;
	}

	void FixedUpdate() {
		if (_doMovement) {
			if ((transform.position - _path[_i]).magnitude < _epsilon) {
				_i++;
			}

			if (_i >= _path.Length || _forceStop) {
				StopMovement();
			}
			else {
				Vector3 target = _path[_i] - transform.position;
				_rb.velocity = target.normalized * _speed;
			}
		}
	}

	void ReceivedPath(Vector3[] path, bool success) {
		_path = path;
		if (success) {
			StartMovement();
		}
		else {
			StopMovement();
		}
	}

	void StartMovement() {
		_i = 0;
		_doMovement = true;
	}

	void StopMovement() {
		_doMovement = false;
		_rb.velocity = Vector3.zero;
	}
}
