using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleLoader : MonoBehaviour
{
    public static PuzzleLoader Instance { get; private set; }

    public string LoadedPuzzle { get; private set; }

    private SceneHandler _sceneHandler = null;
    [SerializeField] GameObject _transition = null;

    private void Awake() {
        if(Instance  != null) {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _sceneHandler = FindObjectOfType<SceneHandler>() ? FindObjectOfType<SceneHandler>().GetComponent<SceneHandler>() : null;
    }

    private void Update() {
        //Teste
        if (Input.GetKeyDown(KeyCode.Return)) {
            LoadPuzzleByName("teste");
        }
    }

    public void LoadPuzzleByName(string puzzleName) {
        //Carrega um txt compilado junto ao executável
        TextAsset txt = (TextAsset)Resources.Load("PuzzleData/" + puzzleName, typeof(TextAsset));
        LoadedPuzzle = txt.text;

        Debug.Log(LoadedPuzzle);
        CallPuzzleTransition();
    }

    private void CallPuzzleTransition() {
        //Troca para a cena do Puzzle, construindo ela com base nos parâmetros
        //presentes na string LoadedPuzzle

        if (!_sceneHandler) {
            Debug.LogError("No SceneHandler in the scene!");
        }
        else {
            _sceneHandler.LoadScene("Puzzle", _transition);
        }
    }
}
