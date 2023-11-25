using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public static List<Character> unlockedCharacters = new List<Character>();

    // Start is called before the first frame update
    void Start()
    {
        /// DEBUG PREFS

        /*GameObject eventSystem = GameObject.Find("charData");
        CharData dataChar = eventSystem.GetComponent<CharData>();
        Character unlockedCharacter = dataChar.charData[0];
        unlockedCharacter.SaveCharacter();*/

        /*PlayerPrefs.DeleteKey("UnlockedCharacterList");
        Debug.Log("Try to delete player data");*/

        unlockedCharacters = GetUnlockedCharacterList();

        /// DEBUG PREFS
    }

    void Update()
    {
        foreach (Character character in unlockedCharacters)
        {
            Debug.Log("Unlocked character: " + character.name);
        }
    }

    // Mendapatkan list karakter yang di-unlock oleh player
    public static List<Character> GetUnlockedCharacterList()
    {
        string characterListData = PlayerPrefs.GetString("UnlockedCharacterList", "");
        if (!string.IsNullOrEmpty(characterListData))
        {
            Debug.Log("Player Data Loaded!");
            return JsonUtility.FromJson<List<Character>>(characterListData);
        }
        Debug.Log("Player Data Empty");
        return new List<Character>();
    }
}
