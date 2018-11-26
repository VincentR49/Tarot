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
        CleanDog();
    }


    private void Update()
    {
        UpdateDisplay();
    }


    public void Show()
    {
        flipped = false;
    }


    public void Hide()
    {
        flipped = true;
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


    private void UpdateDisplay()
    {
        // A optimiser
        CleanDog();
        if (dog == null)
        {
            return;
        }

        int count = 0;
        foreach (Card card in dog.Items)
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
