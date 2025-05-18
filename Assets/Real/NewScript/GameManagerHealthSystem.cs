using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public List<GameObject> ObjectCouter01;
    public List<GameObject> ObjectCouter02;

    public Player01Health player01Health;
    public Player02Health player02Health;

    public TextMeshProUGUI textInGameOver;
    private bool hasRoundEnded = false; // ✅ เพิ่ม flag

    void Start()
    {
        activePlayersHealth.Clear();
        CounterWinPlayer01 = 0;
        CounterWinPlayer02 = 0;
        hasRoundEnded = false;
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);

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
        player01Health = GameObject.FindGameObjectWithTag("Player01Health").GetComponent<Player01Health>();
        player02Health = GameObject.FindGameObjectWithTag("Player02Health").GetComponent<Player02Health>();
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
        if (hasRoundEnded || activePlayersHealth.Count < 2) return;

        float player1Health = activePlayersHealth[0];
        float player2Health = activePlayersHealth[1];

        if (player1Health > player2Health && player2Health == 0)
        {
            CounterWinPlayer01++;
            hasRoundEnded = true;

            // ✅ เปิด Object ตามลำดับของ Player01
            if (CounterWinPlayer01 - 1 < ObjectCouter01.Count)
            {
                ObjectCouter01[CounterWinPlayer01 - 1].SetActive(true);
            }

            CheckForWinner();
        }
        else if (player2Health > player1Health && player1Health == 0)
        {
            CounterWinPlayer02++;
            hasRoundEnded = true;

            // ✅ เปิด Object ตามลำดับของ Player02
            if (CounterWinPlayer02 - 1 < ObjectCouter02.Count)
            {
                ObjectCouter02[CounterWinPlayer02 - 1].SetActive(true);
            }

            CheckForWinner();
        }
    }

    private void CheckForWinner()
    {
        if (CounterWinPlayer01 == 2)
        {
            StartCoroutine(WaitForEndgame());
        }
        else if (CounterWinPlayer02 == 2)
        {
            StartCoroutine(WaitForEndgame());
        }
        else
        {
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
        player01Health.knockout = false;
        player02Health.knockout = false;

        hasRoundEnded = false;
    }

    IEnumerator WaitForEndgame()
    {
        yield return new WaitForSeconds(1f);
        if (CounterWinPlayer01 == 2)
        {
            textInGameOver.text = "Player 01 Win";
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        else if (CounterWinPlayer02 == 2)
        {
            textInGameOver.text = "Player 02 Win";
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }
}
