using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams{
    public enum team{Player, Enemy}
}
public class Fighter
{
    public Teams.team charTeam;
    public int indexPosition;
    public CharModel character;
    public double HP;
    public double Energy;
    public BattleStatus battleStatus;
}