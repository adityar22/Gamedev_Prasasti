using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    System.Random random;
    EnemyList listEnemy;
    void Start()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        listEnemy = GetComponent<EnemyList>();
        random = new System.Random();
        int countEnemy = random.Next(1, 4);

        Debug.Log(countEnemy);
        for (int i = 0; i < countEnemy; i++)
        {
            ChoosedPlayer.choosedEnemy.Add(enemyGenerator(i));
        }
    }

    private Character enemyGenerator(int index)
    {
        Character charEnemy = new Character();

        double typeRate = random.NextDouble();
        
        if (typeRate < EnemyList.chanceDarkHero)
        {
            List <EnemyList.EnemyData> shuffle = listEnemy.listOfDarkHero;
            Shuffle(shuffle);
            foreach (var enemy in shuffle)
            {
                double enemyRate = random.NextDouble();
                if (enemy.chanceRate > enemyRate)
                {
                    charEnemy = enemy.charData;
                    Debug.Log(charEnemy.character.name);
                    return charEnemy;
                }
            }
        }
        else
        {
            List <EnemyList.EnemyData> shuffle = listEnemy.listOfEnemy;
            Shuffle(shuffle);
            foreach (var enemy in shuffle)
            {
                double enemyRate = random.NextDouble();
                if (enemy.chanceRate > enemyRate)
                {
                    charEnemy = enemy.charData;
                    Debug.Log(charEnemy.character.name);
                    return charEnemy;
                }
            }
        }
        return charEnemy;
    }

    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}