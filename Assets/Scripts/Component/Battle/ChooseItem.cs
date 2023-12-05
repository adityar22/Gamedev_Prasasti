using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseItem : MonoBehaviour
{
    public int indexChar;
    [SerializeField] private Button btn;
    [SerializeField] private Image profile;
    // Start is called before the first frame update
    void Start()
    {
        profile.sprite = PlayerCharacter.unlockedCharacters[indexChar].attribut.profile;
    }

    public void chooseClicked()
    {
        PlayerInput.clickTeam(indexChar, ChoosePlayer.teamIndex);
        Battle.choosed += 1;
        btn.interactable = false;
    }
}
