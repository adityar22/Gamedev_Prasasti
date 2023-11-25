using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Spawner : Portal<Spawner>
{
    [SerializeField] private GameObject[] portals;
    [SerializeField] private GameObject player;
    [SerializeField] private float waitTime;
    [SerializeField] private int startingPortal;
    [SerializeField] private float minRange;
    [SerializeField] private float maxRange;
    [SerializeField] private float spawnRate;

    private void Awake()
    {
        Assert.IsNotNull(portals);
        Assert.IsNotNull(player);
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i < Random.Range(1, startingPortal); i++)
        {
            InstantiatePortal();
        }
        StartCoroutine(GeneratePortals());
    }

    private IEnumerator GeneratePortals()
    {
        while (true)
        {
            if(Random.Range(0.0f, 1.0f) < spawnRate)
            {
                InstantiatePortal();
            }
            
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void InstantiatePortal()
    {
        int index = Random.Range(0, portals.Length);
        float x = player.transform.position.x + Generaterange();
        float z = player.transform.position.z + Generaterange();
        float y = player.transform.position.y;

        Instantiate(portals[index], new Vector3(x, y, z), Quaternion.identity);
    }

    private float Generaterange()
    {
        float randomNum = Random.Range(minRange, maxRange);
        bool isPositive = Random.Range(0, 10) < 5;
        return randomNum * (isPositive ? 1 : -1);
    }
}
