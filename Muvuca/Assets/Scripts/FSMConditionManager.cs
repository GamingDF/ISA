using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMConditionManager : MonoBehaviour {
	Dictionary<string, bool> _conditions = new Dictionary<string, bool>();

	static FSMConditionManager _instance;
	public static FSMConditionManager Instance {
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

	void Start() {
		_conditions = SaveSystem.Instance.LoadConditions();
	}

	void OnDestroy() {
		SaveSystem.Instance.SaveConditions(_conditions);
	}

	public void SetCondition(string key, bool value) {
		_conditions[key] = value;
	}

	public bool GetCondition(string key) {
		if (_conditions.ContainsKey(key)) {
			return _conditions[key];
		}
		else {
			return false;
		}
	}

	public bool ExistCondition(string key) {
		if (_conditions.ContainsKey(key)) {
			return true;
		}
		else {
			return false;
		}
	}

	public void ResetCondition() {
		_conditions = new Dictionary<string, bool>();
		SaveSystem.Instance.ResetConditions();
	}
}
