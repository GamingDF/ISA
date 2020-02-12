using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance
    {
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

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
    }

    private void Start() {

    }

    private void Update() {
        if (startDialogue) {
            startDialogue = false;
            Debug.Log(branch + " " + dNumber);
            NextDialogue(toShow[branch].text[dNumber]);
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            if (goToNextDialogue) {
                goToNextDialogue = false;
                dNumber++;
                if (dNumber < toShow[branch].text.Length) {
                    NextDialogue(toShow[branch].text[dNumber]);
                }
                else {
                    onDialogue = false;
                    dialogueBox.SetActive(false);
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

    public void CallDialogue(Dialogue[] d) {
        onDialogue = true;
        startDialogue = true;

        toShow = d;

        dialogueBox.SetActive(true);
    }

    private void NextDialogue(string d) {
        dialogueBox.transform.Find("Text").GetComponent<Text>().text = "";

        StartCoroutine(GetComponent<TextParser>().Parse(d));
    }
}
