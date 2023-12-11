using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    // init Story properties
    [System.Serializable]
    public class Dialog
    {
        public int characterIndex;
        [TextArea]
        public string dialogText;
        public bool isQuestion;
        public bool isTransition;
        public Sprite background;
    }
    [System.Serializable]
    public class SubChapter
    {
        public string subtitle;
        public List<Dialog> dialogList;
        public AudioClip bgm;
    }
    [System.Serializable]
    public class Chapter
    {
        public string title;
        public string kingdom;
        public List<SubChapter> subList;
    }
    public List<Chapter> listChapter;
}
