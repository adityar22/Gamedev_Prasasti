using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyList : MonoBehaviour
{
    [System.Serializable]
    public class EnemyData{
        public Character charData;
        public double chanceRate;
    }

    public List<EnemyData> listOfEnemy = new List<EnemyData>();
    public static double chanceVillain = 0.7;
    public List<EnemyData> listOfDarkHero =  new List<EnemyData>();
    public static double chanceDarkHero = 0.3;
}