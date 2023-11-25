using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Navigation : MonoBehaviour
{
    [SerializeField] private string SceneTarget;

    public void nextScene()
    {
        SceneManager.LoadScene(this.SceneTarget);
    }

    public void nextScene(int state)
    {
        SceneManager.LoadScene("Story");
    }

}
