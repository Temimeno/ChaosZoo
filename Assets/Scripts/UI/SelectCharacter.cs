using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    [Header("Database & UI References")]
    public CharacterDataBase characterDataBase;
    public GameObject ImagePrefeb;
    public Image CharacterPlayer01Image;
    public Image CharacterPlayer02Image;

    private GameObject currentCharacter01;
    public TextMeshProUGUI player01Name;
    public TextMeshProUGUI player02Name;
    public int characterplayer01;
    public int characterplayer02;

    private bool isPlayer01Locked = false; // ✅ flag ล็อกฝั่งซ้าย

    public GameObject ReadyButton;
    public GameObject StartButton;
    public string sceneToload;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // ===== ปุ่มเลือกตัวละคร =====
    public void SelectPengang()
    {
        SetCharacterById(4);
    }

    public void SelectShark()
    {
        SetCharacterById(1);
    }

    public void SelectCapybara()
    {
        SetCharacterById(2);
    }

    public void SelectKen()
    {
        SetCharacterById(3);
    }

    // ===== กด Lock เพื่อยืนยันฝั่งซ้าย =====
    public void LockPlayer01()
    {
        isPlayer01Locked = true;
        Debug.Log("Player01 locked character ID: " + characterplayer01);
        ReadyButton.SetActive(false);
        StartButton.SetActive(true);
    }

    // ===== ระบบแสดงภาพตัวละครตามฝั่ง =====
    public void SetCharacterById(int characterId)
    {
        CharacterData characterData = characterDataBase.GetCharacterById(characterId);

        if (characterData == null)
        {
            Debug.LogError("Character not found for ID: " + characterId);
            return;
        }

        if (!isPlayer01Locked)
        {
            // ฝั่งซ้ายยังไม่ล็อก → เปลี่ยนภาพฝั่งซ้าย
            CharacterPlayer01Image.sprite = characterData.ShowCharacterLeft;
            player01Name.text = characterData.Name;
            characterplayer01 = characterId;
        }
        else
        {
            // ฝั่งซ้ายล็อกแล้ว → เปลี่ยนภาพฝั่งขวา
            CharacterPlayer02Image.sprite = characterData.ShowCharacterRight;
            player02Name.text = characterData.Name;
            characterplayer02 = characterId;
        }
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene(sceneToload);
    }
}
