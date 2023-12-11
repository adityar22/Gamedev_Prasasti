using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RankTypes
{
    public enum rankType { Yodha, Virat, Mahayodha }
}

[System.Serializable]
public class intervenceStatus
{
    public enum status { Normal, Paralyze, Poisoned, Burn }

    public static double intervenceAcc(int stage)
    {
        switch (stage)
        {
            case -4:
                return 3.0 / 7;
                break;
            case -3:
                return 3.0 / 6;
                break;
            case -2:
                return 3.0 / 5;
                break;
            case -1:
                return 3.0 / 4;
                break;
            case 0:
                return 3.0 / 3;
                break;
            case 1:
                return 4.0 / 3;
                break;
            case 2:
                return 5.0 / 3;
                break;
            case 3:
                return 6.0 / 3;
                break;
            case 4:
                return 7.0 / 3;
                break;
            default:
                return stage < 0 ? 3.0 / 8 : 8.0 / 3;
                break;
        }
    }
}

[System.Serializable]
public class Stat
{
    //Rank and Level Stat
    public int level = 1;
    public int Level
    {
        get { return level; }
    }

    private RankTypes.rankType rankType;
    public RankTypes.rankType RankType
    {
        get
        {
            if (Level > 0 && Level <= 16)
            {
                return RankTypes.rankType.Yodha;
            }
            else if (Level > 16 && Level <= 36)
            {
                return RankTypes.rankType.Virat;
            }
            else if (Level > 36 && Level <= 50)
            {
                return RankTypes.rankType.Mahayodha;
            }
            return RankTypes.rankType.Mahayodha;
        }
    }
    [SerializeField] private double baseExp;
    public double BaseExp
    {
        get { return baseExp; }
    }
    public double targetExp
    {
        get
        {
            return (6.0 / 5) * Math.Pow(level + 1, 3) - 15 * Math.Pow(level + 1, 2) + (100 * (level + 1)) - 140;
        }
    }
    public double totalExp
    {
        get
        {
            double total = 0;
            for (int i = 1; i < level; i++)
            {
                total += baseExp + baseExp * (0.2 * i);
            }
            return total + selfExp;
        }
    }

    private double selfExp;
    public double SelfExp
    {
        get
        {
            return selfExp;
        }
        set
        {
            selfExp += value;
            while (selfExp > targetExp)
            {
                level += 1;
                double expRemain = selfExp - targetExp;
                selfExp = expRemain;
            }
        }
    }

    //Modifier Stat
    public int criticalStage = 0;

    private intervenceStatus.status status;
    public intervenceStatus.status Status
    {
        get { return status; }
        set { status = value; }
    }

    //HP Stat
    public double baseHP;
    public double HP
    {
        set { baseHP = baseHP - value; }
        get { return baseHP + (baseHP * (level * 0.2)); }
    }

    // Basic Stat
    public double baseAtk;
    public double atkMod = 1.0;
    public double Atk
    {
        get { return (baseAtk + (baseAtk * (level * 0.1))) * atkMod * (Status == intervenceStatus.status.Burn ? 0.75 : 1.0); }
    }
    public double baseDef;
    public double defMod = 1.0;
    public double Def
    {
        get { return (baseDef + (baseDef * (level * 0.05))) * defMod * (Status == intervenceStatus.status.Poisoned ? 0.75 : 1.0); }
    }
    public double baseSpd;
    public double spdMod = 1.0;
    public double Spd
    {
        get { return (baseSpd + (baseSpd * (level * 0.02))) * spdMod * (Status == intervenceStatus.status.Paralyze ? 0.75 : 1.0); }
    }

    //Secondary Stat
    private int accStage;
    public int AccStage
    {
        get { return accStage; }
        set { accStage += value; }
    }
    public double baseAcc;
    public double Acc
    {
        get { return baseAcc * intervenceStatus.intervenceAcc(AccStage); }
    }

    private int evaStage;
    public int EvaStage
    {
        get { return evaStage; }
        set { evaStage += value; }
    }
    public double baseEva;
    public double Eva
    {
        get { return baseEva * intervenceStatus.intervenceAcc(EvaStage); }
    }

    public double baseEnergy;
    public double Energy
    {
        get { return baseEnergy + (baseEnergy * (level * 0.15)); }
    }
}
