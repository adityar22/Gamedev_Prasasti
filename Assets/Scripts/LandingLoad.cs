using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LandingLoad : MonoBehaviour
{
    [SerializeField] GameObject playerData;
    PlayerChapter playerChapter;
    void Start()
    {
        GameObject instanceLoad = Instantiate(playerData);
        playerChapter = instanceLoad.GetComponent<PlayerChapter>();
        playerChapter.LoadChapter();
    }
    public void onStartLanding()
    {
        if (PlayerChapter.isInit)
        {
            SceneManager.LoadScene("Story");
        }
        else
        {
            SceneManager.LoadScene("Adventure");
        }
    }
}