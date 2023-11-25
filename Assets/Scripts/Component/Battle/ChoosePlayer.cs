using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlayer : MonoBehaviour
{
    public int teamIndex;

    public void choosed(int id)
    {
        GameObject _charData = GameObject.Find("charData");
        CharData data = _charData.GetComponent<CharData>();
        ChoosedPlayer.choosedChar[teamIndex] = data.charData[id];
    }
}
