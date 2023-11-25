using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class LocationBased : MonoBehaviour
{
    public ARSessionOrigin arSessionOrigin;
    public AbstractMap mapboxMap;
    public Transform targetIcon;
    public float triggerDistance = 10f;

    void Update()
    {
        // Dapatkan koordinat GPS Anda dari Mapbox
        Vector2d myGPSLocation = mapboxMap.WorldToGeoPosition(arSessionOrigin.transform.position);

        // Lakukan deteksi objek di sekitar menggunakan AR Foundation
        // Misalnya, kita hanya mendeteksi objek jika ada objek dengan tag tertentu
        GameObject[] detectedObjects = GameObject.FindGameObjectsWithTag("DetectedObject");

        foreach (GameObject obj in detectedObjects)
        {
            // Dapatkan koordinat objek yang terdeteksi
            Vector2d objGPSLocation = mapboxMap.WorldToGeoPosition(obj.transform.position);

            // Hitung jarak antara posisi Anda dan objek yang terdeteksi
            float distance = (float)Vector2d.Distance(myGPSLocation, objGPSLocation);

            // Jika jarak kurang dari triggerDistance, tampilkan ikon
            if (distance < triggerDistance)
            {
                targetIcon.gameObject.SetActive(true);
                // Atur posisi ikon berdasarkan posisi objek yang terdeteksi
                targetIcon.position = obj.transform.position;
            }
            else
            {
                targetIcon.gameObject.SetActive(false);
            }
        }
    }
}
