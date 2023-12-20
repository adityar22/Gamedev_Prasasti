using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrameObj : MonoBehaviour
{
    public Character character;
    [SerializeField] GameObject profile;
    [SerializeField] TextMeshProUGUI txtName;

    CharEnsiklopedia charEP;
    public void setPP()
    {
        Image imgPP = profile.GetComponent<Image>();
        imgPP.sprite = character.character.attribut.profile;
        txtName.text = character.character.name;

        GameObject es = GameObject.Find("EventSystem");
        charEP = es.GetComponent<CharEnsiklopedia>();
    }

    public void onClickChar()
    {
        charEP.indexChar = character;
    }
}