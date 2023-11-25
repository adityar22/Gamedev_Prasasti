using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils; // Add this line
using System.Collections.Generic;

public abstract class Portal<T> : MonoBehaviour where T : MonoBehaviour
{
    private T instance;

    public T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            return instance;
        }
    }
}
