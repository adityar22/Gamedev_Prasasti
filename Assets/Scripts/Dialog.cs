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
    [SerializeField] AudioSource sfxAudio;

    // init question component
    [SerializeField] GameObject questionPanel;
    [SerializeField] GameObject btnAnswer;
    GameObject[] answer;

    // init notification component
    [SerializeField] GameObject notifPanel;
    [SerializeField] TextMeshProUGUI txtNotif;

    // Start is called before the first frame update
    void Start()
    {
        GameObject charDataObj = Instantiate(charData);
        this.character = charDataObj.GetComponent<CharData>();

        GameObject storyDataObj = Instantiate(storyData);
        this.story = storyDataObj.GetComponent<Story>();

        ActiveChapter = hasBattle ? Battle.activeChapter : setChapter;
        ActiveSub += hasBattle ? Battle.activeSubChapter + 1 : 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ShowUnlockedCharacters()
    {
        foreach (var newChara in this.story.listChapter[ActiveChapter].subList[ActiveSub].indexUnlockedCharacter)
        {
            notifPanel.SetActive(true);
            txtNotif.text = "Unlocked new character: " + newChara.character.name;

            yield return new WaitForSeconds(0.5f);

            notifPanel.SetActive(false);
        }
    }
    void onChangeDialog()
    {
        bool isEnd = ActiveDialog == this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList.Count - 2;
        if (isEnd && this.story.listChapter[ActiveChapter].subList[ActiveSub].indexUnlockedCharacter.Length > 0)
        {
            StartCoroutine(ShowUnlockedCharacters());
        }


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

        imgChar.sprite = charId != -1 ? this.character.charData[charId].character.attribut.idle : null;

        if (this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].isTransition)
        {
            imgBg.sprite = this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].background;
        }

        if (this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].bgm)
        {
            audio.Stop();
            audio.clip = this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].bgm;
            audio.Play();
        }

        sfxAudio.Stop();
        if(this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].hasSoundEffect){
            sfxAudio.clip = this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList[ActiveDialog].soundEffect;
            sfxAudio.Play();
        }
    }

    void onChangeSubChapter()
    {
        if (!this.story.listChapter[ActiveChapter].subList[ActiveSub].isBattlePhase)
        {
            ActiveDialog = 0;
        }
        else if (!hasBattle)
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
        else
        {
            ActiveDialog = 0;
            hasBattle = false;
        }
    }

    void onChangeChapter()
    {

    }

    public void onClickDialogControl(int state)
    {
        int dialogState = ActiveDialog + state;
        if (dialogState < this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList.Count)
        {
            ActiveDialog += state;
        }
        else
        {
            int subState = ActiveSub + state;
            if (subState < this.story.listChapter[ActiveChapter].subList.Count)
            {
                ActiveSub += state;
            }
            else
            {
                int i = 0;
                foreach (var index in this.story.listChapter[ActiveChapter].indexEnemy)
                {
                    EnemySpawn.setIndex[i] = index;
                    i += 1;
                }
                Battle.isAdventure = false;
                Battle.activeChapter = ActiveChapter;
                Battle.activeSubChapter = ActiveSub;
                Battle.isBossChapter = true;
                hasBattle = true;
                SceneManager.LoadScene("Battle");
            }
        }
        // ActiveDialog += ActiveDialog <= this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList.Count - 1 ? state : 0;
        // if (ActiveDialog >= this.story.listChapter[ActiveChapter].subList[ActiveSub].dialogList.Count - 1)
        // {
        //     ActiveSub += ActiveSub <= this.story.listChapter[ActiveChapter].subList.Count - 1 ? state : 0;
        //     if (ActiveSub >= this.story.listChapter[ActiveChapter].subList.Count - 1)
        //     {

        //     }
        // }
    }

    public void onClickAnswer(string answer)
    {

    }
}
