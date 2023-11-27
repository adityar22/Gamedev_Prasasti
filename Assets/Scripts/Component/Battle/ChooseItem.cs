using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseItem : MonoBehaviour
{
    public int indexChar;
    [SerializeField] private Button btn;
    CharData dataChar;
    [SerializeField] private Image profile;
    // Start is called before the first frame update
    void Start()
    {
        GameObject eventSystem = GameObject.Find("charData");
        dataChar = eventSystem.GetComponent<CharData>();
        profile.sprite = dataChar.charData[indexChar].character.attribut.profile;
    }

    public void chooseClicked()
    {
        PlayerInput.clickTeam(indexChar, ChoosePlayer.teamIndex);
        btn.interactable = false;
    }
}
