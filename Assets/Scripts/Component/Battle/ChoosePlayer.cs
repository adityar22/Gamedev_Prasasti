using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePlayer : MonoBehaviour
{
    public static int teamIndex;

    public int indexOfChar;
    public int indexPosition;
    public Sprite placeHolderChar;

    private static Battle battle;

    void Start()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        battle = eventSystem.GetComponent<Battle>();
    }
    public void positionClicked()
    {
        if (Battle.statePhase == 0)
        {
            Debug.Log(teamIndex);
            ChoosePlayer.teamIndex = indexPosition;
            if (ChoosedPlayer.choosedChar[teamIndex].character != null && ChoosedPlayer.choosedChar[teamIndex].character.name != null)
            {
                Character removed = new Character();
                ChoosedPlayer.choosedChar[teamIndex] = removed;

                Image imageComponent = battle.player[teamIndex].GetComponent<Image>();
                imageComponent.sprite = placeHolderChar;

                Battle.choosed += -1;
            }
        }
    }
}
