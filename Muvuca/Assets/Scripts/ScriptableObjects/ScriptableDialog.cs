using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/Dialog", order = 1)]
public class ScriptableDialog : ScriptableObject {
	public Dialogue[] dialog;
}
