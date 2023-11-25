using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionList : MonoBehaviour
{
    public enum Answer { Iya, Tidak }
    public enum Difficulty { Easy, Normal, Hard}

    [System.Serializable]
    public class Question
    {
        public int ID;
        [TextArea]
        public string question;
        public Answer answer;
        public Difficulty difficulty;

        public bool isInit = true;
    }

    public List<Question> questionList;
}
