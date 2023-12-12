using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseItem : MonoBehaviour
{
    public int indexChar;
    public CharModel character;
    [SerializeField] private Button btn;
    [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private Image profile;
    // Start is called before the first frame update
    void Start()
    {
        profile.sprite = character.attribut.profile;
        txtLevel.text = character.stat.Level.ToString();
    }

    public void chooseClicked()
    {
        PlayerInput.clickTeam(indexChar, ChoosePlayer.teamIndex);
        Battle.choosed += 1;
        btn.interactable = false;
    }
}
