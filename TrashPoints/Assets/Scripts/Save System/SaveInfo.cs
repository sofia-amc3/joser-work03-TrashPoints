using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveInfo
{
    public int[] MaxScores { get; set; }

    [SerializeField] private int[] maxScores;
}
