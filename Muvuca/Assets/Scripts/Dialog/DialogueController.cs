using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {
	public static DialogueController Instance {
		get;
		private set;
	}

	[SerializeField]
	private GameObject dialogueBox = null;
	[SerializeField]
	private bool playSoundOnText = false;

	public Dialogue[] toShow;

	public bool onDialogue;
	public bool goToNextDialogue;
	public bool startDialogue;

	public int branch = 0;
	public int dNumber = 0;

	PlayMakerFSM _fsm = null;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
			return;
		}
	}

	private void Update() {
		if (startDialogue) {
			startDialogue = false;
			Debug.Log(branch + " " + dNumber);
			NextDialogue(toShow[branch].text[dNumber]);
		}

		if (Input.GetKeyDown(KeyCode.Return) || TapInput.Instance.IsSingle) {
			if (goToNextDialogue) {
				goToNextDialogue = false;
				dNumber++;
				if (dNumber < toShow[branch].text.Length) {
					NextDialogue(toShow[branch].text[dNumber]);
				}
				else {
					OnDialogEnd();
				}
			}
			else {
				GetComponent<TextParser>().jumpToEnd = true;
			}
		}
	}

	public void ChangePortrait(string name) {
		dialogueBox.transform.Find("Portrait").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Portraits/" + name);
	}

	public void InsertChar(char c) {
		dialogueBox.transform.Find("Text").GetComponent<Text>().text += c;
		if (playSoundOnText) {
			GetComponent<AudioSource>().Play();
		}
	}

	public void CallDialogue(ScriptableDialog dialog, PlayMakerFSM fsm) {
		onDialogue = true;
		startDialogue = true;
		_fsm = fsm;

		toShow = dialog.dialog;

		dialogueBox.SetActive(true);
	}

	private void OnDialogEnd() {
		onDialogue = false;
		dialogueBox.SetActive(false);
		if (_fsm != null) {
			_fsm.Fsm.Event("DialogEnd");
		}
		_fsm = null;
		branch = 0;
		dNumber = 0;
	}

	private void NextDialogue(string d) {
		dialogueBox.transform.Find("Text").GetComponent<Text>().text = "";

		StartCoroutine(GetComponent<TextParser>().Parse(d));
	}
}
