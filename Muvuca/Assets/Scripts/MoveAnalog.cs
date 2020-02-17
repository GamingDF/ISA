using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnalog : MonoBehaviour {
	[SerializeField] Rigidbody2D _rb = null;
	[SerializeField] float _speed = 1;
	Vector2 _touchPosition;
	Vector2 _input;
	bool _stop = false;

	void Update() {
		_input = AnalogInput.Instance.Axis;
		if (AnalogInput.Instance.Deactivated) {
			_stop = true;
		}
	}

	void FixedUpdate() {
		if (AnalogInput.Instance.IsActive) {
			_rb.velocity = _input * _speed;
		}
		else if (_stop) {
			_rb.velocity = Vector2.zero;
			_stop = false;
		}
	}
}
