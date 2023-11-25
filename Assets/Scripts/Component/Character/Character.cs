using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elements
{
    [SerializeField] public enum Element { Fire, Water, Earth, Wind, Dark, Light }
}

public class CharTypes
{
    [SerializeField] public enum CharType { Swordsman, Lancer, Archer, Caster, Berserker, Assassin }
}

public class Kingdoms
{
    [SerializeField] public enum Kingdom { Majapahit, Sriwijaya, Demak, Singasari, MataramKuno, MataramIslam, GowaTalo }
}

public class Tiers
{
    [SerializeField] public enum Tier { Common, Rare, Epic, Ultimate}
}

[System.Serializable]
public class Character : MonoBehaviour
{
    public string name;
    public Kingdoms.Kingdom kingdom;
    public CharTypes.CharType type;
    public Elements.Element element;
    public Tiers.Tier tier;

    // base stat here
    [SerializeField] public Stat stat;
    [SerializeField] public Skill skill;
    [SerializeField] public AttributView attribut;


    //player unlocked character
    List<Character> unlockedCharacters;

    void Start()
    {
        unlockedCharacters = PlayerCharacter.unlockedCharacters;
    }

    public void SaveCharacter()
    {
        // Cek apakah karakter sudah ada dalam list
        int existingIndex = unlockedCharacters.FindIndex(c => c.name == this.name);

        if (existingIndex != -1)
        {
            // Jika karakter sudah ada, update data karakter
            unlockedCharacters[existingIndex] = this;
        }
        else
        {
            // Jika karakter belum ada, tambahkan ke list
            unlockedCharacters.Add(this);
        }

        // Simpan list karakter ke PlayerPrefs
        SaveCharacterList();
    }

    // Simpan list karakter ke PlayerPrefs
    private void SaveCharacterList()
    {
        string characterListData = JsonUtility.ToJson(unlockedCharacters);
        PlayerPrefs.SetString("UnlockedCharacterList", characterListData);
    }
}
