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

    public PuzzleCell() {
        plants = PlantType.None;
    }

    public PuzzleCell(PlantType p, InfluenceType i) {
        influenced = i;
        plants = p;
    }
}
