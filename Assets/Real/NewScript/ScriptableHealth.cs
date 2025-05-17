using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableHealth", menuName = "ScriptableObjects/ScriptableHealth", order = 1)]

public class ScriptableHealth : ScriptableObject
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Stamina Setting")]
    public float specialMoveEnergy = 100;
    public float currentEnergy;
    
}
