using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCell : MonoBehaviour
{
    public enum PlantType {
        P,
        S,
        C
    }

    public bool influenced;
    public List<PlantType> plants;

    private void Start() {
        plants = new List<PlantType>();
    }
}
