namespace Mapbox.Unity.Location
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class ObjPortal : MonoBehaviour
    {
        public void onClickPortal()
        {
            PlayerToPortal.playerPosition = new Vector3(0.0f, 0.0f, 0.0f);
            PlayerToPortal.portalPosition = transform.position;
            Debug.Log("Portal clicked at: " + transform.position);
            
            SceneManager.LoadScene("AR Camera");
        }
    }
}