using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// CHOOSE PHASE CODE
public class ChoosedPlayer
{
    public static List<Character> choosedChar = new List<Character>();
    public static List<Character> choosedEnemy = new List<Character>();

    public static Character activeChar;
    public static Character ActiveChar{
        get{return activeChar;}
        set{
            activeChar = value;
            Battle.setTurnText(activeChar);
        }
    }
    public static Character targetEnemy;
}

public class Battle : MonoBehaviour
{
    public static int statePhase;
    [SerializeField] private GameObject battlePhase;
    [SerializeField] private GameObject choosePhase;

    [SerializeField] public GameObject[] player = new GameObject[] { };
    [SerializeField] public GameObject[] enemy = new GameObject[] { };

    [SerializeField] public GameObject _charData;
    [SerializeField] public GameObject _playerData;

    [SerializeField] private Sprite placeHolderChar;

    public List<ChooseItem> chooseItemList;
    public GameObject chooseBox;
    public Transform choosePanel;

    public int teamCount;
    public static int choosed;
    [SerializeField] private GameObject btnStart;

    BattleManager battleManager;

    public static TextMeshProUGUI txtTurn;
    public static TextMeshProUGUI txtComment;

    void Start()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        battleManager = eventSystem.GetComponent<BattleManager>();
        // code for debug
        GameObject charData = Instantiate(_charData);
        CharData dataChar = charData.GetComponent<CharData>();

        GameObject playerData = Instantiate(_playerData);
        PlayerCharacter playerCharacter = playerData.GetComponent<PlayerCharacter>();

        for(int i = 0; i < teamCount; i++){
            Character placeHolder = new Character();
            placeHolder.character = new CharModel();
            ChoosedPlayer.choosedChar.Add(placeHolder);
        }

        initChoose();
    }

    void Update(){
        if(choosed!=0){
            btnStart.SetActive(true);
        }else{
            btnStart.SetActive(false);
        }

        for (int i = 0; i < ChoosedPlayer.choosedEnemy.Count; i++)
        {
            Image imageComponent = this.enemy[i].GetComponent<Image>();
            Sprite yourSprite = ChoosedPlayer.choosedEnemy[i].character.attribut.idle;


            if (imageComponent != null)
            {
                // Set the sprite of the Image component
                imageComponent.sprite = yourSprite ? yourSprite : placeHolderChar;

                // Calculate the aspect ratio of the sprite
                float aspectRatio = yourSprite ? yourSprite.rect.width / yourSprite.rect.height : placeHolderChar.rect.width / placeHolderChar.rect.height;

                // Set the size of the Image component based on the sprite's aspect ratio
                imageComponent.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imageComponent.rectTransform.rect.width / aspectRatio);
            }
        }
    }

    public void initChoose()
    {
        battlePhase.SetActive(false);
        choosePhase.SetActive(true);

        foreach(var character in PlayerCharacter.unlockedCharacters)
        {
            GameObject chooseInstantiated = Instantiate(chooseBox, choosePanel);
            ChooseItem instantiate = chooseInstantiated.GetComponent<ChooseItem>();
            instantiate.indexChar = PlayerCharacter.unlockedCharacters.IndexOf(character);
        }

        // foreach(var enemy in ChoosedPlayer.choosedEnemy){
        //     Image imageComponent = this.enemy[ChoosedPlayer.choosedEnemy.FindIndex(c => c == enemy)].GetComponent<Image>();
        //     Sprite yourSprite = enemy.character.attribut.idle;


        //     if (imageComponent != null)
        //     {
        //         // Set the sprite of the Image component
        //         imageComponent.sprite = yourSprite ? yourSprite : placeHolderChar;

        //         // Calculate the aspect ratio of the sprite
        //         float aspectRatio = yourSprite ? yourSprite.rect.width / yourSprite.rect.height : placeHolderChar.rect.width / placeHolderChar.rect.height;

        //         // Set the size of the Image component based on the sprite's aspect ratio
        //         imageComponent.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imageComponent.rectTransform.rect.width / aspectRatio);
        //     }
        // }
    }

    public void initBattle()
    {
        battlePhase.SetActive(true);
        choosePhase.SetActive(false);

        GameObject txtTurnObj = GameObject.Find("txtTurn");
        Battle.txtTurn = txtTurnObj.GetComponent<TextMeshProUGUI>();
        GameObject txtCommentObj = GameObject.Find("txtComment");
        Battle.txtComment = txtCommentObj.GetComponent<TextMeshProUGUI>();

        Battle.statePhase = 1;
        battleManager.StartBattle();
    }

    public static void setTurnText(Character activeChar){
        Battle.txtTurn.text = activeChar.character.name;
    }

    public static void setCommentText(string comment){
        Battle.txtComment.text = comment;
    }
}