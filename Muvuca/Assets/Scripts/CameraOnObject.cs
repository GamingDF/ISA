using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOnObject : MonoBehaviour {
	[SerializeField] Transform _target = null;
	[SerializeField] Vector2 _offset = Vector2.zero;

	void Update() {
		if (_target) {
			transform.position = new Vector3(_target.position.x + _offset.x, _target.position.y + _offset.y, transform.position.z);
		}
	}
}
