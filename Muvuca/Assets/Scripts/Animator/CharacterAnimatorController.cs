using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour {
	Animator _animator = null;
	Rigidbody2D _rb = null;

	void Start() {
		_animator = GetComponent<Animator>();
		_rb = GetComponent<Rigidbody2D>();
	}

	void Update() {
		_animator.SetFloat("angle", Mathf.Atan2(_rb.velocity.y, _rb.velocity.x));
		_animator.SetFloat("velocity", _rb.velocity.magnitude);
	}
}
