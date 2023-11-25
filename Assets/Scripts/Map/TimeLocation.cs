using UnityEngine;
using TMPro;
using Mapbox.Geocoding;
using Mapbox.Utils;
using System;
using Mapbox.Unity.Map;

public class TimeLocation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtTime;
    [SerializeField] private TextMeshProUGUI txtLocation;
    public AbstractMap map; // Reference to your Mapbox map component
    /*private ReverseGeocodeResource _reverseGeocodeResource;
    private GeocodingAPI _geocoder;*/

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateInfo", 0f, 60f); // Update every 60 seconds
    }

    void UpdateInfo()
    {
        // Get current system time
        DateTime currentTime = DateTime.Now;

        // Format time in hour-minute format
        string formattedTime = currentTime.ToString("HH:mm");

        // Set the text on the Text component
        txtTime.text = formattedTime;

        /*// Get current map center coordinates
        Vector2d currentMapCenter = map.CenterLatitudeLongitude;

        // Reverse geocode to get location name
        ReverseGeocode(currentMapCenter);*/
    }

    /*void ReverseGeocode(Vector2d coordinates)
    {
        _reverseGeocodeResource = new ReverseGeocodeResource(coordinates, map.Zoom);
        _geocoder = new GeocodingAPI(_reverseGeocodeResource);

        _geocoder.OnGeocodingResponse += OnGeocodingResponse;
        _geocoder.Geocode();
    }

    void OnGeocodingResponse(ForwardGeocodeResponse response)
    {
        if (response != null && response.Features.Count > 0)
        {
            // Get the location name
            string locationName = response.Features[0].PlaceName;

            // Set the text on the location Text component
            txtLocation.text = locationName;
        }
        else
        {
            // Handle case when no location name is found
            txtLocation.text = "Location not found";
        }
    }*/
}
