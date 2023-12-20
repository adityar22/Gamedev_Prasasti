using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DescAndHist
{
    [TextArea]
    public string description;
    [TextArea]
    public string additionalDesc;

    public int[] unlockedHistories = new int[]{};
    [SerializeField] public Story histories;
}