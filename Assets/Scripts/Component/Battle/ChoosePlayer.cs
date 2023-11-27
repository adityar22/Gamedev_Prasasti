using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlayer : MonoBehaviour
{
    public static int teamIndex;

    public int indexPosition;

    public void positionClicked()
    {
        ChoosePlayer.teamIndex = indexPosition;
    }
}
