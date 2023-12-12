using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Monument
{
    public static bool hasVisited;
    public int id;
    public string name;
    public double latitude;
    public double longitude;
    public Sprite image;
    [TextArea]
    public string description;
    public List<Character> listOfSpecialSpawn = new List<Character>();
}

public class ListOfMonument : MonoBehaviour
{
    public List<Monument> listOfMonument = new List<Monument>();
}