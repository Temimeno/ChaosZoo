using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // ✅ อย่าลืมใส่ถ้าจะใช้ .Any()

[CreateAssetMenu(fileName = "New Character Database", menuName = "Characters/Database")]
public class CharacterDataBase : ScriptableObject
{
    [SerializeField] private CharacterData[] characters = new CharacterData[0];

    public CharacterData[] GetAllCharacters() => characters;

    public CharacterData GetCharacterById(int id)
    {
        foreach (var character in characters)
        {
            if (character.Id == id)
            {
                return character;
            }
        }

        return null;
    }

    public bool IsValidCharacterId(int id)
    {
        return characters.Any(x => x.Id == id);
    }
}
