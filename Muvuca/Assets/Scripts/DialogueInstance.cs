using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInstance : MonoBehaviour {
	[Tooltip("Diálogos relacionados ao objeto. Cada elemento é um branch do diálogo" +
		"(usar apenas um se o diálogo não possui branch). Aumentar o size aumenta a quantidade de branches.")]
	[SerializeField]
	private Dialogue[] dialogues = new Dialogue[0];

	private void Update() {
		//Input de teste
		if (Input.GetKeyDown(KeyCode.D)) {
			DialogueController.Instance.CallDialogue(dialogues);
		}
	}
}
