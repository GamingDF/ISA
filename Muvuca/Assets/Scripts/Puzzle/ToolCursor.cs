using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolCursor : MonoBehaviour
{
    public enum ToolType
    {
        Shadow,
        Sun
    }

    public ToolType tool;

    private Grid g;
    private PuzzleArea a;

    private void Awake() {
        g = GameObject.Find("PuzzleGrid").GetComponent<Grid>();
    }

    private void Start() {
        a = TurnController.Instance.GetComponent<PuzzleArea>();
    }

    // Update is called once per frame
    void Update()
    {
        //Segue o mouse
        var mouseWPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var cell = g.WorldToCell(mouseWPos);
        transform.position = g.GetCellCenterWorld(cell);

        if (Input.GetMouseButtonDown(0)) {
            //Debug.Log("cell: " + cell);
            if (a.CheckNotBoundries(new Vector2Int(cell.x, -cell.y))) {
                var posVector = new Vector2Int(cell.x, -cell.y);
                //Debug.Log("posVector: " + posVector);
                if(a.cells[posVector.x][posVector.y].plants == PuzzleCell.PlantType.P && tool == ToolType.Sun) {
                    a.ApplyGrowth(posVector, PuzzleCell.PlantType.P);
                }
                else if(a.cells[posVector.x][posVector.y].plants == PuzzleCell.PlantType.S && tool == ToolType.Shadow) {
                    a.ApplyGrowth(posVector, PuzzleCell.PlantType.S);
                }

                TurnController.Instance.PassTurn();
            }
        }

        //Teste de troca de ferramenta
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            tool = ToolType.Shadow;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            tool = ToolType.Sun;
        }
    }

    public void SetSunTool() {
        tool = ToolType.Sun;
    }

    public void SetShadowTool() {
        tool = ToolType.Shadow;
    }
}
