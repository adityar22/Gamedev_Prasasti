using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int indexCharacter;
    public int level;

    Character enemyData;
    // Start is called before the first frame update
    void Start()
    {
        GameObject charData = GameObject.Find("charData");
        CharData data = charData.GetComponent<CharData>();

        enemyData = data.charData[indexCharacter];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onAttack()
    {

    }

    void onGetAttack()
    {

    }

    void onDeath()
    {

    }
}
