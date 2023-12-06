using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// BATTLE PHASE CODE
/// Kode berikut digunakan untuk mengidentifikasi action yang dapat dilakukan player
public class BattleAction
{
    public enum ActionType
    {
        BasicAttack,
        Skill,
        Item
        // Add more action types as needed
    }

    public ActionType Type;
    public Character Source;
    public Character Target;

    // Add additional properties depending on the action type (e.g., skill type, item type, etc.)

    public BattleAction(ActionType type, Character source, Character target)
    {
        Type = type;
        Source = source;
        Target = target;
    }
}

/// Kode berikut digunakan untuk manage turn order dan aksi yang dipilih oleh player
public class BattleManager : MonoBehaviour
{
    private List<Character> turnOrder;
    private bool hasAction;

    System.Random random;

    public void StartBattle()
    {
        random = new System.Random();
        InitializeBattle();
    }

    private void InitializeBattle()
    {
        // Combine both teams to determine turn order
        turnOrder = new List<Character>();
        // Initialize playerTeam and enemyTeam with characters
        foreach (var chars in ChoosedPlayer.choosedChar)
        {
            if (chars.character.name != null)
            {
                turnOrder.Add(chars);
            }
        }

        foreach (var chars in ChoosedPlayer.choosedEnemy)
        {
            if (chars)
            {
                turnOrder.Add(chars);
            }
        }

        // Sort turnOrder based on character speed (higher speed goes first)
        turnOrder.Sort((a, b) => b.character.stat.Spd.CompareTo(a.character.stat.Spd));

        // Start the first turn
        StartCoroutine(StartTurns());
    }

    private IEnumerator StartTurns()
    {
        foreach (var character in turnOrder)
        {
            // Check if the character is still alive
            if (character.character.stat.HP > 0)
            {
                hasAction = false;

                // Perform actions (BasicAttack, Skill, Item) based on player input or AI logic
                ChoosedPlayer.ActiveChar = character;
                Debug.Log("Now is " + ChoosedPlayer.activeChar.character.name + "'s turn");

                // Wait until hasAction becomes true
                while (!hasAction)
                {
                    yield return null;
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
        // Start the next round of turns
        StartCoroutine(StartTurns());
    }


    public void PerformAction(BattleAction.ActionType actionType, Character source, Character target)
    {
        switch (actionType)
        {
            case BattleAction.ActionType.BasicAttack:
                BasicAttack(source, target, false);
                Battle.setCommentText(source.character.name+" use basic attack to "+target.character.name);
                break;
            case BattleAction.ActionType.Skill:
                BasicAttack(source, target, true);
                Battle.setCommentText(source.character.name+" use "+source.character.skill.name+" to "+target.character.name);
                break;
            case BattleAction.ActionType.Item:
                // Implement item logic
                break;
        }
    }

    private double intervenceState(Character attacker)
    {
        switch (attacker.character.stat.Status)
        {
            case intervenceStatus.status.Burn:
                return 0.75;
                break;
            case intervenceStatus.status.Poisoned:
                return 0.80;
                break;
            default:
                return 1.0;
                break;
        }
    }

    private double criticalHitRatio(Character attacker)
    {
        switch (attacker.character.stat.criticalStage)
        {
            case 0:
                return 1.0 / 16;
                break;
            case 1:
                return 1.0 / 8;
                break;
            case 2:
                return 1.0 / 4;
                break;
            case 3:
                return 1.0 / 3;
                break;
            case 4:
                return 1.0 / 2;
                break;
            default:
                return 1.0;
                break;
        }
    }
    private double isCritical(Character attacker)
    {
        double randomValue = random.NextDouble();
        if (randomValue < criticalHitRatio(attacker)) { Debug.Log("It's a critical hit!"); }

        return randomValue < criticalHitRatio(attacker) ? 1.5 : 1.0;
    }

    private bool isLandingAttack(Character attacker, Character defender)
    {
        double landingPercentage = random.NextDouble();
        double intervence = attacker.character.stat.Status == intervenceStatus.status.Paralyze ? 0.25 : 0.0;
        double actualAcc = attacker.character.stat.Acc * (1 - defender.character.stat.Eva) * (1 - intervence);

        return landingPercentage <= actualAcc;
    }

    private void BasicAttack(Character attacker, Character target, bool isSkill)
    {
        if (isLandingAttack(attacker, target))
        {
            double interval = random.NextDouble() * (1.0 - 0.85) + 0.85;

            double basicDamage = (((2 * attacker.character.stat.level) / 5) + 2) * (isSkill ? attacker.character.skill.power * (attacker.character.stat.Atk / target.character.stat.Def) : (attacker.character.stat.Atk - (target.character.stat.Def * 0.2)));
            double typeEffective = ChartWeakness.ElementChart(attacker.character.element, target.character.element);
            double damage = (basicDamage / (isSkill ? 50 : 10) + 2) * typeEffective * isCritical(attacker) * intervenceState(attacker) * interval;
            if (isSkill)
            {
                // call skill effect here
            }
            ChoosedPlayer.targetEnemy.character.stat.HP -= damage;
            Debug.Log(attacker.character.name + "give "+ damage +" damages to "+target.character.name);
        }
        else
        {
            Debug.Log(attacker.character.name + "'s attack missed");
        }
        hasAction = true;
    }
}
