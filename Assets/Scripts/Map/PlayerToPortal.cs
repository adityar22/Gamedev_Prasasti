namespace Mapbox.Unity.Location
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerToPortal : MonoBehaviour
    {
        [SerializeField] Camera arCamera;
        [SerializeField] GameObject QuizCanvas;

        public static Vector3 portalPosition;
        public static Vector3 playerPosition;

        [SerializeField] private float distanceThreshold = 10f;
        [SerializeField] private float angleThreshold = 30f;

        void Start()
        {
            Debug.Log("Init Portal Location: " + portalPosition);
            Debug.Log("Init Player Location: " + playerPosition);

            QuizCanvas.SetActive(false);
        }
        void FixedUpdate()
        {
            // Vector3 acceleration = Input.acceleration;

            // // Hanya menggunakan komponen x dari akselerasi
            // float xAcceleration = acceleration.x;
            // float yAcceleration = acceleration.y;
            // float zAcceleration = acceleration.z;

            // // Normalisasi akselerasi x untuk mendapatkan arah
            // Vector2 normalizedAcceleration = new Vector2(xAcceleration, yAcceleration).normalized;

            float trueHeading = Input.compass.trueHeading;

            // Perbarui UserHeading
            float UserHeading = trueHeading;

            // Gunakan UserHeading sesuai kebutuhan Anda
            Debug.Log("User Heading: " + UserHeading);

            Vector3 directionToPlayer = playerPosition - portalPosition;

            // Perhitungan sudut antara arah kamera dan vektor ke portalPosition
            float angle = Vector3.Angle(arCamera.transform.forward, directionToPlayer);

            // Tampilkan data akselerasi dan sudut pada konsol
            Debug.Log("Sudut antara kamera dan portal: " + angle);

            // Periksa apakah sudut lebih kecil dari threshold
            if (UserHeading <= angle + angleThreshold && UserHeading >= angle - angleThreshold)
            {
                QuizCanvas.SetActive(true);
            }
            else
            {
                QuizCanvas.SetActive(false);
            }
        }
    }
}