using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTouch : MonoBehaviour {
	void Update() {
		for (int i = 0; i < Input.touchCount; i++) {
			Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
			switch (i) {
				case 0:
					Debug.DrawLine(Vector3.zero, touchPosition, Color.red);
					break;
				case 1:
					Debug.DrawLine(Vector3.zero, touchPosition, Color.blue);
					break;
				case 2:
					Debug.DrawLine(Vector3.zero, touchPosition, Color.green);
					break;
				case 3:
					Debug.DrawLine(Vector3.zero, touchPosition, Color.magenta);
					break;
				case 4:
					Debug.DrawLine(Vector3.zero, touchPosition, Color.cyan);
					break;
				case 5:
					Debug.DrawLine(Vector3.zero, touchPosition, Color.yellow);
					break;
				default:
					Debug.DrawLine(Vector3.zero, touchPosition, Color.black);
					break;
			}
		}
	}
}
