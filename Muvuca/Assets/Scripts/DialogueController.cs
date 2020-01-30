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
    private GameObject dialogueBox;
    [SerializeField]
    private bool playSoundOnText;

    public bool onDialogue;

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
        //Teste de diálogo
        if (Input.GetKeyDown(KeyCode.D)) {
            CallDialogue("[fteste]aAAAaaaaAAAAAaaaaAaAAAaA");
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

    public void CallDialogue(string d) {
        onDialogue = true;
        dialogueBox.SetActive(true);
        dialogueBox.transform.Find("Text").GetComponent<Text>().text = "";

        StartCoroutine(GetComponent<TextParser>().Parse(d));
    }
}
