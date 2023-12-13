using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerChapter : MonoBehaviour
{
    public static bool isInit;
    public static int lastChapter;

    void Start()
    {

    }

    public void LoadChapter()
    {
        string initPrefs = PlayerPrefs.GetString("prefsChapterState", "");
        isInit = initPrefs != "hasInit" ? true : false;
    }

    public static void SaveChapter(int chapter)
    {
        PlayerPrefs.SetString("SavedChapter", chapter.ToString());
        PlayerPrefs.SetString("prefsChapterState", "hasInit");
    }

}