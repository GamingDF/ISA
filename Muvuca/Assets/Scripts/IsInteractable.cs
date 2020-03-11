using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IsInteractable : MonoBehaviour {
	PlayMakerFSM _fsm;
	Collider2D _collider;
	Action<bool> _interactionCallback;
	PlayerMovement _player = null;

	void Start() {
		_interactionCallback += GotToInteract;

		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		if (!_player) {
			Debug.LogWarning("No Player with PlayerMovement in the scene.");
		}

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
				_player.MoveToPosition(gameObject, _interactionCallback);
			}
		}
	}

	void GotToInteract(bool interacted) {
		if (interacted) {
			_fsm.Fsm.Event("Interacted");
		}
	}
}
