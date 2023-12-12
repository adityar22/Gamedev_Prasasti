using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    // init question properties
    [System.Serializable]
    public class AnswerList
    {
        public string answer;
        public string[] options = new string[]{};
    }
    // init Story properties
    [System.Serializable]
    public class Dialog
    {
        public bool bgText;
        public int characterIndex;
        [TextArea]
        public string dialogText;
        public bool isQuestion;
        public AnswerList answers;
        public bool isTransition;
        public Sprite background;

        public bool hasSoundEffect;
        public AudioClip soundEffect;
    }
    [System.Serializable]
    public class SubChapter
    {
        public string subtitle;
        public List<Dialog> dialogList;
        public AudioClip bgm;
        public bool isBattlePhase;
        public Character[] indexEnemy = new Character[]{};
        public Character[] indexUnlockedCharacter = new Character[] { };
    }
    [System.Serializable]
    public class Chapter
    {
        public string title;
        public string kingdom;
        public List<SubChapter> subList;
        public Character[] indexEnemy = new Character[]{};
    }
    public List<Chapter> listChapter;
}
