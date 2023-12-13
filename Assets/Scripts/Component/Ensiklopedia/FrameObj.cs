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
    public void setPP()
    {
        Image imgPP = profile.GetComponent<Image>();
        imgPP.sprite = character.character.attribut.profile;
        txtName.text = character.character.name;
    }

}