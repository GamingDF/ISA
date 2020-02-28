using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public bool drawMap;
    public Transform mapHolder;

    [SerializeField]
    private GameObject avPrefab;
    [SerializeField]
    private GameObject pPrefab;
    [SerializeField]
    private GameObject sPrefab;
    [SerializeField]
    private GameObject cPrefab;
    [SerializeField]
    private GameObject obstaclePrefab;
    [SerializeField]
    private GameObject brotoPrefab;

    private PuzzleArea area;
    private Grid g;

    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<PuzzleArea>();
        g = GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drawMap && !GetComponent<TurnController>().interturns) {
            CleanMap();
            CreateMap();
            drawMap = false;
        }
    }

    private void CreateMap() {
        for (int i = 0; i < area.cells.Count; i++) {
            for (int j = 0; j < area.cells[0].Count; j++) {
                /*if (area.cells[i][j].plants.Contains(PuzzleCell.PlantType.C)) {
                    Instantiate(cPrefab, mapHolder.position + new Vector3(i, j, 0), Quaternion.identity, mapHolder);
                }
                else if (area.cells[i][j].plants.Contains(PuzzleCell.PlantType.S)) {
                    Instantiate(sPrefab, mapHolder.position + new Vector3(i, j, 0), Quaternion.identity, mapHolder);
                }
                else if (area.cells[i][j].plants.Contains(PuzzleCell.PlantType.P)) {
                    Instantiate(pPrefab, mapHolder.position + new Vector3(i, j, 0), Quaternion.identity, mapHolder);
                }
                else if (area.cells[i][j].plants.Contains(PuzzleCell.PlantType.AV)) {
                    Instantiate(avPrefab, mapHolder.position + new Vector3(i, j, 0), Quaternion.identity, mapHolder);
                }*/
                if (area.cells[i][j].isObstacle) {
                    CallInstantiate(PuzzleCell.PlantType.None, i, j, true);
                }
                else if (area.cells[i][j].influenced == PuzzleCell.InfluenceType.Double) {
                    CallInstantiate(PuzzleCell.PlantType.C, i, j);
                }
                else if (area.cells[i][j].influenced == PuzzleCell.InfluenceType.SSingle) {
                    CallInstantiate(PuzzleCell.PlantType.S, i, j);
                }
                else if (area.cells[i][j].influenced == PuzzleCell.InfluenceType.PSingle) {
                    CallInstantiate(PuzzleCell.PlantType.P, i, j);
                }
                else if (area.cells[i][j].influenced == PuzzleCell.InfluenceType.AVOnly) {
                    CallInstantiate(PuzzleCell.PlantType.AV, i, j);
                }
            }
        }
    }
    public void CallInstantiate(PuzzleCell.PlantType p, int i, int j, bool isObstacle = false) {
        Vector3 pos = g.GetCellCenterWorld(g.WorldToCell(mapHolder.position + new Vector3(i, -j, 0)));

        if (isObstacle) {
            GameObject obj = Instantiate(obstaclePrefab, pos, Quaternion.identity, mapHolder);
            obj.GetComponent<ObstacleController>().SetSprite(area.cells[i][j].obstacleSprite);
            return;
        }

        GameObject cellObj = null;

        switch (p) {
            case PuzzleCell.PlantType.AV:
                cellObj = Instantiate(avPrefab, pos, Quaternion.identity, mapHolder);
                break;
            case PuzzleCell.PlantType.P:
                cellObj = Instantiate(pPrefab, pos, Quaternion.identity, mapHolder);
                break;
            case PuzzleCell.PlantType.S:
                cellObj = Instantiate(sPrefab, pos, Quaternion.identity, mapHolder);
                break;
            case PuzzleCell.PlantType.C:
                cellObj = Instantiate(cPrefab, pos, Quaternion.identity, mapHolder);
                break;
        }

        if (area.cells[i][j].showBroto) {
            GameObject broto = Instantiate(brotoPrefab, cellObj.transform);
            broto.transform.localPosition = new Vector3(0, 0, -1);
        }
    }

    private void CleanMap() {
        Queue<GameObject> q = new Queue<GameObject>();
        foreach(Transform t in mapHolder) {
            q.Enqueue(t.gameObject);
        }

        while(q.Count > 0) {
            Destroy(q.Dequeue());
        }
    }
}
