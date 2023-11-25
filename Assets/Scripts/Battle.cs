using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// CHOOSE PHASE CODE
public class ChoosedPlayer
{
    public static List<Character> choosedChar = new List<Character>();
    public static List<Character> choosedEnemy = new List<Character>();

    public static Character activeChar;
    public static Character targetEnemy;
}

public class Battle : MonoBehaviour
{
    [SerializeField] private GameObject[] player = new GameObject[] { };
    [SerializeField] private GameObject[] enemy = new GameObject[] { };

    public void initBattle()
    {
        for(int i = 0; i < ChoosedPlayer.choosedChar.Count; i++)
        {
            Image imageComponent = this.player[i].GetComponent<Image>();
            imageComponent.sprite = ChoosedPlayer.choosedChar[i].attribut.idle;
        }
    }

    void Start()
    {
        // code for debug

    }
}