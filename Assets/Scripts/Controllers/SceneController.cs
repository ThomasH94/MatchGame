using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour
{
    public const int GRID_ROWS = 2;
    public const int GRID_COLUMNS = 4;
    public const float OFFSET_X = 4.0f;
    public const float OFFSET_Y = 5.0f;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] images;

    private MainCard firstRevealedCard;
    private MainCard secondRevealedCard;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreLabel;
    public bool CanReveal
    {
        get { return secondRevealedCard == null; }
    }

    //TODO: Should call this something like "Initialize Cards" or something
    void Start()
    {
        //Set our starting position = to the original cards position
        Vector3 startPosition = originalCard.transform.position;

        //The amount of cards and matches
        int[] numbers = {0,0,1,1,2,2,3,3}; 
        numbers = ShuffleArray(numbers);

        //We start with one card in the scene, and then we spawn cards if they aren't the original
        for(int i = 0; i < GRID_COLUMNS; i++)
        {
            for(int j = 0; j < GRID_ROWS; j++)
            {
                MainCard card;
                if(i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }

                int index = j * GRID_COLUMNS + i;
                int id = numbers[index];

                //Change the sprite to the card image based on what is in our image array using id as an indexer
                card.ChangeSprite(id, images[id]);

                float posX = (OFFSET_X * i) + startPosition.x;
                float posY = (OFFSET_Y * j) + startPosition.y;
                card.transform.position = new Vector3(posX, posY, startPosition.z);
            }
        }
    }


    //Shuffles the array we send from the start method by going through each item in the array and returning
    //a random number by creating a temp number and swapping the current index with the temp and getting from the random
    int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0; i < newArray.Length; i++)
        {
            int temp = newArray[i];
            int random = Random.Range(i, newArray.Length);
            newArray[i] = newArray[random];
            newArray[random] = temp;
        }

        return newArray;
    }

    public void CardRevealed(MainCard card)
    {
        if(firstRevealedCard == null)
        {
            firstRevealedCard = card;
        }

        else
        {
            secondRevealedCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        if(firstRevealedCard.CardID == secondRevealedCard.CardID)
        {
            score ++;
            scoreLabel.text = "Score: " + score;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            firstRevealedCard.UnReveal();
            secondRevealedCard.UnReveal();
        }

        firstRevealedCard = null;
        secondRevealedCard = null;

    }


}
