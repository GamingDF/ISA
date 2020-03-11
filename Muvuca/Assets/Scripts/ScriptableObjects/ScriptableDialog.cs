using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/dialog", order = 1)]
public class ScriptableDialog : ScriptableObject {
	public Dialogue[] dialog;
}
