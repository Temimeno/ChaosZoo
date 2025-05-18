using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCharacter : MonoBehaviour
{
    public List<GameObject> Character = new List<GameObject>();
    public SelectCharacter selectCharacter;
    public GameManagerHealthSystem gameManagerHealthSystem;

    void Start()
    {
        selectCharacter = GameObject.FindGameObjectWithTag("SelectCharacter").GetComponent<SelectCharacter>();
        SetActiveCharacterplayer01();
        SetActiveCharacterplayer02();
        gameManagerHealthSystem.SetUpEverything();
    }

    public void SetActiveCharacterplayer01()
    {
        if (selectCharacter.characterplayer01 == 1) Character[0].SetActive(true);
        else if (selectCharacter.characterplayer01 == 2) Character[1].SetActive(true);
        else if (selectCharacter.characterplayer01 == 3) Character[2].SetActive(true);
        else if (selectCharacter.characterplayer01 == 4) Character[3].SetActive(true);

    }

    public void SetActiveCharacterplayer02()
    {
        if (selectCharacter.characterplayer02 == 1) Character[4].SetActive(true);
        else if (selectCharacter.characterplayer02 == 2) Character[5].SetActive(true);
        else if (selectCharacter.characterplayer02 == 3) Character[6].SetActive(true);
        else if (selectCharacter.characterplayer02 == 4) Character[7].SetActive(true);
    }

}
