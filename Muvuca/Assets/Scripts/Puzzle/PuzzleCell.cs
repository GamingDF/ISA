using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCell
{
    public enum PlantType {
        None,
        AV,
        P,
        S,
        C
    }

    public enum InfluenceType {
        None = 0,
        AVOnly = 1,
        PSingle = 2,
        SSingle = 3,
        Double = 4
    }

    public InfluenceType influenced;
    public PlantType plants;
    public int turnsToGrow;

    public List<PlantType> plantsToGrow;
    public List<PlantType> progressToWin;

    public bool isObstacle;

    public PuzzleCell() {
        plants = PlantType.None;
    }

    public PuzzleCell(PlantType p, InfluenceType i, PlantType[] toGrow = null ) {
        influenced = i;
        plants = p;

        if(p == PlantType.C) {
            plantsToGrow = new List<PlantType>();
            progressToWin = new List<PlantType>();

            for(int c = 0; c < toGrow.Length; c++) {
                Debug.Log("Adicionou");
                plantsToGrow.Add(toGrow[c]);
            }
        }
    }
}
