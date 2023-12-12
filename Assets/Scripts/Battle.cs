using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// CHOOSE PHASE CODE
public class ChoosedPlayer
{
    public static List<Fighter> choosedChar = new List<Fighter>();
    public static List<Fighter> choosedEnemy = new List<Fighter>();

    public static int activeChar;
    public static int ActiveChar
    {
        get { return activeChar; }
        set
        {
            activeChar = value;
            Battle.setTurnText(activeChar);
        }
    }
    public static int targetEnemy;
    public static int[] targetChar;

    public static int totalPlayer;
    public static int totalEnemy;
}

public class Battle : MonoBehaviour
{
    public static bool isTutorial;
    public static bool isAdventure;
    public static int activeChapter;
    public static int activeSubChapter;
    public static int statePhase;
    [SerializeField] private GameObject battlePhase;
    [SerializeField] private GameObject choosePhase;
    [SerializeField] private GameObject resultPhase;

    [SerializeField] public GameObject[] player = new GameObject[] { };
    [SerializeField] public GameObject[] enemy = new GameObject[] { };

    [SerializeField] public GameObject[] playerInfo = new GameObject[] { };
    [SerializeField] public GameObject[] enemyInfo = new GameObject[] { };

    [SerializeField] private GameObject[] playerHP = new GameObject[] { };
    [SerializeField] private GameObject[] enemyHP = new GameObject[] { };

    [SerializeField] private GameObject[] playerEnergy = new GameObject[] { };
    [SerializeField] private GameObject[] enemyEnergy = new GameObject[] { };

    [SerializeField] public GameObject _charData;
    [SerializeField] public GameObject _playerData;

    [SerializeField] private Sprite placeHolderChar;
    [SerializeField] private Sprite placeHolderDefeated;

    [SerializeField] public Button[] btnAction = new Button[] { };

    public List<ChooseItem> chooseItemList;
    public GameObject chooseBox;
    public Transform choosePanel;

    public int teamCount;
    public static int choosed;
    [SerializeField] private GameObject btnStart;

    BattleManager battleManager;

    public GameObject labelDamage;

    public static TextMeshProUGUI txtTurn;
    public static TextMeshProUGUI txtNextTurn;
    public static TextMeshProUGUI txtComment;
    public static TextMeshProUGUI txtDetailComment;

    [SerializeField] private TextMeshProUGUI txtResultTitle;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject textRewardPanel;
    [SerializeField] private GameObject textExpReward;

    public PlayerCharacter playerCharacter;

    void Start()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        battleManager = eventSystem.GetComponent<BattleManager>();
        // code for debug
        GameObject charData = Instantiate(_charData);
        CharData dataChar = charData.GetComponent<CharData>();

        GameObject playerData = Instantiate(_playerData);
        playerCharacter = playerData.GetComponent<PlayerCharacter>();
        playerCharacter.LoadCharacter();

        for (int i = 0; i < teamCount; i++)
        {
            Fighter placeHolder = new Fighter();
            placeHolder.character = new CharModel();
            ChoosedPlayer.choosedChar.Add(placeHolder);
        }

