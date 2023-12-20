using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HistoryMenu : MonoBehaviour
{
    public int indexHistory;
    public Character character;
    [SerializeField] public TextMeshProUGUI titleMenu;

    public void setTitle()
    {
        titleMenu.text = character.character.descriptionAndHistories.histories.listChapter[indexHistory].title;
    }

    public void onClickMenu()
    {
        CharEnsiklopedia.textHistory = character.character.descriptionAndHistories.histories.listChapter[indexHistory].synopsis;
    }
}
