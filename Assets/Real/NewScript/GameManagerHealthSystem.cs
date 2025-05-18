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

    public List<float> activePlayersHealth = new List<float>();
    public GameObject gameOverPanel;
    public Transform positionPlayer01;
    public Transform positionPlayer02;
    public int CounterWinPlayer01;
    public int CounterWinPlayer02;
    public GameObject player01;
    public GameObject player02;

    private bool hasRoundEnded = false; // ✅ เพิ่ม flag

    void Start()
    {
        activePlayersHealth.Clear();
        CounterWinPlayer01 = 0;
        CounterWinPlayer02 = 0;
        hasRoundEnded = false;

        foreach (var player in players)
        {
            if (player.playerObject.activeSelf)
            {
                player.healthSlider.maxValue = player.playerHealth.maxHealth;
                player.healthSlider.value = player.playerHealth.currentHealth;

                player.EnergySlider.maxValue = player.playerHealth.specialMoveEnergy;
                player.playerHealth.currentEnergy = 0;
                player.EnergySlider.value = 0;

                activePlayersHealth.Add(player.playerHealth.currentHealth);
            }
        }

        player01 = GameObject.FindGameObjectWithTag("Player01");
        player02 = GameObject.FindGameObjectWithTag("Player02");
    }

    void Update()
    {
        UpdateHealthSliders();
        UpdateEnergySliders();
        UpdateActivePlayersHealth();
        CheckHealthBarForWin();
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

    private void UpdateActivePlayersHealth()
    {
        activePlayersHealth.Clear();
        foreach (var player in players)
        {
            if (player.playerObject.activeSelf)
            {
                activePlayersHealth.Add(player.playerHealth.currentHealth);
            }
        }
    }

    private void CheckHealthBarForWin()
    {
        if (hasRoundEnded || activePlayersHealth.Count < 2) return; // ✅ หยุดถ้าเคยจบรอบแล้ว หรือไม่มีผู้เล่นครบ

        float player1Health = activePlayersHealth[0];
        float player2Health = activePlayersHealth[1];

        if (player1Health > player2Health && player2Health == 0)
        {
            CounterWinPlayer01++;
            hasRoundEnded = true;
            StartCoroutine(ResetPositionCharacter());
        }
        else if (player2Health > player1Health && player1Health == 0)
        {
            CounterWinPlayer02++;
            hasRoundEnded = true;
            StartCoroutine(ResetPositionCharacter());
        }
    }

    IEnumerator ResetPositionCharacter()
    {
        yield return new WaitForSeconds(2f);

        // รีเซ็ตตำแหน่ง
        player01.transform.position = positionPlayer01.position;
        player02.transform.position = positionPlayer02.position;

        foreach (var player in players)
        {
            if (player.playerObject.activeSelf && player.playerHealth.currentHealth == 0)
            {
                Animator animator = player.playerObject.GetComponentInChildren<Animator>();
                if (animator != null)
                {
                    animator.SetTrigger("recove"); // 🔁 ส่ง trigger ให้ recover
                }
            }
        }

        // รีเซ็ตพลังชีวิต และพลังพิเศษ
        foreach (var player in players)
        {
            if (player.playerObject.activeSelf)
            {
                player.playerHealth.currentHealth = player.playerHealth.maxHealth;
                player.playerHealth.currentEnergy = 0;
            }
        }

        hasRoundEnded = false;
    }
}
