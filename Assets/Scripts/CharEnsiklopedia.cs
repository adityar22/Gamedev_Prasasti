using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharEnsiklopedia : MonoBehaviour
{
    [SerializeField] GameObject charData;
    [SerializeField] GameObject charFrame;
    [SerializeField] GameObject contentPanel;

    void Start()
    {
        GameObject charDataInstance = Instantiate(charData);
        CharData charDataClass = charDataInstance.GetComponent<CharData>();

        int index = 0;
        foreach (var chara in charDataClass.charData)
        {
            if (index != 0)
            {
                GameObject frameObjInstance = Instantiate(charFrame, contentPanel.transform);
                FrameObj framChar = frameObjInstance.GetComponent<FrameObj>();
                framChar.character = chara;
                framChar.setPP();
            }
            index++;
        }
    }
}