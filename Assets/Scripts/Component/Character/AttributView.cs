using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffects{
    public enum effect{Projectile, Summon, Area}
}
[System.Serializable]
public class AttributView
{
    public Sprite profile;

    public Sprite idle;
    public Sprite attackPose;
    public Sprite skillPose;
    public Sprite skillEffect;
    public SkillEffects.effect effectType;
    public Sprite deathPose;

    public Sprite dialog;
}
