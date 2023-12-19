using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScene : MonoBehaviour
{
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

    //Graphic Field
    [SerializeField] private Button[] effectBtn = new Button[] { };
    [SerializeField] private Button[] particleBtn = new Button[] { };

    //Audio Field
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button[] bgmBtn = new Button[] { };
    [SerializeField] private Button[] sfxBtn = new Button[] { };

    //Language Field
    // Start is called before the first frame update
    void Start()
    {
        IndexTab = 0;
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

    //Manage Graphic Setting
    public void onChangeEffectSetting(bool state)
    {
        SettingValue.useEffect = state;

        int index = 0;
        int active = SettingValue.useEffect != true ? 0 : 1;
        foreach (var btn in effectBtn)
        {
            btn.interactable = index == active ? true : false;
            index++;
        }
    }

    public void onChangeParticleSetting(bool state)
    {
        SettingValue.useParticle = state;

        int index = 0;
        int active = SettingValue.useParticle != true ? 0 : 1;
        foreach (var btn in particleBtn)
        {
            btn.interactable = index == active ? true : false;
            index++;
        }
    }

    //Manage Audio Setting
    public void onVolumeUpdate()
    {
        SettingValue.volume = volumeSlider.value;
    }
    public void onChangeBGMSetting(bool state)
    {
        SettingValue.enableBGM = state;

        int index = 0;
        int active = SettingValue.enableBGM != true ? 0 : 1;
        foreach (var btn in bgmBtn)
        {
            btn.interactable = index == active ? true : false;
            index++;
        }
    }

    public void onChangeSFXSetting(bool state)
    {
        SettingValue.enableSFX = state;

        int index = 0;
        int active = SettingValue.enableSFX != true ? 0 : 1;
        foreach (var btn in sfxBtn)
        {
            btn.interactable = index == active ? true : false;
            index++;
        }
    }

    //Manage Language Setting
}