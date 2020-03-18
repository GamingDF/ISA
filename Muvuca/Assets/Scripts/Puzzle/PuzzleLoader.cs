using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleLoader : MonoBehaviour
{
    public static PuzzleLoader Instance { get; private set; }

    public string LoadedPuzzle { get; private set; }

    public void Awake() {
        if(Instance  != null) {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Teste
        LoadPuzzleByName("teste");
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

        //Provisório, deve ser integrado ao sistema de transições de cena
        //SceneManager.LoadScene("Puzzle");
    }
}
