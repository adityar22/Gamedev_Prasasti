using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharEnsiklopedia : MonoBehaviour
{
    private Character _indexChar;
    public Character indexChar
    {
        get { return _indexChar; }
        set
        {
            _indexChar = value;
            selectChar();
        }
    }
    [SerializeField] GameObject charData;
    [SerializeField] GameObject charFrame;
    [SerializeField] GameObject contentPanel;


    private int stateMenu = 0;
    [SerializeField] private GameObject menuPanel;

    private int indexTab = 0;
    private int IndexTab
    {
        get { return indexTab; }
        set
        {
            indexTab = value;
            changeOption();
        }
    }

    [SerializeField] private GameObject[] btnOption = new GameObject[] { };

    [SerializeField] private GameObject[] panelOption = new GameObject[] { };

    [SerializeField] private Image imgChar;
    [SerializeField] private TextMeshProUGUI txtDescMain;
    [SerializeField] private TextMeshProUGUI txtDescSecondary;

    [SerializeField] public GameObject menuHistory;
    [SerializeField] private GameObject contentMenuHistory;
    public static string textHistory;
    [SerializeField] private TextMeshProUGUI txtHistrory;

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

        IndexTab = 0;
    }

    public void onSetUpDownMenu()
    {
        UIEffectsManager menuEM = menuPanel.GetComponent<UIEffectsManager>();
        menuEM.Run(stateMenu == 0 ? "setDown" : "setUp");
    }

    public void changeStateMenu(int state)
    {
        this.stateMenu = state;
    }

    public void onChangeOption(int index)
    {
        this.IndexTab = index;
    }

    private void changeOption()
    {
        for (int i = 0; i < btnOption.Length; i++)
        {
            Button theButton = btnOption[i].GetComponent<Button>();
            ColorBlock theColor = btnOption[i].GetComponent<Button>().colors;
            btnOption[i].GetComponent<Image>().color = i != indexTab ? new Color(0.7f, 0.7f, 0.7f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 1.0f);
            theColor.normalColor = i != indexTab ? new Color(0.7f, 0.7f, 0.7f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 1.0f);

            theButton.colors = theColor;

            bool isActived = i == indexTab ? true : false;
            panelOption[i].SetActive(isActived);
        }
    }

    private void selectChar()
    {
        Debug.Log(indexChar.character.name);

        imgChar.sprite = indexChar.character.attribut.idle;

        txtDescMain.text = indexChar.character.descriptionAndHistories.description;
        txtDescSecondary.text = indexChar.character.descriptionAndHistories.additionalDesc;
    }
}