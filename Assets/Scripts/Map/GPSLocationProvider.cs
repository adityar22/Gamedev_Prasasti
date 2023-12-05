/*========================================================================
 Copyright (c) 2019-2022 PTC Inc. All Rights Reserved.

 Vuforia is a trademark of PTC Inc., registered in the United States and other
 countries.
 =========================================================================*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Behavior and usage of this class is described in the public article
// "Area Targets with GPS External Position"

public class GPSLocationProvider : MonoBehaviour
{
    public class playerCoor{
        public static float latitude;
        public static float longitude;
    }
    public class portalCoor{
        public static float latitude;
        public static float longitude;
    }
    public bool Initialized { get; private set; }

    public float Latitude { get; private set; }
    public float Longitude { get; private set; }
    public float Altitude { get; private set; }

    [Header("General Settings")]
    [Tooltip("The desired accuracy in meters.")]
    public float Accuracy = 5f;

    [Tooltip("The update distance in meters.")]
    public float UpdateDistance = 5f;

    [Tooltip("Automatically start GPS service on awake")]
    public bool StartOnAwake = true;

    void Awake()
    {
        if (StartOnAwake)
        {
            StartCoroutine(StartLocationServiceAsync());
        }
    }

    void Update()
    {
        if (!Initialized)
            return;

        var lastLocationData = Input.location.lastData;
        float accuracy = lastLocationData.horizontalAccuracy;
        Latitude = lastLocationData.latitude;
        Longitude = lastLocationData.longitude;
        Altitude = lastLocationData.altitude;

        Debug.Log($@"Location:
                 {System.Environment.NewLine} lat: {Latitude}
                 {System.Environment.NewLine} lon: {Longitude}
                 {System.Environment.NewLine} alt: {Altitude}
                 {System.Environment.NewLine} accuracy: {accuracy.ToString("0.0")}");
    }

    public void StartLocationService()
    {
        StartCoroutine(StartLocationServiceAsync());
    }

    IEnumerator StartLocationServiceAsync()
    {
        Debug.Log("Trying to access location...");

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location access not enabled by user.");
            yield break;
        }

        // Start service before querying location
        Input.location.Start(Accuracy, UpdateDistance);

        // Wait until service initializes
        const int maxTries = 10;
        int tryCount = 0;
        while (Input.location.status == LocationServiceStatus.Initializing &&
                tryCount < maxTries)
        {
            yield return new WaitForSeconds(1);
            tryCount++;
        }

        // Service didn't initialize
        if (tryCount >= maxTries)
        {
            Debug.Log("Location service initialization timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }

        // Access granted and location value could be retrieved
        if (Input.location.status == LocationServiceStatus.Running)
        {
            Initialized = true;

            var lastLocationData = Input.location.lastData;
            Debug.Log($@"Location:
                     {System.Environment.NewLine} lat: {lastLocationData.latitude}
                     {System.Environment.NewLine} lon: {lastLocationData.longitude}
                     {System.Environment.NewLine} alt: {lastLocationData.altitude}
                     {System.Environment.NewLine} accuracy: {lastLocationData.horizontalAccuracy}
                     {System.Environment.NewLine} time: {lastLocationData.timestamp}");
        }
    }
}
