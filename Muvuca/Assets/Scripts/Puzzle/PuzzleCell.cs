using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCell
{
    public enum PlantType {
        P,
        S,
        C
    }

    public bool influenced;
    public List<PlantType> plants;

    public PuzzleCell() {
        plants = new List<PlantType>();
    }
}
