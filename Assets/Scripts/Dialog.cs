using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Dialog : MonoBehaviour
{
    public static bool hasBattle = false;
    // init character data
    public GameObject charData;
    private CharData character;
    // init story data
    public GameObject storyData;
    private Story story;

    public static int setChapter = 0;

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
    [SerializeField] GameObject txtDialogPanel;
    [SerializeField] TextMeshProUGUI textChar;
    [SerializeField] TextMeshProUGUI textDialog;
    [SerializeField] GameObject txtBgPanel;
    [SerializeField] TextMeshProUGUI txtBg;
    [SerializeField] Image imgBg;
    [SerializeField] Image imgChar;

    [SerializeField] Button btnDialogControl;

    [SerializeField] AudioSource audio;

    // init question component
    [SerializeField] GameObject questionPanel;
    [SerializeField] GameObject btnAnswer;
    GameObject[] answer;

    // Start is called before the first frame update
    void Start()
    {
        GameObject charDataObj = Instantiate(charData);
        this.character = charDataObj.GetComponent<CharData>();

        GameObject storyDataObj = Instantiate(storyData);
        this.story = storyDataObj.GetComponent<Story>();

        ActiveChapter = hasBattle ? Battle.activeChapter : setChapter;
        ActiveSub += hasBattle ? Battle.activeSubChapter + 1 : 1;
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

        int answerCount = this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].answers.options.Length;
        answer = new GameObject[answerCount];
        for (int i = 0; i < answerCount; i++)
        {
            answer[i] = Instantiate(btnAnswer, questionPanel.transform);
            Answer ansClass = answer[i].GetComponent<Answer>();
            ansClass.answer = this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].answers.options[i];
        }

        int charId = this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].characterIndex;

        txtBgPanel.SetActive(this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].bgText);
        txtDialogPanel.SetActive(!this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].bgText);

        textChar.text = charId != -1 ? this.character.charData[charId].name : "";
        textDialog.text = this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].dialogText;
        txtBg.text = this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].dialogText;

        imgChar.sprite = charId != -1 ? this.character.charData[charId].character.attribut.dialog : null;

        if (this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].isTransition)
        {
            imgBg.sprite = this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].background;
        }
    }

    void onChangeSubChapter()
    {
        audio.Stop();
        if (this.story.listChapter[ActiveChapter].subList[ActiveSub].bgm)
        {
            audio.clip = this.story.listChapter[ActiveChapter].subList[ActiveSub].bgm;
            audio.Play();
        }

        if (!this.story.listChapter[ActiveChapter].subList[ActiveSub].isBattlePhase)
        {
            ActiveDialog = 0;
        }
        else
        {
            int i = 0;
            foreach (var index in this.story.listChapter[ActiveChapter].subList[ActiveSub].indexEnemy)
            {
                EnemySpawn.setIndex[i] = index;
                i += 1;
            }
            Battle.isAdventure = false;
            Battle.activeChapter = ActiveChapter;
            Battle.activeSubChapter = ActiveSub;
            hasBattle = true;
            SceneManager.LoadScene("Battle");
        }
    }

    void onChangeChapter()
    {

    }

    public void onClickDialogControl(int state)
    {
        ActiveDialog += ActiveDialog <= this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList.Count - 1 ? state : 0;
        if (ActiveDialog >= this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList.Count - 1)
        {
            ActiveSub += ActiveSub <= this.story.listChapter[ActiveChapter].subList.Count - 1 ? state : 0;
        }
    }

    public void onClickAnswer(string answer)
    {

    }
}
