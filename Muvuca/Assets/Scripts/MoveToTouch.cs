using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTouch : MonoBehaviour {
	[SerializeField] Rigidbody2D _rb = null;
	[SerializeField] float _speed = 1;
	Vector2 _touchPosition;

	void Update() {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) {
				_touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
			}
		}
	}

	void FixedUpdate() {
		float step = _speed * Time.fixedDeltaTime;
		_rb.MovePosition(Vector2.MoveTowards(transform.position, _touchPosition, _speed));
	}
}
