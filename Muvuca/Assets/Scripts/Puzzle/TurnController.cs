using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public int avSeeds;
    public int pSeeds;
    public int sSeeds;
    public int cSeeds;

    public bool interturns;
    [SerializeField]
    private int turnsQuantity;
    [SerializeField]
    private Text turnsText;

    public int turnsLeft;
    private PuzzleArea area;
    private MapController map;

    public static TurnController Instance { get; private set; }

    private void Awake() {
        if(Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<PuzzleArea>();
        map = GetComponent<MapController>();
        StartPuzzle();
    }

    // Update is called once per frame
    void Update()
    {
        if (interturns) {
            int victoryDegree = 0;
            List<List<PuzzleCell>> cellsAux = new List<List<PuzzleCell>>(); ;

            int count = 0;
            foreach (List<PuzzleCell> l in area.cells) {
                cellsAux.Add(new List<PuzzleCell>());
                foreach (PuzzleCell p in l) {
                    if (p.plants == PuzzleCell.PlantType.C) {
                        cellsAux[count].Add(new PuzzleCell(p.plants, p.influenced, p.plantsToGrow.ToArray()));
                    }
                    else {
                        cellsAux[count].Add(new PuzzleCell(p.plants, p.influenced));
                    }
                }
                count++;
            }

            int testCount = 0;
            for (int i = 0; i < area.cells.Count; i++) {
                for (int j = 0; j < area.cells[0].Count; j++) {
                    if (cellsAux[i][j].plants != PuzzleCell.PlantType.None) {
                        Debug.Log(testCount++);
                        if (cellsAux[i][j].plants == PuzzleCell.PlantType.AV) {
                            area.ApplyGrowth(new Vector2Int(i, j), cellsAux[i][j].plants);
                        }
                        else if (cellsAux[i][j].plants == PuzzleCell.PlantType.P) {
                            area.cells[i][j].turnsToGrow--;
                            if(area.cells[i][j].turnsToGrow <= 0) {
                                area.ApplyGrowth(new Vector2Int(i, j), cellsAux[i][j].plants);
                            }
                        }
                        else if (cellsAux[i][j].plants == PuzzleCell.PlantType.S) {
                            area.cells[i][j].turnsToGrow--;
                            if (area.cells[i][j].turnsToGrow <= 0) {
                                area.ApplyGrowth(new Vector2Int(i, j), cellsAux[i][j].plants);
                            }
                        }
                        else if (cellsAux[i][j].plants == PuzzleCell.PlantType.C) {
                            if (CheckOnClimax(area.cells[i][j])) {
                                victoryDegree++;
                            }
                        }
                    }
                }
            }

            //Teste de vitória e derrota
            if (turnsLeft > 0) {
                if (CheckVictory()) {
                    Debug.Log("Victory!");
                    Debug.Log("Victory Deegre: " + victoryDegree);
                }
                turnsLeft--;
                turnsText.text = "Turns Left: " + turnsLeft;
            }
            else {
                if (CheckVictory()) {
                    Debug.Log("Victory!");
                }
                else {
                    Debug.Log("Defeat!");
                }
            }

            interturns = false;
        }
    }

    public bool CheckVictory() {
        for(int i = 0; i < area.cells.Count; i++) {
            for (int j = 0; j < area.cells[0].Count; j++) {
                if(area.cells[i][j].plants == PuzzleCell.PlantType.None &&
                    area.cells[i][j].influenced == PuzzleCell.InfluenceType.None) {
                    if (area.cells[i][j].isObstacle) {
                        continue;
                    }

                    Debug.Log("Not complete");
                    return false;
                }
            }
        }

        return true;
    }

    public bool CheckOnClimax(PuzzleCell cell) {
        int count = 0;
        foreach(PuzzleCell.PlantType p in cell.plantsToGrow) {
            Debug.Log("Iterou");
            if (cell.progressToWin.Contains(p)) {
                Debug.Log("Count++");
                count++;
            }
        }

        if(count >= cell.plantsToGrow.Count) {
            Debug.Log("Alcançou");
            return true;
        }
        else {
            return false;
        }
    }

    public void StartPuzzle() {
        turnsLeft = turnsQuantity;
        turnsText.text = "Turns Left: " + turnsLeft;
    }

    public void PassTurn() {
        Debug.Log("Passing turn");
        interturns = true;
        map.drawMap = true;
    }
}
