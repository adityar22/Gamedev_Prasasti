using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    private BattleManager battleManager;
    private static Battle battle;

    public static int choosedArea;
    System.Random random;

    private void Start()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        battleManager = eventSystem.GetComponent<BattleManager>();
        battle = eventSystem.GetComponent<Battle>();

        random = new System.Random();
    }

    private void Update()
    {

    }

    public static void clickTeam(int id, int teamIndex)
    {
        GameObject _charData = GameObject.Find("charData");
        CharData data = _charData.GetComponent<CharData>();

        if (teamIndex < ChoosedPlayer.choosedChar.Count)
        {
            ChoosedPlayer.choosedChar[teamIndex].character = PlayerCharacter.unlockedCharacters[id];

        }
        else
        {
            Character input = new Character();
            input.character = PlayerCharacter.unlockedCharacters[id];
            ChoosedPlayer.choosedChar.Add(input);
        }

        Image imageComponent = battle.player[teamIndex].GetComponent<Image>();
        Sprite yourSprite = ChoosedPlayer.choosedChar[teamIndex].character.attribut.idle;

        battle.playerInfo[teamIndex].SetActive(true);

        if (imageComponent != null)
        {
            // Set the sprite of the Image component
            imageComponent.sprite = yourSprite;

            // Calculate the aspect ratio of the sprite
            float aspectRatio = yourSprite.rect.width / yourSprite.rect.height;

            // Set the size of the Image component based on the sprite's aspect ratio
            imageComponent.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imageComponent.rectTransform.rect.width / aspectRatio);
        }
        else
        {
            Debug.LogError("Image component is not assigned.");
        }
        ChoosePlayer.teamIndex += ChoosePlayer.teamIndex != 2 ? 1 : 0;
    }

    private bool checkTargetExist(int index, Teams.team team)
    {
        int targetIndex = getTargetIndex(index, team);
        if (index < ChoosedPlayer.choosedEnemy.Count)
        {
            if (BattleManager.heroInBattle[targetIndex].HP > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    private int getAttackTarget(Fighter source)
    {
        int index;
        Debug.Log(source.character.target);

        switch (source.character.target)
        {
            case TargetAttack.Target.FrontLine:
                index = random.Next(0, 2);
                break;
            case TargetAttack.Target.BackLine:
                index = random.Next(1, 3);
                break;
            case TargetAttack.Target.All:
                index = random.Next(0, 3);
                break;
            default:
                index = 0;
                break;
        }

        return checkTargetExist(index, source.charTeam) ? index : checkTargetExist(index != 0 ? 0 : random.Next(1, 3), source.charTeam) ? index != 0 ? 0 : random.Next(1, 3) : getAttackTarget(source);
    }

    private void addTargetEnemy()
    {

    }
    private void getSkillTarget(Fighter source)
    {
        ChoosedPlayer.targetChar = new int[source.character.skill.skillTarget == TargetSkill.targetSkill.All ? source.character.target == TargetAttack.Target.All ? 3 : 2 : 1];

        for (int i = 0; i < ChoosedPlayer.targetChar.Length; i++)
        {
            switch (source.character.target)
            {
                case TargetAttack.Target.FrontLine:
                    ChoosedPlayer.targetChar[i] = getTargetIndex(i, source.charTeam);
                    break;
                case TargetAttack.Target.BackLine:
                    ChoosedPlayer.targetChar[i] = getTargetIndex(i == 0 ? i : 2, source.charTeam);
                    break;
                case TargetAttack.Target.All:
                    ChoosedPlayer.targetChar[i] = getTargetIndex(i, source.charTeam);
                    break;
                default:
                    break;
            }
        }
    }
    private int getTargetIndex(int index, Teams.team team)
    {
        int indexTarget = BattleManager.heroInBattle.FindIndex(a => a.charTeam != team && a.indexPosition == index);
        return indexTarget;
    }

    public void clickAttack()
    {
        int index = getAttackTarget(BattleManager.heroInBattle[ChoosedPlayer.activeChar]);
        ChoosedPlayer.targetEnemy = getTargetIndex(index, BattleManager.heroInBattle[ChoosedPlayer.activeChar].charTeam);
        Debug.Log(ChoosedPlayer.activeChar+" "+ChoosedPlayer.targetEnemy);
        battleManager.PerformAction(BattleAction.ActionType.BasicAttack, BattleManager.heroInBattle[ChoosedPlayer.activeChar], BattleManager.heroInBattle[ChoosedPlayer.targetEnemy]);
    }

    public void clickSkill()
    {
        getSkillTarget(BattleManager.heroInBattle[ChoosedPlayer.activeChar]);
        battleManager.PerformAction(BattleAction.ActionType.Skill, BattleManager.heroInBattle[ChoosedPlayer.activeChar], BattleManager.heroInBattle[ChoosedPlayer.targetEnemy]);
    }

    public void clickItem()
    {

    }
}