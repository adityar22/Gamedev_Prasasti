using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestionRandomize : MonoBehaviour
{
    [SerializeField] private GameObject questionData;
    QuestionList question;
    [SerializeField] private TextMeshProUGUI txtQuestion;

    public static int index;

    System.Random random;
    void Start()
    {
        question = questionData.GetComponent<QuestionList>();
        random = new System.Random();

        initQuestion();
    }

    private void initQuestion()
    {
        Debug.Log(question == null);
        QuestionRandomize.index = random.Next(0, question.questionList.Count);

        txtQuestion.text = question.questionList[QuestionRandomize.index].question;
    }

    public void checkAnswer(string answer)
    {
        if (answer == question.questionList[QuestionRandomize.index].question)
        {
            Debug.Log("Correct answer");
        }
        SceneManager.LoadScene("Battle");
    }
}