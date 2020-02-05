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
            var cellsAux = area.cells;
            int testCount = 0;
            for (int i = 0; i < area.cells.Count; i++) {
                for (int j = 0; j < area.cells[0].Count; j++) {
                    foreach (PuzzleCell.PlantType v in cellsAux[i][j].plants) {
                        Debug.Log(testCount++);
                        area.ApplyGrowth(new Vector2Int(i, j), v);
                    }
                }
            }

            interturns = false;
        }
    }

    public void PassTurn() {
        interturns = true;
        map.drawMap = true;
    }
}
