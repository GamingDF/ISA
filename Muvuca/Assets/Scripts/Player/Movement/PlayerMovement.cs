using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] NodeGrid _grid = null;
	[SerializeField] Rigidbody2D _rb = null;
	[SerializeField] float _speed = 0;

	Action<Vector3[], bool> _pathfindingCallback;
	Vector3[] _path = null;
	GameObject _pathTarget = null;
	Action<bool> _targetCallback;
	Vector2 _input;
	bool _stop = false;
	bool _doPath = false;
	bool _freeze = false;
	int _i = 0;

	void Start() {
		_pathfindingCallback += ReceivedPath;
	}

	void Update() {
		if (!_freeze) {
			if (TapInput.Instance.IsSingle) {
				PathRequestManager.instance.RequestPath(transform.position, TapInput.Instance.WorldPosition, _grid, _pathfindingCallback);
			}

			_input = AnalogInput.Instance.Axis;
			if (AnalogInput.Instance.Deactivated) {
				_stop = true;
			}
		}
	}

	public void MoveToPosition(GameObject target, Action<bool> callback) {
		if (!_freeze) {
			PathRequestManager.instance.RequestPath(transform.position, target.transform.position, _grid, _pathfindingCallback);
			_pathTarget = target;
			_targetCallback = callback;
		}
	}

	public void MoveToPosition(Vector3 target) {
		PathRequestManager.instance.RequestPath(transform.position, target, _grid, _pathfindingCallback);
	}

	void FixedUpdate() {
		if (AnalogInput.Instance.IsActive && !_freeze) {
			_rb.velocity = _input * _speed;
		}
		else if (_doPath) {
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
				_targetCallback(false);
				_pathTarget = null;
			}

			_stop = false;
		}
	}

	void ReceivedPath(Vector3[] path, bool success) {
		_path = path;
		if (success) {
			_doPath = true;
		}
		else {
			_doPath = false;
			_stop = true;
		}
	}

	public void FreezeMovement() {
		StartCoroutine(SetFreeze(true));
		_stop = true;
	}

	public void UnfreezeMovement() {
		StartCoroutine(SetFreeze(false));
	}

	IEnumerator SetFreeze(bool value) {
		yield return null;
		_freeze = value;
		yield break;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (_pathTarget) {
			if (other.gameObject == _pathTarget) {
				_targetCallback(true);
				_pathTarget = null;
				_stop = true;
			}
		}
	}

	void OnCollisionStay2D(Collision2D other) {
		if (_pathTarget) {
			if (other.gameObject == _pathTarget) {
				_targetCallback(true);
				_pathTarget = null;
				_stop = true;
			}
		}
	}
}
