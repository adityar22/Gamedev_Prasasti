using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Answer: MonoBehaviour
{
    public string answer;
    [SerializeField] public TextMeshProUGUI txtAnswer;

    Dialog dialog;

    public void onClick(){
        GameObject eventSystem = GameObject.Find("EventSystem");
        dialog = eventSystem.GetComponent<Dialog>();

        dialog.onClickAnswer(answer);
    }
}