        initChoose();
    }

    void Update()
    {
        if (choosed != 0)
        {
            btnStart.SetActive(true);
        }
        else
        {
            btnStart.SetActive(false);
        }

        if (choosePhase.activeSelf)
        {
            for (int i = 0; i < ChoosedPlayer.choosedEnemy.Count; i++)
            {
                Image imageComponent = this.enemy[i].GetComponent<Image>();
                Sprite yourSprite = ChoosedPlayer.choosedEnemy[i].character.attribut.idle;

                this.enemyInfo[i].SetActive(true);

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
    }

    public void initChoose()
    {
        resultPhase.SetActive(false);
        battlePhase.SetActive(false);
        choosePhase.SetActive(true);

        foreach (var character in playerCharacter.unlockedCharacters)
        {
            GameObject chooseInstantiated = Instantiate(chooseBox, choosePanel);
            ChooseItem instantiate = chooseInstantiated.GetComponent<ChooseItem>();
            instantiate.indexChar = playerCharacter.unlockedCharacters.IndexOf(character);
            instantiate.character = character;
        }
    }

    public void initBattle()
    {
        battlePhase.SetActive(true);
        choosePhase.SetActive(false);

        GameObject txtTurnObj = GameObject.Find("txtTurn");
        Battle.txtTurn = txtTurnObj.GetComponent<TextMeshProUGUI>();

        GameObject txtNextTurnObj = GameObject.Find("txtNextTurn");
        Battle.txtNextTurn = txtNextTurnObj.GetComponent<TextMeshProUGUI>();

        GameObject txtCommentObj = GameObject.Find("txtComment");
        Battle.txtComment = txtCommentObj.GetComponent<TextMeshProUGUI>();

        GameObject txtDetailCommentObj = GameObject.Find("txtDetailComment");
        Battle.txtDetailComment = txtDetailCommentObj.GetComponent<TextMeshProUGUI>();

        Battle.statePhase = 1;
        battleManager.StartBattle();
    }

    private double calculateGainExp(Fighter player)
    {
        double totalGainedExp = 0;
        foreach (var enemy in ChoosedPlayer.choosedEnemy)
        {
            double gainedExp = (enemy.character.stat.BaseExp * enemy.character.stat.Level / 5.0) * (1.0 / ChoosedPlayer.choosedChar.Count) * (Math.Pow(((2 * enemy.character.stat.Level + 10) / (enemy.character.stat.Level + player.character.stat.Level + 10)), 3) + 2);
            Debug.Log(player.character.name + " get " + gainedExp + " exp from defeated " + enemy.character.name);
            totalGainedExp += gainedExp;
        }

        GameObject textRewardObj = Instantiate(textExpReward, textRewardPanel.transform);
        TextMeshProUGUI txtReward = textRewardObj.GetComponent<TextMeshProUGUI>();
        txtReward.text = "+" + totalGainedExp.ToString("0.00") + " EXP";
        return totalGainedExp;
    }
    private void setExpReward()
    {
        double totalExpReward = 0;
        foreach (var player in ChoosedPlayer.choosedChar)
        {
            if (player.character.name != null)
            {
                GameObject chooseInstantiated = Instantiate(chooseBox, resultPanel.transform);
                ChooseItem instantiate = chooseInstantiated.GetComponent<ChooseItem>();
                instantiate.indexChar = ChoosedPlayer.choosedChar.IndexOf(player);
                instantiate.character = player.character;

                player.character.stat.SelfExp += calculateGainExp(player);
                Debug.Log(player.character.name + " level: " + player.character.stat.Level);
                playerCharacter.SaveCharacter(player.character);
            }
        }
    }
    private void setResultTitle()
    {
        txtResultTitle.text = ChoosedPlayer.totalPlayer > ChoosedPlayer.totalEnemy ? "You Win!" : "You Lose!";
        if (ChoosedPlayer.totalPlayer > ChoosedPlayer.totalEnemy)
        {
            setExpReward();
        }
        ChoosedPlayer.choosedEnemy = new List<Fighter>();
        ChoosedPlayer.choosedChar = new List<Fighter>();
        ChoosedPlayer.totalEnemy = 0;
        ChoosedPlayer.totalPlayer = 0;
        PlayerInput.choosedArea = 0;
    }
    private void checkTotal()
    {
        Debug.Log("total player - enemy :" + ChoosedPlayer.totalPlayer + " " + ChoosedPlayer.totalEnemy);
        if (ChoosedPlayer.totalPlayer <= 0)
        {
            battlePhase.SetActive(false);
            resultPhase.SetActive(true);
            setResultTitle();
        }
        else if (ChoosedPlayer.totalEnemy <= 0)
        {
            battlePhase.SetActive(false);
            resultPhase.SetActive(true);
            setResultTitle();
        }
    }

    public void updateHPBar(int index, int indexPosition, Teams.team team, double damage)
    {
        if (team == Teams.team.Player)
        {
            Vector3 currentScale = this.playerHP[indexPosition].transform.localScale;
            float currentHP = (float)(BattleManager.heroInBattle[index].HP / BattleManager.heroInBattle[index].character.stat.HP);

            this.playerHP[indexPosition].transform.localScale = new Vector3(currentHP, currentScale.y, currentScale.z);

            GameObject labelDamageInstantiate = Instantiate(labelDamage, player[indexPosition].transform);
            LabelDamage damageTextObj = labelDamageInstantiate.GetComponent<LabelDamage>();
            damageTextObj.txtDamage.text = "-" + damage.ToString("0.00") + " HP";

            if (currentHP <= 0.0)
            {
                playerInfo[indexPosition].SetActive(false);

                Image imageComponent = this.player[indexPosition].GetComponent<Image>();
                Sprite yourSprite = BattleManager.heroInBattle[index].character.attribut.deathPose;
                imageComponent.sprite = yourSprite ? yourSprite : placeHolderDefeated;

                float aspectRatio = yourSprite ? yourSprite.rect.width / yourSprite.rect.height : placeHolderDefeated.rect.width / placeHolderDefeated.rect.height;
                imageComponent.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imageComponent.rectTransform.rect.width / aspectRatio);

                ChoosedPlayer.totalPlayer -= 1;
                checkTotal();
            }
        }
        else
        {
            Vector3 currentScale = this.enemyHP[indexPosition].transform.localScale;
            float currentHP = (float)(BattleManager.heroInBattle[index].HP / BattleManager.heroInBattle[index].character.stat.HP);

            this.enemyHP[indexPosition].transform.localScale = new Vector3(currentHP, currentScale.y, currentScale.z);

            GameObject labelDamageInstantiate = Instantiate(labelDamage, enemy[indexPosition].transform);
            labelDamageInstantiate.transform.Rotate(0, 180, 0);
            LabelDamage damageTextObj = labelDamageInstantiate.GetComponent<LabelDamage>();
            damageTextObj.txtDamage.text = "-" + damage.ToString("0.00") + " HP";

            if (currentHP <= 0.0)
            {
                enemyInfo[indexPosition].SetActive(false);

                Image imageComponent = this.enemy[indexPosition].GetComponent<Image>();
                Sprite yourSprite = BattleManager.heroInBattle[index].character.attribut.deathPose;
                imageComponent.sprite = yourSprite ? yourSprite : placeHolderDefeated;

                float aspectRatio = yourSprite ? yourSprite.rect.width / yourSprite.rect.height : placeHolderDefeated.rect.width / placeHolderDefeated.rect.height;
                imageComponent.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imageComponent.rectTransform.rect.width / aspectRatio);

                ChoosedPlayer.totalEnemy -= 1;
                checkTotal();
            }
        }
    }

    public void updateEnergyBar(int index, int indexPosition, Teams.team team)
    {
        if (team == Teams.team.Player)
        {
            Vector3 currentScale = this.playerEnergy[indexPosition].transform.localScale;
            float currentEnergy = (float)(BattleManager.heroInBattle[index].Energy / BattleManager.heroInBattle[index].character.stat.Energy);

            this.playerEnergy[indexPosition].transform.localScale = new Vector3(currentEnergy, currentScale.y, currentScale.z);
        }
        else
        {
            Vector3 currentScale = this.enemyEnergy[indexPosition].transform.localScale;
            float currentEnergy = (float)(BattleManager.heroInBattle[index].Energy / BattleManager.heroInBattle[index].character.stat.Energy);

            this.enemyEnergy[indexPosition].transform.localScale = new Vector3(currentEnergy, currentScale.y, currentScale.z);
        }
    }

    public void setBattlePose(int index, int indexPosition, Teams.team team)
    {

    }

    public static void setTurnText(int activeChar)
    {
        Battle.txtTurn.text = BattleManager.heroInBattle[activeChar].charTeam == Teams.team.Player ? BattleManager.heroInBattle[activeChar].character.name : "Foe's " + BattleManager.heroInBattle[activeChar].character.name;
        Battle.txtNextTurn.text = BattleManager.heroInBattle[activeChar == BattleManager.heroInBattle.Count - 1 ? 0 : activeChar + 1].charTeam == Teams.team.Player ? BattleManager.heroInBattle[activeChar == BattleManager.heroInBattle.Count - 1 ? 0 : activeChar + 1].character.name : "Foe's " + BattleManager.heroInBattle[activeChar == BattleManager.heroInBattle.Count - 1 ? 0 : activeChar + 1].character.name;
    }

    public static void setCommentText(string comment)
    {
        Battle.txtComment.text = comment;
    }

    public static void setDetailCommentText(string comment)
    {
        Battle.txtDetailComment.text = comment;
    }

    public void onClickConfirm()
    {
        if (isAdventure)
        {
            SceneManager.LoadScene("Adventure");
        }
        else
        {
            SceneManager.LoadScene("Story");
        }
    }
}