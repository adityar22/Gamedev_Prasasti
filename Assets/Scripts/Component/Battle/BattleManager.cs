using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// BATTLE PHASE CODE
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
    public static List<Fighter> heroInBattle;
    private bool hasAction;
    private static Battle battle;
    private static PlayerInput pInput;

    public float chanceEnemyUseSkill;

    System.Random random;

    public void StartBattle()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        battle = eventSystem.GetComponent<Battle>();
        pInput = eventSystem.GetComponent<PlayerInput>();

        random = new System.Random();
        InitializeBattle();
    }

    private void InitializeBattle()
    {
        BattleManager.heroInBattle = new List<Fighter>();
        // Combine both teams to determine turn order
        int indexHero = 0;
        int indexPosition = 0;
        // Initialize playerTeam and enemyTeam with characters
        foreach (var chars in ChoosedPlayer.choosedChar)
        {
            if (chars.character.name != null)
            {
                Fighter index = new Fighter();
                BattleManager.heroInBattle.Add(index);
                BattleManager.heroInBattle[indexHero].charTeam = Teams.team.Player;
                BattleManager.heroInBattle[indexHero].indexPosition = indexPosition;
                BattleManager.heroInBattle[indexHero].character = chars.character;
                BattleManager.heroInBattle[indexHero].HP = chars.character.stat.HP;
                BattleManager.heroInBattle[indexHero].Energy = 0.0;
                indexHero += 1;
                indexPosition += 1;
                ChoosedPlayer.totalPlayer += 1;
            }
        }

        indexPosition = 0;
        foreach (var chars in ChoosedPlayer.choosedEnemy)
        {
            if (chars.character != null)
            {
                Fighter index = new Fighter();
                BattleManager.heroInBattle.Add(index);
                BattleManager.heroInBattle[indexHero].charTeam = Teams.team.Enemy;
                BattleManager.heroInBattle[indexHero].indexPosition = indexPosition;
                BattleManager.heroInBattle[indexHero].character = chars.character;
                BattleManager.heroInBattle[indexHero].HP = chars.character.stat.HP;
                BattleManager.heroInBattle[indexHero].Energy = 0.0;
                indexHero += 1;
                indexPosition += 1;
                ChoosedPlayer.totalEnemy += 1;
            }
        }

        // Sort turnOrder based on character speed (higher speed goes first)
        heroInBattle.Sort((a, b) => b.character.stat.Spd.CompareTo(a.character.stat.Spd));

        // Start the first turn
        StartCoroutine(StartTurns());
    }

    private IEnumerator StartTurns()
    {
        int index = 0;
        foreach (var character in heroInBattle)
        {
            // Check if the character is still alive
            if (character.HP > 0)
            {
                Battle.setDetailCommentText("");
                Battle.setCommentText("");
                hasAction = false;

                // Perform actions (BasicAttack, Skill, Item) based on player input or AI logic
                ChoosedPlayer.ActiveChar = index;

                battle.setIdlePose(character.indexPosition, character.charTeam);

                if (character.charTeam == Teams.team.Player)
                {
                    Debug.Log("Now is your " + heroInBattle[ChoosedPlayer.activeChar].character.name + "'s turn");
                    foreach (var btn in battle.btnAction)
                    {
                        btn.interactable = true;
                    }
                    if (character.Energy < character.character.stat.Energy)
                    {
                        battle.btnAction[2].interactable = false;
                    }
                }
                else
                {
                    Debug.Log("Now is foe's " + heroInBattle[ChoosedPlayer.activeChar].character.name + "'s turn");
                    foreach (var btn in battle.btnAction)
                    {
                        btn.interactable = false;
                    }
                    float useSkill = (float)random.NextDouble();

                    if (useSkill < chanceEnemyUseSkill)
                    {
                        if (character.Energy >= character.character.stat.Energy)
                        {
                            pInput.clickSkill();
                        }
                        else
                        {
                            pInput.clickAttack();
                        }
                    }
                    else
                    {
                        pInput.clickAttack();
                    }
                }

                // Wait until hasAction becomes true
                while (!hasAction)
                {
                    yield return null;
                }
            }
            index += 1;
            battle.stopIdlePose(character.indexPosition, character.charTeam);
            yield return new WaitForSeconds(0.8f);
        }
        // Start the next round of turns
        StartCoroutine(StartTurns());
    }


    public void PerformAction(BattleAction.ActionType actionType, Fighter source, Fighter target)
    {
        switch (actionType)
        {
            case BattleAction.ActionType.BasicAttack:
                BasicAttack(source, target);
                Battle.setCommentText(source.character.name + " use basic attack to " + target.character.name);
                break;
            case BattleAction.ActionType.Skill:
                SkillAttack(source);
                Battle.setCommentText(source.character.name + " use " + source.character.skill.name);
                break;
            case BattleAction.ActionType.Item:
                // Implement item logic
                break;
        }
    }

    private double intervenceState(Fighter attacker)
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

    private double criticalHitRatio(Fighter attacker)
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
    private double isCritical(Fighter attacker)
    {
        double randomValue = random.NextDouble();
        if (randomValue < criticalHitRatio(attacker))
        {
            Debug.Log("It's a critical hit!");
            Battle.setDetailCommentText(attacker.character.name + "'s It's a critical hit!");
        }

        return randomValue < criticalHitRatio(attacker) ? 1.5 : 1.0;
    }

    private bool isLandingAttack(Fighter attacker, Fighter defender)
    {
        double landingPercentage = random.NextDouble();
        double intervence = attacker.character.stat.Status == intervenceStatus.status.Paralyze ? 0.25 : 0.0;
        double actualAcc = attacker.character.stat.Acc * (1 - defender.character.stat.Eva) * (1 - intervence);

        return landingPercentage <= actualAcc;
    }

    private void BasicAttack(Fighter attacker, Fighter target)
    {
        if (isLandingAttack(attacker, target))
        {
            battle.setBattlePose(heroInBattle.FindIndex(a => a == attacker), attacker.indexPosition, attacker.charTeam);
            double interval = random.NextDouble() * (1.0 - 0.85) + 0.85;

            double basicDamage = (((2 * attacker.character.stat.level) / 5) + 2) * (attacker.character.stat.Atk - (target.character.stat.Def * 0.2));
            double typeEffective = ChartWeakness.ElementChart(attacker.character.element, target.character.element);
            double damage = ((basicDamage / 10) + 2) * typeEffective * isCritical(attacker) * intervenceState(attacker) * interval;

            heroInBattle[ChoosedPlayer.targetEnemy].HP -= damage;

            double addTargetEnergy = heroInBattle[ChoosedPlayer.targetEnemy].Energy < heroInBattle[ChoosedPlayer.targetEnemy].character.stat.Energy ? 0.5 * heroInBattle[ChoosedPlayer.targetEnemy].character.stat.Energy : 0.0;
            double addAttackerEnergy = heroInBattle[ChoosedPlayer.activeChar].Energy < heroInBattle[ChoosedPlayer.activeChar].character.stat.Energy ? 0.2 * heroInBattle[ChoosedPlayer.activeChar].character.stat.Energy : 0.0;
            heroInBattle[ChoosedPlayer.targetEnemy].Energy += heroInBattle[ChoosedPlayer.targetEnemy].Energy + addTargetEnergy <= heroInBattle[ChoosedPlayer.targetEnemy].character.stat.Energy ? addTargetEnergy : heroInBattle[ChoosedPlayer.targetEnemy].character.stat.Energy - heroInBattle[ChoosedPlayer.targetEnemy].Energy;
            heroInBattle[ChoosedPlayer.activeChar].Energy += heroInBattle[ChoosedPlayer.targetEnemy].Energy + addAttackerEnergy <= heroInBattle[ChoosedPlayer.activeChar].character.stat.Energy ? addAttackerEnergy : heroInBattle[ChoosedPlayer.activeChar].character.stat.Energy - heroInBattle[ChoosedPlayer.activeChar].Energy;
            battle.updateEnergyBar(heroInBattle.FindIndex(a => a == target), target.indexPosition, target.charTeam);
            battle.updateEnergyBar(heroInBattle.FindIndex(a => a == attacker), attacker.indexPosition, attacker.charTeam);

            Debug.Log(attacker.character.name + " give " + damage + " damages to " + target.character.name);

            battle.getAttackPose(target.indexPosition, target.charTeam);
            battle.updateHPBar(heroInBattle.FindIndex(a => a == target), target.indexPosition, target.charTeam, damage);

        }
        else
        {
            Debug.Log(attacker.character.name + "'s attack missed");
            Battle.setDetailCommentText(attacker.character.name + "'s attack missed");
        }
        hasAction = true;
    }

    private void SkillStatusIntervence(Fighter attacker, Fighter target)
    {
        double actualRate = random.NextDouble();
        if (actualRate < attacker.character.skill.effectStatusRate)
        {
            switch (attacker.character.skill.skillEffectToStatus)
            {
                case intervenceStatus.status.Burn:
                    target.character.stat.Status = intervenceStatus.status.Burn;
                    break;
                case intervenceStatus.status.Paralyze:
                    target.character.stat.Status = intervenceStatus.status.Paralyze;
                    break;
                case intervenceStatus.status.Poisoned:
                    target.character.stat.Status = intervenceStatus.status.Poisoned;
                    break;
                default:
                    target.character.stat.Status = intervenceStatus.status.Normal;
                    break;
            }
            Debug.Log(target.character.name + " is " + target.character.stat.Status);
        }
    }
    private void SkillBuffOrDebuff(){

    }
    private void SkillStatChange(Fighter attacker, Fighter target)
    {
        double actualRate = random.NextDouble();
        if (actualRate < attacker.character.skill.effectStatRate)
        {
            switch (attacker.character.skill.skillEffectToStat)
            {
                case EffectToStat.effectToStat.Buff:
                    
                    break;
                case EffectToStat.effectToStat.Debuff:
                    
                    break;
                case EffectToStat.effectToStat.Heal:
                    
                    break;
                default:
                    
                    break;
            }
            Debug.Log(target.character.name + " is " + target.character.stat.Status);
        }
    }
    private void SkillAttack(Fighter attacker)
    {
        battle.setIconSkill(heroInBattle.FindIndex(a => a == attacker), attacker.indexPosition, attacker.charTeam);
        foreach (var index in ChoosedPlayer.targetChar)
        {
            if (heroInBattle[index]!=null && heroInBattle[index].character!=null)
            {
                if (heroInBattle[index].HP > 0)
                {
                    double interval = random.NextDouble() * (1.0 - 0.85) + 0.85;

                    double basicDamage = (((2 * attacker.character.stat.level) / 5) + 2) * (attacker.character.skill.power * (attacker.character.stat.Atk / heroInBattle[index].character.stat.Def));
                    double typeEffective = ChartWeakness.ElementChart(attacker.character.element, heroInBattle[index].character.element);
                    double damage = ((basicDamage / 50) + 2) * typeEffective * isCritical(attacker) * intervenceState(attacker) * interval;

                    heroInBattle[index].HP -= damage;
                    Debug.Log(attacker.character.name + " give " + damage + " damages to " + heroInBattle[index].character.name);
                    Debug.Log(heroInBattle[index].character.name + " HP  " + heroInBattle[index].HP + " / " + heroInBattle[index].character.stat.HP + " left");

                    battle.getAttackPose(heroInBattle[index].indexPosition, heroInBattle[index].charTeam);
                    battle.updateHPBar(heroInBattle.FindIndex(a => a == heroInBattle[index]), heroInBattle[index].indexPosition, heroInBattle[index].charTeam, damage);

                    SkillStatChange(attacker, heroInBattle[index]);
                    SkillStatusIntervence(attacker, heroInBattle[index]);
                }
            }
        }

        heroInBattle[ChoosedPlayer.activeChar].Energy = 0;
        battle.updateEnergyBar(heroInBattle.FindIndex(a => a == attacker), attacker.indexPosition, attacker.charTeam);
        hasAction = true;
    }
}
