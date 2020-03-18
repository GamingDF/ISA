using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PuzzleArea : MonoBehaviour
{
    [Tooltip("Altura e largura da grid")]
    [SerializeField]
    private int gridSize;

    [SerializeField]
    private Sprite[] obstacleSprites;

    public List<List<PuzzleCell>> cells;

    private Tilemap tilemap;
    private Grid g;
    private MapController map;

    private void Awake() {
        g = GetComponentInChildren<Grid>();
        map = GetComponent<MapController>();
        cells = new List<List<PuzzleCell>>();
        for(int i = 0; i < gridSize; i++) {
            cells.Add(new List<PuzzleCell>());
            for(int j = 0; j < gridSize; j++) {
                cells[i].Add(new PuzzleCell());
            }
        }
    }

    private void Start() {
        string temp = PuzzleLoader.Instance.LoadedPuzzle.Replace(System.Environment.NewLine, "");
        Debug.Log(temp);

        for(int i = 0; i < gridSize; i++) {
            for (int j = 0; j < gridSize; j++) {
                switch(temp[(j * gridSize) + i]) {
                    case 'A':
                        CreatePlant(new Vector2Int(i, j), PuzzleCell.PlantType.AV);
                        break;
                    case 'P':
                        CreatePlant(new Vector2Int(i, j), PuzzleCell.PlantType.P);
                        break;
                    case 'S':
                        CreatePlant(new Vector2Int(i, j), PuzzleCell.PlantType.S);
                        break;
                    case '0'://Climax AV
                        CreatePlant(new Vector2Int(i, j), PuzzleCell.PlantType.C, new PuzzleCell.PlantType[] { PuzzleCell.PlantType.AV });
                        break;
                    case '1'://Climax P
                        CreatePlant(new Vector2Int(i, j), PuzzleCell.PlantType.C, new PuzzleCell.PlantType[] { PuzzleCell.PlantType.P });
                        break;
                    case '2'://Climax S
                        CreatePlant(new Vector2Int(i, j), PuzzleCell.PlantType.C, new PuzzleCell.PlantType[] { PuzzleCell.PlantType.S });
                        break;
                    case '3'://Climax AV, P
                        CreatePlant(new Vector2Int(i, j), PuzzleCell.PlantType.C, new PuzzleCell.PlantType[] { PuzzleCell.PlantType.AV, PuzzleCell.PlantType.P });
                        break;
                    case '4'://Climax AV, S
                        CreatePlant(new Vector2Int(i, j), PuzzleCell.PlantType.C, new PuzzleCell.PlantType[] { PuzzleCell.PlantType.AV, PuzzleCell.PlantType.S });
                        break;
                    case '5'://Climax P, S
                        CreatePlant(new Vector2Int(i, j), PuzzleCell.PlantType.C, new PuzzleCell.PlantType[] { PuzzleCell.PlantType.S, PuzzleCell.PlantType.P });
                        break;
                    case '6'://Climax AV, P, S
                        CreatePlant(new Vector2Int(i, j), PuzzleCell.PlantType.C, new PuzzleCell.PlantType[] { PuzzleCell.PlantType.AV, PuzzleCell.PlantType.P, PuzzleCell.PlantType.S });
                        break;
                    case 'X':
                        cells[i][j].isObstacle = true;
                        cells[i][j].obstacleSprite = obstacleSprites[0];
                        map.CallInstantiate(PuzzleCell.PlantType.None, i, j, true);
                        break;
                }
            }
        }

        //Mapa teste
        /*for(int i = 0; i < gridSize; i++) {
            cells[i][0].isObstacle = true;
            cells[i][0].obstacleSprite = obstacleSprites[0];
            map.CallInstantiate(PuzzleCell.PlantType.None, i, 0, true);
        }

        for(int i = 0; i < gridSize; i++) {
            cells[gridSize - 1][i].isObstacle = true;
            cells[gridSize - 1][i].obstacleSprite = obstacleSprites[0];
            map.CallInstantiate(PuzzleCell.PlantType.None, gridSize - 1, i, true);
        }

        CreatePlant(new Vector2Int(0, 1), PuzzleCell.PlantType.C, new PuzzleCell.PlantType[] { PuzzleCell.PlantType.P });
        CreatePlant(new Vector2Int(6, 2), PuzzleCell.PlantType.C, new PuzzleCell.PlantType[] { PuzzleCell.PlantType.AV, PuzzleCell.PlantType.S });
        CreatePlant(new Vector2Int(5, 4), PuzzleCell.PlantType.C, new PuzzleCell.PlantType[] { PuzzleCell.PlantType.AV, PuzzleCell.PlantType.P });

        CreatePlant(new Vector2Int(1, 2), PuzzleCell.PlantType.P);
        CreatePlant(new Vector2Int(1, 5), PuzzleCell.PlantType.P);
        CreatePlant(new Vector2Int(5, 5), PuzzleCell.PlantType.P);

        CreatePlant(new Vector2Int(0, 7), PuzzleCell.PlantType.AV);
        CreatePlant(new Vector2Int(4, 2), PuzzleCell.PlantType.AV);
        CreatePlant(new Vector2Int(4, 3), PuzzleCell.PlantType.AV);
        CreatePlant(new Vector2Int(4, 4), PuzzleCell.PlantType.AV);

        CreatePlant(new Vector2Int(3, 7), PuzzleCell.PlantType.S);
        CreatePlant(new Vector2Int(6, 1), PuzzleCell.PlantType.S);*/
    }

    // Update is called once per frame
    void Update()
    {
        //Teste de plantar
        /*if(Input.GetMouseButton(0)) {
            Debug.Log(Input.mousePosition);

            Vector3Int pos = GetAreaOnGame(Input.mousePosition);

            Debug.Log(pos);

            if (pos.x >= 0 && pos.x < 8) {
                if (pos.y > -8 && pos.y <= 0) {
                    int x, y;
                    x = pos.x;
                    y = -pos.y;
                    Debug.Log(GetAreaOnGame(Input.mousePosition));

                    if (Input.GetKeyDown(KeyCode.Alpha1)) {
                        CreatePlant(new Vector2Int(x, y), PuzzleCell.PlantType.AV);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                        CreatePlant(new Vector2Int(x, y), PuzzleCell.PlantType.P);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                        CreatePlant(new Vector2Int(x, y), PuzzleCell.PlantType.S);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                        CreatePlant(new Vector2Int(x, y), PuzzleCell.PlantType.C);
                    }
                }
            }
        }*/
    }

    public void CreatePlant(Vector2Int c, PuzzleCell.PlantType t, PuzzleCell.PlantType[] p = null) {
        cells[c.x][c.y].plants = t;

        cells[c.x][c.y].showBroto = true;

        switch (t) {
            case PuzzleCell.PlantType.AV:
                cells[c.x][c.y].influenced = PuzzleCell.InfluenceType.AVOnly;
                break;
            case PuzzleCell.PlantType.P:
                cells[c.x][c.y].influenced = PuzzleCell.InfluenceType.PSingle;
                cells[c.x][c.y].turnsToGrow = 3;
                break;
            case PuzzleCell.PlantType.S:
                cells[c.x][c.y].influenced = PuzzleCell.InfluenceType.SSingle;
                cells[c.x][c.y].turnsToGrow = 5;
                break;
            case PuzzleCell.PlantType.C:
                cells[c.x][c.y] = new PuzzleCell(PuzzleCell.PlantType.C, PuzzleCell.InfluenceType.Double, p);
                break;
        }

        map.CallInstantiate(t, c.x, c.y);
    }

    public Vector3Int GetAreaOnGame(Vector3 mousepos) {
        return g.WorldToCell(Camera.main.ScreenToWorldPoint(mousepos));
    }

    public void ApplyGrowth(Vector2Int pos, PuzzleCell.PlantType p) {
        //Adubo verde
        if(p == PuzzleCell.PlantType.AV) {
            if (CheckNotBoundries(new Vector2Int(pos.x + 1, pos.y))) {
                if(cells[pos.x + 1][pos.y].isObstacle) {
                    return;
                }

                if(cells[pos.x + 1][pos.y].plants == PuzzleCell.PlantType.C) {
                    if(cells[pos.x + 1][pos.y].plantsToGrow.Contains(PuzzleCell.PlantType.AV) &&
                        !cells[pos.x + 1][pos.y].progressToWin.Contains(PuzzleCell.PlantType.AV)){
                        Debug.Log("AV added");
                        cells[pos.x + 1][pos.y].progressToWin.Add(PuzzleCell.PlantType.AV);
                    }
                }
                else if(cells[pos.x + 1][pos.y].plants != PuzzleCell.PlantType.None &&
                    cells[pos.x + 1][pos.y].plants != PuzzleCell.PlantType.C) {
                    cells[pos.x + 1][pos.y].plants = PuzzleCell.PlantType.AV;
                    cells[pos.x + 1][pos.y].influenced = PuzzleCell.InfluenceType.AVOnly;
                }
                else if (cells[pos.x + 1][pos.y].influenced == PuzzleCell.InfluenceType.None) {
                    cells[pos.x + 1][pos.y].influenced = PuzzleCell.InfluenceType.AVOnly;
                    if (!(cells[pos.x + 1][pos.y].plants == PuzzleCell.PlantType.AV)) {
                        cells[pos.x + 1][pos.y].plants = PuzzleCell.PlantType.AV;
                    }
                }
            }
        }

        //Primária
        if (p == PuzzleCell.PlantType.P) {
            for (int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {
                    if (CheckNotBoundries(new Vector2Int(pos.x + i, pos.y + j))){
                        if (cells[pos.x + i][pos.y + j].isObstacle) {
                            continue;
                        }

                        if (cells[pos.x + i][pos.y + j].plants == PuzzleCell.PlantType.C) {
                            if (cells[pos.x + i][pos.y + j].plantsToGrow.Contains(PuzzleCell.PlantType.P) &&
                                !cells[pos.x + i][pos.y + j].progressToWin.Contains(PuzzleCell.PlantType.P)) {
                                cells[pos.x + i][pos.y + j].progressToWin.Add(PuzzleCell.PlantType.P);
                                Debug.Log("P added");
                            }
                        }
                        else if (cells[pos.x + i][pos.y + j].influenced == PuzzleCell.InfluenceType.None) {
                            cells[pos.x + i][pos.y + j].influenced = PuzzleCell.InfluenceType.PSingle;
                        }
                    }
                }
            }
        }

        //Secundária
        if (p == PuzzleCell.PlantType.S) {
            bool triggerX = false;
            bool triggerY = false;
            int startX = 0;
            int startY = 0;
            int stopOnX = gridSize - 1;
            int stopOnY = gridSize - 1;

            int w = 0;

            for(int i = 0; i < pos.x; i++) {
                if (cells[i][pos.y].isObstacle) {
                    startX = Mathf.Clamp(i - 1, 0, gridSize - 1);
                }
            }

            for(int i = 0; i < pos.y; i++) {
                if (cells[pos.x][i].isObstacle) {
                    startY = Mathf.Clamp(i - 1, 0, gridSize - 1);
                }
            }

            for(int i = pos.x; i < gridSize; i++) {
                if (cells[i][pos.y].isObstacle) {
                    stopOnX = Mathf.Clamp(i - 1, pos.x + 1, gridSize - 1);
                    break;
                }
            }

            for (int i = pos.y; i < gridSize; i++) {
                if (cells[pos.x][i].isObstacle) {
                    stopOnY = Mathf.Clamp(i - 1, pos.y + 1, gridSize - 1);
                    break;
                }
            }
            /*for (int i = 0; i < gridSize; i++) {
                if (cells[i][w].isObstacle && i == pos.x) {
                    if (pos.x > i) {
                        startX = i;
                    }
                    else {
                        if (!triggerX) {
                            stopOnX = i;
                            triggerX = true;
                        }
                    }
                }

                for (int j = 0; j < gridSize; j++) {
                    if (i == pos.x || j == pos.y) {
                        if (CheckNotBoundries(new Vector2Int(i, j))) {
                            if (cells[i][j].isObstacle) {
                                if (pos.y > j) {
                                    startY = j;
                                }
                                else {
                                    if (!triggerY) {
                                        stopOnY = j;
                                        triggerY = true;
                                    }
                                }
                            }
                        }
                    }
                }
                w++;
            }*/

            Debug.Log("Start:" + startX + ", " + startY);
            Debug.Log("Stop:" + stopOnX + ", " + stopOnY);

            for (int i = startX; i <= stopOnX; i++) {
                for (int j = startY; j <= stopOnY; j++) {
                    Debug.Log("Iterating on S");
                    if (i == pos.x || j == pos.y) {
                        Debug.Log("Is same line or column");
                        if (CheckNotBoundries(new Vector2Int(i, j))) {
                            Debug.Log("S is on boundries");
                            if (cells[i][j].plants == PuzzleCell.PlantType.C) {
                                Debug.Log("Is C type");
                                //Teste
                                Debug.Log(i + ", " + j);

                                foreach(PuzzleCell.PlantType planta in cells[i][j].plantsToGrow) {
                                    switch (planta) {
                                        case PuzzleCell.PlantType.AV:
                                            Debug.Log("AV here");
                                            break;
                                        case PuzzleCell.PlantType.P:
                                            Debug.Log("P here");
                                            break;
                                        case PuzzleCell.PlantType.S:
                                            Debug.Log("S here");
                                            break;
                                        case PuzzleCell.PlantType.C:
                                            Debug.Log("C here");
                                            break;
                                    }
                                }

                                foreach (PuzzleCell.PlantType planta in cells[i][j].progressToWin) {
                                    switch (planta) {
                                        case PuzzleCell.PlantType.AV:
                                            Debug.Log("AV here 2");
                                            break;
                                        case PuzzleCell.PlantType.P:
                                            Debug.Log("P here 2");
                                            break;
                                        case PuzzleCell.PlantType.S:
                                            Debug.Log("S here 2");
                                            break;
                                        case PuzzleCell.PlantType.C:
                                            Debug.Log("C here 2");
                                            break;
                                    }
                                }
                                //fim do teste
                                if (cells[i][j].plantsToGrow.Contains(PuzzleCell.PlantType.S) &&
                                    !cells[i][j].progressToWin.Contains(PuzzleCell.PlantType.S)) {
                                    cells[i][j].progressToWin.Add(PuzzleCell.PlantType.S);
                                    Debug.Log("S added");
                                }
                            }
                            else if (cells[i][j].influenced == PuzzleCell.InfluenceType.None) {
                                cells[i][j].influenced = PuzzleCell.InfluenceType.SSingle;
                            }
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

    public bool CheckNotBoundries(Vector2Int pos) {
        if(pos.x < 0 || pos.y < 0 || pos.x >= cells.Count || pos.y >= cells[0].Count) {
            return false;
        }

        return true;
    }
}
