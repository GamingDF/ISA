using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PuzzleArea : MonoBehaviour
{
    [Tooltip("Altura e largura da grid")]
    [SerializeField]
    private int gridSize;
    public List<List<PuzzleCell>> cells;

    private Tilemap tilemap;

    private void Awake() {
        cells = new List<List<PuzzleCell>>();
        for(int i = 0; i < gridSize; i++) {
            cells.Add(new List<PuzzleCell>());
            for(int j = 0; j < gridSize; j++) {
                cells[i].Add(new PuzzleCell());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Teste de plantar
        if(Input.GetMouseButtonDown(0)) {
            if(Input.mousePosition.x >= 0 && Input.mousePosition.x < 8) {
                if (Input.mousePosition.y >= 0 && Input.mousePosition.y < 8) {
                    int x, y;
                    x = GetAreaOnGame(Input.mousePosition).x;
                    y = GetAreaOnGame(Input.mousePosition).y;
                    Debug.Log(GetAreaOnGame(Input.mousePosition));

                    if (Input.GetKey(KeyCode.)) {
                        CreatePlant(new Vector2Int(x, y), PuzzleCell.PlantType.AV);
                    }
                    else if (Input.GetKey(KeyCode.Alpha2)) {
                        CreatePlant(new Vector2Int(x, y), PuzzleCell.PlantType.P);
                    }
                    else if (Input.GetKey(KeyCode.Alpha3)) {
                        CreatePlant(new Vector2Int(x, y), PuzzleCell.PlantType.S);
                    }
                    else if (Input.GetKey(KeyCode.Alpha4)) {
                        CreatePlant(new Vector2Int(x, y), PuzzleCell.PlantType.C);
                    }
                }
            }
        }
    }

    public void CreatePlant(Vector2Int c, PuzzleCell.PlantType t) {
        cells[c.x][c.y].plants.Add(t);
    }

    public Vector3Int GetAreaOnGame(Vector3 mousepos) {
        return Vector3Int.FloorToInt(Camera.main.ScreenToWorldPoint(mousepos));
    }

    public void ApplyGrowth(Vector2Int pos, PuzzleCell.PlantType p) {
        //Adubo verde
        if(p == PuzzleCell.PlantType.AV) {
            for(int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {
                    if(CheckNotBoundries(new Vector2Int(pos.x + i, pos.y + j))){
                        if (cells[pos.x + i][pos.y + j].influenced == 0) {
                            cells[pos.x + i][pos.y + j].influenced = PuzzleCell.InfluenceType.AVOnly;
                            if(!cells[pos.x + i][pos.y + j].plants.Contains(PuzzleCell.PlantType.AV)) {
                                cells[pos.x + i][pos.y + j].plants.Add(PuzzleCell.PlantType.AV);
                            }
                        }
                    }
                }
            }
        }

        //Primária
        if (p == PuzzleCell.PlantType.P) {
            for (int i = 0; i <= 2; i++) {
                for (int j = 0; j <= 2; j++) {
                    if (CheckNotBoundries(new Vector2Int(pos.x + i, pos.y + j))){
                        if (cells[pos.x + i][pos.y + j].influenced == PuzzleCell.InfluenceType.AVOnly ||
                            cells[pos.x + i][pos.y + j].influenced == PuzzleCell.InfluenceType.None) {
                            cells[pos.x + i][pos.y + j].influenced = PuzzleCell.InfluenceType.PSingle;
                        }
                        else if(cells[pos.x + i][pos.y + j].influenced == PuzzleCell.InfluenceType.SSingle) {
                            cells[pos.x + i][pos.y + j].influenced = PuzzleCell.InfluenceType.Double;
                        }
                    }
                }
            }
        }

        //Secundária
        if (p == PuzzleCell.PlantType.S) {
            for (int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {
                    if (CheckNotBoundries(new Vector2Int(pos.x + i, pos.y + j))){
                        if (cells[pos.x + i][pos.y + j].influenced == PuzzleCell.InfluenceType.AVOnly ||
                            cells[pos.x + i][pos.y + j].influenced == PuzzleCell.InfluenceType.None) {
                            cells[pos.x + i][pos.y + j].influenced = PuzzleCell.InfluenceType.SSingle;
                        }
                        else if (cells[pos.x + i][pos.y + j].influenced == PuzzleCell.InfluenceType.PSingle) {
                            cells[pos.x + i][pos.y + j].influenced = PuzzleCell.InfluenceType.Double;
                        }
                    }
                }
            }
        }

        //Climax
        if (p == PuzzleCell.PlantType.C) {
            for (int i = -2; i <= 2; i++) {
                for (int j = -2; j <= 2; j++) {
                    if (CheckNotBoundries(new Vector2Int(pos.x + i, pos.y + j))){
                        cells[pos.x + i][pos.y + j].influenced = PuzzleCell.InfluenceType.Double;
                    }
                }
            }
        }
    }

    private bool CheckNotBoundries(Vector2Int pos) {
        if(pos.x < 0 || pos.y < 0 || pos.x >= cells.Count || pos.y >= cells[0].Count) {
            return false;
        }

        return true;
    }
}
