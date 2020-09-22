using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    [SerializeField] private GameObject cardBack;
    private int cardID;
    public int CardID
    {
        get{ return cardID; }
    }

    public void OnMouseDown()
    {
        if(cardBack.activeSelf && sceneController.CanReveal)
        {
            cardBack.SetActive(false);
            sceneController.CardRevealed(this);
        }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        cardID = id;
        GetComponent<SpriteRenderer>().sprite = image;  //Gets the sprite renderer component and changes the property of it's sprite
    }

    public void UnReveal()
    {
        cardBack.SetActive(true);
    }

}
