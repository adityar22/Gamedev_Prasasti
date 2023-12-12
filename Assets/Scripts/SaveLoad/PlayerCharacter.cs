using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerCharacter : MonoBehaviour
{
    public List<CharModel> unlockedCharacters = new List<CharModel>();

    CharData dataChar;

    // Start is called before the first frame update
    void Start()
    {
        /// DEBUG PREFS

        // PlayerPrefs.DeleteKey("prefsState");
        // PlayerPrefs.DeleteKey("UnlockedCharacterList");
        // Debug.Log("Try to delete player data");
    }

    public void LoadCharacter()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        Battle battle = eventSystem.GetComponent<Battle>();
        GameObject _charDataObj = battle._charData;
        dataChar = _charDataObj.GetComponent<CharData>();

        if (getIsInit())
        {
            SaveCharacter(dataChar.charData[1].character);
            SaveCharacter(dataChar.charData[2].character);
            SaveCharacter(dataChar.charData[3].character);
        }

        List<CharModel> playerPrefsLoad = GetUnlockedCharacterList();

        //Update Data
        List<Character> updated = new List<Character>();
        foreach (var load in playerPrefsLoad)
        {
            updated.Add(dataChar.charData.Find(c => c.character.name == load.name));
        }

        foreach (var chars in updated)
        {
            unlockedCharacters.Add(chars.character);
        }

        Debug.Log("Total loaded character: " + unlockedCharacters.Count);
    }

    public void SaveCharacter(CharModel toSaveCharacter)
    {
        // Cek apakah karakter sudah ada dalam list
        int existingIndex = unlockedCharacters.FindIndex(c => c.name == toSaveCharacter.name);
        Debug.Log(existingIndex);

        if (existingIndex != -1)
        {
            // Jika karakter sudah ada, update data karakter
            unlockedCharacters[existingIndex] = toSaveCharacter;

        }
        else
        {
            // Jika karakter belum ada, tambahkan ke list
            unlockedCharacters.Add(toSaveCharacter);
        }
        // Simpan list karakter ke PlayerPrefs
        SaveCharacterList();
    }

    // Simpan list karakter ke PlayerPrefs
    private void SaveCharacterList()
    {
        string charListData = "";
        foreach (var data in unlockedCharacters)
        {
            charListData += JsonUtility.ToJson(data) + " <sep> ";
        }
        // Debug.Log("List Contents: " + charListData);
        PlayerPrefs.SetString("UnlockedCharacterList", charListData);
    }

    private static bool getIsInit()
    {
        string initPrefs = PlayerPrefs.GetString("prefsState", "");

        bool isInit = initPrefs != "hasInit" ? true : false;
        PlayerPrefs.SetString("prefsState", "hasInit");
        return isInit;
    }


    // Mendapatkan list karakter yang di-unlock oleh player
    public static List<CharModel> GetUnlockedCharacterList()
    {
        List<CharModel> loadedChar = new List<CharModel>();
        string characterListData = PlayerPrefs.GetString("UnlockedCharacterList", "");
        if (characterListData != "" && characterListData != "{}")
        {
            string[] jsonParts = characterListData.Split(new string[] { " <sep> " }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in jsonParts)
            {
                if (!string.IsNullOrEmpty(part))
                {
                    CharModel charLoad = JsonUtility.FromJson<CharModel>(part);
                    loadedChar.Add(charLoad);
                }
            }

            return loadedChar;
        }
        else
        {
            Debug.Log("Player Data Empty");
            return new List<CharModel>();
        }
    }
}
