using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private BattleManager battleManager;

    private void Start()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        battleManager = eventSystem.GetComponent<BattleManager>();
    }

    private void Update()
    {
        // Handle player input for actions (basic attack, skill, item)
        // For simplicity, you can use Input.GetKey or Input.GetButtonDown
        if (Input.GetKeyDown(KeyCode.A)) { battleManager.PerformAction(BattleAction.ActionType.BasicAttack, ChoosedPlayer.activeChar, ChoosedPlayer.targetEnemy); }
        if (Input.GetKeyDown(KeyCode.S)) { battleManager.PerformAction(BattleAction.ActionType.Skill, ChoosedPlayer.activeChar, ChoosedPlayer.targetEnemy); }
/*        if (Input.GetKeyDown(KeyCode.W)) { battleManager.PerformAction(BattleAction.ActionType.Item, ChoosedPlayer.activeChar, ChoosedPlayer.targetEnemy); }*/
    }

    public void clickAttack()
    {
        Debug.Log("clicked basic attack");
        battleManager.PerformAction(BattleAction.ActionType.BasicAttack, ChoosedPlayer.activeChar, ChoosedPlayer.targetEnemy);
    }

    public void clickSkill()
    {
        battleManager.PerformAction(BattleAction.ActionType.Skill, ChoosedPlayer.activeChar, ChoosedPlayer.targetEnemy);
    }
}
