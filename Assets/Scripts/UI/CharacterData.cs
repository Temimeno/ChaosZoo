using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Character")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private int id = -1;
    [SerializeField] private string name = "New";
    [SerializeField] private Sprite icon;
    [SerializeField] private Sprite showCharacterLeft;
    [SerializeField] private Sprite showCharacterRight;

    public int Id => id;
    public string Name => name;
    public Sprite Icon => icon;
    public Sprite ShowCharacterLeft => showCharacterLeft;
    public Sprite ShowCharacterRight => showCharacterRight; 
}
