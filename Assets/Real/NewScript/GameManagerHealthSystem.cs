using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerHealthSystem : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public GameObject playerObject;
        public ScriptableHealth playerHealth;
        public Slider healthSlider;
        public Slider EnergySlider;
    }

    public List<PlayerData> players = new List<PlayerData>();

    void Start()
    {
        foreach (var player in players)
        {
            if (player.playerObject.activeSelf)
            {
                player.healthSlider.maxValue = player.playerHealth.maxHealth;
                player.healthSlider.value = player.playerHealth.currentHealth;

                player.EnergySlider.maxValue = player.playerHealth.specialMoveEnergy;
                player.playerHealth.currentEnergy = 0;
                player.EnergySlider.value = 0;
            }
        }
    }

    void Update()
    {
        UpdateHealthSliders();
        UpdateEnergySliders();
    }

    private void UpdateHealthSliders()
    {
        foreach (var player in players)
        {
            if (player.playerObject.activeSelf && player.healthSlider.value != player.playerHealth.currentHealth)
            {
                player.healthSlider.value = player.playerHealth.currentHealth;
            }
        }
    }

    private void UpdateEnergySliders()
    {
        foreach (var player in players)
        {
            if (player.playerObject.activeSelf && player.EnergySlider.value != player.playerHealth.currentEnergy)
            {
                player.EnergySlider.value = player.playerHealth.currentEnergy;
            }
        }
    }
}