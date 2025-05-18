using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayImage : MonoBehaviour
{
    public Image characterImage;

    public void SetCharacterSprite(Sprite sprite)
    {
        if (characterImage != null)
        {
            characterImage.sprite = sprite;
        }
    }

}
