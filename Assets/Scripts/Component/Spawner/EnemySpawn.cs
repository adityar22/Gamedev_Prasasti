using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public static Character[] setIndex = new Character[3];
    System.Random random;
    EnemyList listEnemy;
    void Start()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        listEnemy = GetComponent<EnemyList>();
        if (setIndex[0] == null)
        {
            random = new System.Random();
            int countEnemy = random.Next(1, 4);

            Debug.Log(countEnemy);
            for (int i = 0; i < countEnemy; i++)
            {
                ChoosedPlayer.choosedEnemy.Add(enemyGenerator(i));
            }
        }
        else
        {
            int i = 0;
            foreach (var chara in setIndex)
            {
                if (setIndex[i] != null)
                {
                    Fighter fighter = new Fighter();
                    fighter.character = chara.character;
                    ChoosedPlayer.choosedEnemy.Add(fighter);
                    i += 1;
                }
            }
        }
    }

    private Fighter enemyGenerator(int index)
    {
        Fighter charEnemy = new Fighter();

        double typeRate = random.NextDouble();

        if (typeRate < EnemyList.chanceDarkHero)
        {
            List<EnemyList.EnemyData> shuffle = listEnemy.listOfDarkHero;
            Shuffle(shuffle);
            foreach (var enemy in shuffle)
            {
                double enemyRate = random.NextDouble();
                if (enemy.chanceRate > enemyRate)
                {
                    charEnemy.character = enemy.charData.character;
                    Debug.Log(charEnemy.character.name);
                    return charEnemy;
                }
            }
        }
        else
        {
            List<EnemyList.EnemyData> shuffle = listEnemy.listOfEnemy;
            Shuffle(shuffle);
            foreach (var enemy in shuffle)
            {
                double enemyRate = random.NextDouble();
                if (enemy.chanceRate > enemyRate)
                {
                    charEnemy.character = enemy.charData.character;
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