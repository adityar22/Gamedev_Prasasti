// using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    private BattleManager battleManager;
    private static Battle battle;

    public static int choosedArea;

    private void Start()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        battleManager = eventSystem.GetComponent<BattleManager>();
        battle = eventSystem.GetComponent<Battle>();
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

    public void clickAttack()
    {
        Debug.Log("clicked basic attack");
        ChoosedPlayer.targetEnemy = ChoosedPlayer.choosedEnemy[0];
        battleManager.PerformAction(BattleAction.ActionType.BasicAttack, ChoosedPlayer.activeChar, ChoosedPlayer.targetEnemy);
    }

    public void clickSkill()
    {
        battleManager.PerformAction(BattleAction.ActionType.Skill, ChoosedPlayer.activeChar, ChoosedPlayer.targetEnemy);
    }
}
