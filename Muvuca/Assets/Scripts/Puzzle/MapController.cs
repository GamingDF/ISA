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
                if (area.cells[i][j].influenced == PuzzleCell.InfluenceType.Double) {
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
    public void CallInstantiate(PuzzleCell.PlantType p, int i, int j) {
        Vector3 pos = g.GetCellCenterWorld(g.WorldToCell(mapHolder.position + new Vector3(i, -j, 0)));
        switch (p) {
            case PuzzleCell.PlantType.AV:
                Instantiate(avPrefab, pos, Quaternion.identity, mapHolder);
                break;
            case PuzzleCell.PlantType.P:
                Instantiate(pPrefab, pos, Quaternion.identity, mapHolder);
                break;
            case PuzzleCell.PlantType.S:
                Instantiate(sPrefab, pos, Quaternion.identity, mapHolder);
                break;
            case PuzzleCell.PlantType.C:
                Instantiate(cPrefab, pos, Quaternion.identity, mapHolder);
                break;
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
