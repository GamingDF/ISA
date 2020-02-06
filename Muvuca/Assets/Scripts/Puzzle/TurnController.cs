using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public int avSeeds;
    public int pSeeds;
    public int sSeeds;
    public int cSeeds;

    public bool interturns;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (interturns) {
            List<List<PuzzleCell>> cellsAux = new List<List<PuzzleCell>>(); ;

            int count = 0;
            foreach (List<PuzzleCell> l in area.cells) {
                cellsAux.Add(new List<PuzzleCell>());
                foreach (PuzzleCell p in l) {
                    cellsAux[count].Add(new PuzzleCell(p.plants, p.influenced));
                }
                count++;
            }

            int testCount = 0;
            for (int i = 0; i < area.cells.Count; i++) {
                for (int j = 0; j < area.cells[0].Count; j++) {
                    if (cellsAux[i][j].plants != PuzzleCell.PlantType.None) {
                        Debug.Log(testCount++);
                        if(cellsAux[i][j].plants == PuzzleCell.PlantType.AV)
                            area.ApplyGrowth(new Vector2Int(i, j), cellsAux[i][j].plants);
                    }
                }
            }

            interturns = false;
        }
    }

    public void PassTurn() {
        Debug.Log("Passing turn");
        interturns = true;
        map.drawMap = true;
    }
}
