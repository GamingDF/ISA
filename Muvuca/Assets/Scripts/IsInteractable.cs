using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInteractable : MonoBehaviour {
	PlayMakerFSM _fsm;
	Collider2D _collider;

	void Start() {
		_fsm = gameObject.GetComponent<PlayMakerFSM>();
		if (!_fsm) {
			Debug.LogWarning("No PlayMakerFSM in the object: " + gameObject.name);
		}

		_collider = gameObject.GetComponent<Collider2D>();
		if (!_collider) {
			Debug.LogWarning("No Collider2D in the object: " + gameObject.name);
		}
	}

	void Update() {
		if (TapInput.Instance.IsSingle) {
			if (_collider.OverlapPoint(TapInput.Instance.WorldPosition)) {
				_fsm.Fsm.Event("Interacted");
			}
		}
	}
}
