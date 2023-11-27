using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// CHOOSE PHASE CODE
public class ChoosedPlayer
{
    public static List<Character> choosedChar = new List<Character>();
    public static List<Character> choosedEnemy = new List<Character>();

    public static Character activeChar;
    public static Character targetEnemy;
}

public class Battle : MonoBehaviour
{
    [SerializeField] private GameObject battlePhase;
    [SerializeField] private GameObject choosePhase;

    [SerializeField] private GameObject[] player = new GameObject[] { };
    [SerializeField] private GameObject[] enemy = new GameObject[] { };

    [SerializeField] public GameObject _charData;
    [SerializeField] public GameObject _playerData;

    public List<ChooseItem> chooseItemList;
    public GameObject chooseBox;
    public Transform choosePanel;

    void Start()
    {
        // code for debug
        GameObject charData = Instantiate(_charData);
        CharData dataChar = charData.GetComponent<CharData>();

        GameObject playerData = Instantiate(_playerData);
        PlayerCharacter playerCharacter = playerData.GetComponent<PlayerCharacter>();

        initChoose();
    }

    public void initChoose()
    {
        foreach(var character in PlayerCharacter.unlockedCharacters)
        {
            GameObject chooseInstantiated = Instantiate(chooseBox, choosePanel);
            ChooseItem instantiate = chooseInstantiated.GetComponent<ChooseItem>();
            instantiate.indexChar = PlayerCharacter.unlockedCharacters.IndexOf(character);
        }
    }

    public void initBattle()
    {
        for (int i = 0; i < player.Length; i++)
        {
            Image imageComponent = this.player[i].GetComponent<Image>();
            Sprite yourSprite = ChoosedPlayer.choosedChar[i].character.attribut.idle;

            if (imageComponent != null)
            {
                // Set the sprite of the Image component
                imageComponent.sprite = yourSprite;

                // Calculate the aspect ratio of the sprite
                float aspectRatio = yourSprite.rect.width / yourSprite.rect.height;

                // Set the size of the Image component based on the sprite's aspect ratio
                imageComponent.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imageComponent.rectTransform.rect.width / aspectRatio);
            }
            else
            {
                Debug.LogError("Image component is not assigned.");
            }
        }

        for (int i = 0; i < enemy.Length; i++)
        {
            Image imageComponent = this.enemy[i].GetComponent<Image>();
            Sprite yourSprite = ChoosedPlayer.choosedEnemy[i].character.attribut.idle;


            if (imageComponent != null)
            {
                // Set the sprite of the Image component
                imageComponent.sprite = yourSprite;

                // Calculate the aspect ratio of the sprite
                float aspectRatio = yourSprite.rect.width / yourSprite.rect.height;

                // Set the size of the Image component based on the sprite's aspect ratio
                imageComponent.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imageComponent.rectTransform.rect.width / aspectRatio);
            }
            else
            {
                Debug.LogError("Image component is not assigned.");
            }
        }
    }
}