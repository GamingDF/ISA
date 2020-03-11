using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour {
	static SaveSystem _instance;
	public static SaveSystem Instance {
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

	public void SaveConditions(Dictionary<string, bool> conditions) {
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/Conditions.banana";
		Debug.Log(path);
		FileStream stream = new FileStream(path, FileMode.Create);

		formatter.Serialize(stream, conditions);
		stream.Close();
	}

	public Dictionary<string, bool> LoadConditions() {
		string path = Application.persistentDataPath + "/Conditions.banana";
		Debug.Log(path);
		if (File.Exists(path)) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			return formatter.Deserialize(stream) as Dictionary<string, bool>;
		}
		else {
			return new Dictionary<string, bool>();
		}
	}

	public void ResetConditions() {
		string path = Application.persistentDataPath + "/Conditions.banana";
		Debug.Log(path);
		if (File.Exists(path)) {
			File.Delete(path);
		}
	}
}
