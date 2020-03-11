using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleDefeatMenu : MonoBehaviour
{
    public void RestartPuzzle() {
        //Reloada a scene(provisório)
        SceneManager.LoadScene("Puzzle");
    }

    public void GoBackToExploration() {
        //Retorna a exploração. Deve ser implementado
    }

    public void ExitGame() {
        //Manda para o menu principal. Deve ser implementado
    }
}
