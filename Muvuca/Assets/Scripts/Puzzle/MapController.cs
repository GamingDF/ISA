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

    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<PuzzleArea>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drawMap) {
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
                    Instantiate(cPrefab, mapHolder.position + new Vector3(i, j, 0), Quaternion.identity, mapHolder);
                }
                else if (area.cells[i][j].influenced == PuzzleCell.InfluenceType.SSingle) {
                    Instantiate(sPrefab, mapHolder.position + new Vector3(i, j, 0), Quaternion.identity, mapHolder);
                }
                else if (area.cells[i][j].influenced == PuzzleCell.InfluenceType.PSingle) {
                    Instantiate(pPrefab, mapHolder.position + new Vector3(i, j, 0), Quaternion.identity, mapHolder);
                }
                else if (area.cells[i][j].influenced == PuzzleCell.InfluenceType.AVOnly) {
                    Instantiate(avPrefab, mapHolder.position + new Vector3(i, j, 0), Quaternion.identity, mapHolder);
                }
            }
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
