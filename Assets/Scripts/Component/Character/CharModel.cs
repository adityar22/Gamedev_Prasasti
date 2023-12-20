using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elements
{
    public enum Element { Fire, Water, Earth, Wind, Dark, Light }
}

public class CharTypes
{
    public enum CharType { Swordsman, Lancer, Archer, Caster, Berserker, Assassin }
}

public class Kingdoms
{
    public enum Kingdom { Majapahit, Sriwijaya, Demak, Singasari, MataramKuno, MataramIslam, GowaTalo, KalaYuga }
}

public class Tiers
{
    public enum Tier { Common, Rare, Epic, Ultimate}
}

public class TargetAttack{
    public enum Target{FrontLine, BackLine, All}
}

[System.Serializable]
public class CharModel
{
    public string name;
    public Kingdoms.Kingdom kingdom;
    public CharTypes.CharType type;
    public Elements.Element element;
    public Tiers.Tier tier;

    public TargetAttack.Target target;

    // base stat here
    [SerializeField] public Stat stat;
    [SerializeField] public Skill skill;
    [SerializeField] public Ability[] abilities = new Ability[]{};
    [SerializeField] public AttributView attribut;

    [SerializeField] public DescAndHist descriptionAndHistories;
}