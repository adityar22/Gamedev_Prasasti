using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class MonumentMap : MonoBehaviour
{
    [SerializeField] private GameObject monumentData;
    ListOfMonument listDataMonument;
    [SerializeField] private GameObject monumentIcon;

    [SerializeField] private AbstractMap map;


    void Start()
    {
        map = FindObjectOfType<AbstractMap>();

        GameObject instanceMonument = Instantiate(monumentData);
        listDataMonument = instanceMonument.GetComponent<ListOfMonument>();

        foreach (var monument in listDataMonument.listOfMonument)
        {
            InstantiateObjectAtCoordinates(monument, monument.latitude, monument.longitude);
        }
    }

    void InstantiateObjectAtCoordinates(Monument data, double latitude, double longitude)
    {
        Vector2d latLong = new Vector2d(latitude, longitude);
        Vector3 position = map.GeoToWorldPosition(latLong, false);

        GameObject monument = Instantiate(monumentIcon, position, Quaternion.identity);
        MonumentObj monumentObj = monument.GetComponent<MonumentObj>();
        Debug.Log(data.name);
        monumentObj.monument = data;
    }
}