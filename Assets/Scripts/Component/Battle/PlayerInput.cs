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
        // Handle player input for actions (basic attack, skill, item)
        // For simplicity, you can use Input.GetKey or Input.GetButtonDown
        if (Input.GetKeyDown(KeyCode.A)) { battleManager.PerformAction(BattleAction.ActionType.BasicAttack, ChoosedPlayer.activeChar, ChoosedPlayer.targetEnemy); }
        if (Input.GetKeyDown(KeyCode.S)) { battleManager.PerformAction(BattleAction.ActionType.Skill, ChoosedPlayer.activeChar, ChoosedPlayer.targetEnemy); }
        /*        if (Input.GetKeyDown(KeyCode.W)) { battleManager.PerformAction(BattleAction.ActionType.Item, ChoosedPlayer.activeChar, ChoosedPlayer.targetEnemy); }*/
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

    private bool checkTargetExist(int index)
    {
        if(index < ChoosedPlayer.choosedEnemy.Count){
            if(ChoosedPlayer.choosedEnemy[index].character.stat.HP > 0){
                return true;
            }
            else{
                return false;
            }
        }
        else{
            return false;
        }
    }
    private Character getTarget(Character source)
    {
        int index;
        Debug.Log(source.character.target);

        switch (source.character.target)
        {
            case TargetAttack.Target.FrontLine:
                index = random.Next(0, 1);
                break;
            case TargetAttack.Target.BackLine:
                index = 2;
                break;
            case TargetAttack.Target.All:
                index = random.Next(0, 2);
                break;
            default:
                index = 0;
                break;
        }

        return checkTargetExist(index) ? ChoosedPlayer.choosedEnemy[index] : checkTargetExist(index != 0 ? 0 : random.Next(1, 2)) ? ChoosedPlayer.choosedEnemy[index != 0 ? 0 : random.Next(1, 2)]: getTarget(source);
    }

    private Character getSkillTarget(Character source)
    {
        Character target = new Character();

        switch (source.character.skill.skillTarget)
        {
            case TargetSkill.targetSkill.FrontLine:
                return target;
                break;
            case TargetSkill.targetSkill.BackLine:
                return target;
                break;
            case TargetSkill.targetSkill.Single:
                return target;
                break;
            case TargetSkill.targetSkill.All:
                return target;
                break;
            default:
                return target;
                break;
        }
    }
    public void clickAttack()
    {
        ChoosedPlayer.targetEnemy = getTarget(ChoosedPlayer.activeChar);
        battleManager.PerformAction(BattleAction.ActionType.BasicAttack, ChoosedPlayer.activeChar, ChoosedPlayer.targetEnemy);
    }

    public void clickSkill()
    {
        ChoosedPlayer.targetEnemy = getSkillTarget(ChoosedPlayer.activeChar);
        battleManager.PerformAction(BattleAction.ActionType.Skill, ChoosedPlayer.activeChar, ChoosedPlayer.targetEnemy);
    }
}
