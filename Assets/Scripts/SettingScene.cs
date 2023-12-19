using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScene : MonoBehaviour
{
    private int indexTab = 0;
    private int IndexTab
    {
        get{return indexTab;}
        set
        { 
            indexTab = value;
            changeOption();
        }
    }

    [SerializeField] private GameObject[] btnOption = new GameObject[]{};

    [SerializeField] private GameObject[] panelOption = new GameObject[]{};

    //Graphic Field

    //Audio Field
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button[] bgmBtn = new Button[]{};
    [SerializeField] private Button[] sfxBtn = new Button[]{};

    //Language Field
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < btnOption.Length; i++){
            Button theButton = btnOption[i].GetComponent<Button>();
            ColorBlock theColor = btnOption[i].GetComponent<Button>().colors;
            theColor.normalColor = i == indexTab ?  new Color(255, 255, 255) :  new Color(188, 188, 188);
            theColor.highlightedColor = new Color(255, 255, 255);
            theColor.pressedColor = new Color(188, 188, 188) ;
            theButton.colors = theColor;
            
            bool isActived = i == indexTab ? true : false;
            panelOption[i].SetActive(isActived);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onChangeOption(int index)
    {
        this.IndexTab = index;
    }

    private void changeOption()
    {
        for(int i = 0; i < btnOption.Length; i++){
            Button theButton = btnOption[i].GetComponent<Button>();
            ColorBlock theColor = btnOption[i].GetComponent<Button>().colors;
            theColor.normalColor = i == indexTab ?  new Color(255, 255, 255) :  new Color(188, 188, 188);
            theColor.highlightedColor = i == indexTab ?  new Color(255, 255, 255) :  new Color(188, 188, 188);
            theColor.pressedColor = new Color(188, 188, 188) ;
            theButton.colors = theColor;
            
            bool isActived = i == indexTab ? true : false;
            panelOption[i].SetActive(isActived);
        }
    }
}
