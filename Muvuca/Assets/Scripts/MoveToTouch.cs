using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTouch : MonoBehaviour {
	public Rigidbody2D rb = null;
	Vector2 touchPosition;
	public float speed = 1;

	void Update() {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) {
				touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
			}
		}
	}

	void FixedUpdate() {
		float step = speed * Time.fixedDeltaTime;
		rb.MovePosition(Vector2.MoveTowards(transform.position, touchPosition, speed));
	}
}
