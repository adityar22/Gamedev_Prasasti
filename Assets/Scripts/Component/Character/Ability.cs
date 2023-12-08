using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AbilityType
{
    public enum abilityType { init, conditionally, passive }
}
[System.Serializable]
public class Ability
{
    public string name;
    public AbilityType.abilityType abilityType;
    public EffectToStat.effectToStat abilityEffect;
    public ChangeStat.changeStat statEffect;
    public int stageChange;
    public double ratioChange;
    public float ratioHeal;
    [TextArea]
    public string description;

}