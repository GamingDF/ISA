using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnalog : MonoBehaviour {
	[SerializeField] Rigidbody2D _rb = null;
	[SerializeField] float _speed = 1;
	Vector2 _touchPosition;
	Vector2 _input;

	void Update() {
		_input = AnalogInput.Instance.Axis;
	}

	void FixedUpdate() {
		_rb.velocity = _input * _speed;
	}
}
