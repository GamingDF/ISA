using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalogInput : MonoBehaviour {
	[SerializeField] float _analogRadius = 1;
	[SerializeField] float _deadZoneRadius = 0.5f;
	[SerializeField] float _timeToActive = 1;
	Vector3 _analogPosition = Vector3.zero;
	float _timer = 0;
	bool _active = false;
	float _horizontal;
	float _vertical;
	public Vector2 _axis;
	public float Horizontal => _horizontal;
	public float Vertical => _vertical;
	public Vector2 Axis => _axis;

	static AnalogInput _instance;
	public static AnalogInput Instance {
		get {
			return _instance;
		}
	}

	void Awake() {
		if (_instance == null) {
			_instance = this;
		}
		else {
			Debug.LogWarning("Duplicate of singleton, destroying: " + gameObject.name);
			Destroy(gameObject);
		}
	}

	void Update() {
		CheckActive();
		if (_active) {
			UpdateInput();
		}
	}

	void CheckActive() {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);
			if (_active) {
				if (touch.phase == TouchPhase.Ended) {
					_active = false;
					_analogPosition = Vector3.zero;
					_timer = 0;

					_horizontal = 0;
					_vertical = 0;
					_axis = Vector2.zero;
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

	void UpdateInput() {
		Touch touch = Input.GetTouch(0);
		Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
		Vector2 diff = position - _analogPosition;
		float magnitude = diff.magnitude;

		magnitude = Mathf.Clamp(magnitude, _deadZoneRadius, _analogRadius) - _deadZoneRadius;
		magnitude = magnitude / (_analogRadius - _deadZoneRadius);
		diff = magnitude * diff.normalized;

		_horizontal = diff.x;
		_vertical = diff.y;
		_axis = new Vector2(diff.x, diff.y);
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(_analogPosition, _analogRadius);
		Gizmos.color = Color.black;
		Gizmos.DrawWireSphere(_analogPosition, _deadZoneRadius);
	}
}
