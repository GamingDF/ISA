using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnalog : MonoBehaviour {
	[SerializeField] float _analogRadius = 1;
	[SerializeField] float _deadZoneRadius = 0.5f;
	[SerializeField] float _timeToActive = 1;
	Vector3 _analogPosition = Vector3.zero;
	float _timer = 0;
	bool _active = false;

	void Update() {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);
			if (_active) {
				if (touch.phase == TouchPhase.Ended) {
					_active = false;
					_analogPosition = Vector3.zero;
					_timer = 0;
				}
			}
			else {
				if (touch.phase == TouchPhase.Stationary) {
					_timer += Time.deltaTime;
					if (_timer >= _timeToActive) {
						_active = true;
						_analogPosition = Camera.main.ScreenToWorldPoint(touch.position);
						_analogPosition.z = 0;
					}
				}
				else {
					_timer = 0;
				}
			}
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(_analogPosition, _analogRadius);
		Gizmos.color = Color.black;
		Gizmos.DrawWireSphere(_analogPosition, _deadZoneRadius);
	}
}
