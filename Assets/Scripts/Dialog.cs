using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Dialog : MonoBehaviour
{
    // init character data
    private CharData character;
    // init story data
    private Story story;

    // init story state
    private int activeChapter = -1;
    public int ActiveChapter
    {
        get { return activeChapter; }
        set
        {
            activeChapter = value;
            onChangeChapter();
        }
    }
    private int activeSub = -1;
    public int ActiveSub
    {
        get { return activeSub; }
        set
        {
            activeSub = value;
            onChangeSubChapter();
        }
    }
    private int activeDialog = -1;
    public int ActiveDialog
    {
        get { return activeDialog; }
        set
        {
            activeDialog = value;
            onChangeDialog();
        }
    }

    // init game component
    [SerializeField] GameObject dialogPanel;
    [SerializeField] TextMeshProUGUI textChar;
    [SerializeField] TextMeshProUGUI textDialog;
    [SerializeField] Image imgBg;
    [SerializeField] Image imgChar;

    [SerializeField] Button btnDialogControl;

    [SerializeField] AudioSource audio;

    // init question component
    [SerializeField] GameObject questionPanel;

    // Start is called before the first frame update
    void Start()
    {
        GameObject charData = GameObject.Find("charData");
        this.character = charData.GetComponent<CharData>();

        GameObject storyData = GameObject.Find("storyData");
        this.story = storyData.GetComponent<Story>();

        ActiveDialog += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onChangeDialog()
    {
        bool state = this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].isQuestion;
        questionPanel.SetActive(state);
        btnDialogControl.interactable = !state;

        int charId = this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].characterIndex;
        textChar.text = this.character.charData[charId].name;
        textDialog.text = this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].dialogText;
        imgChar.sprite = this.character.charData[charId].character.attribut.dialog;
    }

    void onChangeSubChapter()
    {
        ActiveDialog = 0;
        imgBg.sprite = this.story.listChapter[ActiveChapter].subList[ActiveSub].background;

        audio.Stop();
        audio.clip = this.story.listChapter[ActiveChapter].subList[ActiveSub].bgm;
        audio.Play();
    }

    void onChangeChapter()
    {

    }

    public void onClickDialogControl(int state)
    {
        ActiveDialog += ActiveDialog <= this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList.Count - 1 ? state : 0;
        if(ActiveDialog >= this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList.Count - 1)
        {
            ActiveSub += ActiveSub <= this.story.listChapter[ActiveChapter].subList.Count - 1 ? state : 0;
        }
    }
}
