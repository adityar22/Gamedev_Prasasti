using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elements
{
    [SerializeField] public enum Element { Fire, Water, Earth, Wind, Dark, Light }
}

public class CharTypes
{
    [SerializeField] public enum CharType { Swordsman, Lancer, Archer, Caster, Berserker, Assassin }
}

public class Kingdoms
{
    [SerializeField] public enum Kingdom { Majapahit, Sriwijaya, Demak, Singasari, MataramKuno, MataramIslam, GowaTalo, KalaYuga }
}

public class Tiers
{
    [SerializeField] public enum Tier { Common, Rare, Epic, Ultimate}
}

[System.Serializable]
public class CharModel
{
    public string name;
    public Kingdoms.Kingdom kingdom;
    public CharTypes.CharType type;
    public Elements.Element element;
    public Tiers.Tier tier;

    // base stat here
    [SerializeField] public Stat stat;
    [SerializeField] public Skill skill;
    [SerializeField] public AttributView attribut;
}