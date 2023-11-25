using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intervenceStatus
{
    public enum status { Normal, Paralyze, Poisoned, Burn}

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
    public int level = 1;

    public int criticalStage = 0;

    private intervenceStatus.status status;
    public intervenceStatus.status Status
    {
        get { return status; }
        set { status = value; }
    }

    private int accStage;
    public int AccStage
    {
        get { return accStage; }
        set { accStage += value; }
    }

    private int evaStage;
    public int EvaStage
    {
        get { return evaStage; }
        set { evaStage += value; }
    }

    public double exp;

    public double baseAtk;
    public double Atk
    {
        get { return baseAtk + (baseAtk * (level * 0.1)); }
    }
    public double baseDef;
    public double Def
    {
        get { return baseDef + (baseDef * (level * 0.05)); }
    }
    public double baseHP;
    public double HP
    {
        set { baseHP = baseHP - value; }
        get { return baseHP + (baseHP * (level * 0.2)); }
    }
    public double baseSpd;
    public double Spd
    {
        get { return (baseSpd + (baseSpd * (level * 0.02)))*(Status == intervenceStatus.status.Paralyze ? 0.75 : 1.0); }
    }
    public double baseAcc;
    public double Acc
    {
        get { return baseAcc * intervenceStatus.intervenceAcc(AccStage); }
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
