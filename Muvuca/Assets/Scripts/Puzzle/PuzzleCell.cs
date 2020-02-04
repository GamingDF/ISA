using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCell
{
    public enum PlantType {
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
    public List<PlantType> plants;

    public PuzzleCell() {
        plants = new List<PlantType>();
    }
}
