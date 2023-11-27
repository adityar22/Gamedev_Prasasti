using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseItem : MonoBehaviour
{
    public int indexChar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void chooseClicked()
    {
        PlayerInput.clickTeam(indexChar, ChoosePlayer.teamIndex);
    }
}
