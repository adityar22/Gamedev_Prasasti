using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSkill
{
    public enum targetSkill { FrontLine, BackLine, Single, All }
}

public class EffectToStat
{
    public enum effectToStat { Buff, Debuff, None}
}

public class ChangeStat
{
    public enum changeStat { Atk, Def, Spd, Acc, Eva}
}

[System.Serializable]
public class Skill
{
    public string name;
    public double power;

    public double consumeEnergy;

    public TargetSkill.targetSkill skillTarget;

    public intervenceStatus.status skillEffectToStatus;
    public double effectStatusRate;

    public EffectToStat.effectToStat skillEffectToStat;
    public double effectStatRate;
    public ChangeStat.changeStat statChange;
    public int stageChange;
    public double ratioChange;

    [TextArea]
    public string description;
}
