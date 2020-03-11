using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Dialogue
{
    [Tooltip("O tamanho de Text é o tanto de textos que haverão nesta branch do diálogo. Aumente size" +
        "para adicionar mais diálogos.")]
    [TextArea]
    public string[] text;
}
