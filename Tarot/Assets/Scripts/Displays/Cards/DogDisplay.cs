using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gère l'affichage du chien
public class DogDisplay : MonoBehaviour
{
    public Dog dog;
    public GameObject cardPrefab;
    public List<GameObject> placeHolders;

    private bool flipped = true;
    private List<GameObject> cards;


    private void Start()
    {
        cards = new List<GameObject>();
    }


    public void ShowCards()
    {
        if (flipped)
        {
            FlipCards();
            flipped = false;
        }
        
    }


    public void HideCards()
    {
        if (!flipped)
        {
            FlipCards();
            flipped = true;
        }       
    }


    private void FlipCards()
    {
        foreach (GameObject card in cards)
        {
            CardDisplay cardDisplay = card.GetComponent<CardDisplay>();
            cardDisplay.FlipCardAnimated();
        }
    }


    private void CleanDog()
    {
        if (cards != null && cards.Count > 0)
        {
            foreach (GameObject go in cards)
            {
                Destroy(go);
            }
        }   
        cards = new List<GameObject>();
    }


    public void UpdateDisplay()
    {
        CleanDog();
        if (dog == null)
        {
            return;
        }
        int count = 0;
        foreach (Card card in dog.Value)
        {
            GameObject cardObject = Instantiate(cardPrefab, placeHolders[count].transform);
            cardObject.transform.localPosition = new Vector2(0, 0);
            CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
            cardDisplay.card = card;
            cardDisplay.SetFlipped(flipped);
            cards.Add(cardObject);
            count++;
        }
    }
}
