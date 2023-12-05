using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerCharacter : MonoBehaviour
{
    public static List<CharModel> unlockedCharacters = new List<CharModel>();
    CharData dataChar;

    // Start is called before the first frame update
    void Start()
    {
        /// DEBUG PREFS
        GameObject eventSystem = GameObject.Find("charData");
        dataChar = eventSystem.GetComponent<CharData>();

        if (getIsInit())
        {
            SaveCharacter(0);
            SaveCharacter(1);
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

        // PlayerPrefs.DeleteKey("prefsState");
        // PlayerPrefs.DeleteKey("UnlockedCharacterList");
        // Debug.Log("Try to delete player data");
    }

    public void SaveCharacter(int index)
    {
        // Cek apakah karakter sudah ada dalam list
        int existingIndex = unlockedCharacters.FindIndex(c => c.name == dataChar.charData[index].character.name);

        if (existingIndex != -1)
        {
            // Jika karakter sudah ada, update data karakter
            unlockedCharacters[existingIndex] = dataChar.charData[index].character;

        }
        else
        {
            // Jika karakter belum ada, tambahkan ke list
            unlockedCharacters.Add(dataChar.charData[index].character);
        }
        // Simpan list karakter ke PlayerPrefs
        SaveCharacterList(index);
    }

    // Simpan list karakter ke PlayerPrefs
    private void SaveCharacterList(int index)
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

        bool isInit = !String.IsNullOrEmpty(initPrefs) && initPrefs != "" ? false : true;
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
            // Debug.Log("Get Player Data");
            // Debug.Log(characterListData);


            string[] jsonParts = characterListData.Split(new string[] { " <sep> " }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in jsonParts)
            {
                // Debug.Log(part);
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
