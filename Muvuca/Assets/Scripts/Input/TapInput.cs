using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapInput : MonoBehaviour {
	float _timer = 0;
	[SerializeField] float _tapTime = 0.1f;

	[SerializeField] Vector3 _screenPosition = Vector3.zero;
	[SerializeField] Vector3 _worldPosition = Vector3.zero;
	[SerializeField] bool _isSingle = false;
	[SerializeField] bool _isDouble = false;
	public bool IsSingle => _isSingle;
	public bool IsDouble => _isDouble;
	public Vector3 ScreenPosition => _screenPosition;
	public Vector3 WorldPosition => _worldPosition;

	static TapInput _instance;
	public static TapInput Instance {
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
		_isSingle = false;
		_isDouble = false;
		if (Input.touchCount > 0) {
			foreach (Touch input in Input.touches) {
				if (input.fingerId == 0) {
					switch (input.phase) {
						case TouchPhase.Began:
							_timer = 0;
							break;

						case TouchPhase.Stationary:
							_timer += Time.deltaTime;
							break;

						case TouchPhase.Ended:
							if (_timer < _tapTime) {
								_isSingle = true;
								_screenPosition = input.position;
								_worldPosition = Camera.main.ScreenToWorldPoint(input.position);
								_worldPosition.z = 0;
							}
							break;

						default:
							_timer = _tapTime;
							break;
					}
					break;
				}
			}
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(WorldPosition, 0.5f);
	}
}
