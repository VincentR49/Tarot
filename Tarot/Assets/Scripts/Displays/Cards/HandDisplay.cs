using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Gère l'affichage d'une main
public class HandDisplay : MonoBehaviour
{
    public Player player;
    public GameObject cardPrefab;
    public int pixelBetweenCard = 10;
    public bool hideCpuCards = true;

    private bool HideCards => (player is CpuPlayer) && hideCpuCards;
    private List<GameObject> cardObjects;
    private CardList currentHand = new CardList();

    private void Start()
    {
        Clean();
    }


    private void Update()
    {
       
        if (player != null)
        {
            currentHand = player.Hand;
        }
        else
        {
            currentHand = null;
        }
        UpdateDisplay();
    }


    private void UpdateDisplay()
    {
        // A optimiser
        Clean();   
        if (currentHand == null)
        {
            return;
        }
        int startPixel = -currentHand.Count * pixelBetweenCard / 2;
        int count = 0;
        foreach (Card card in currentHand)
        {
            GameObject cardObject = Instantiate(cardPrefab, transform);
            cardObject.transform.localPosition = new Vector2(startPixel + count * pixelBetweenCard, 0);
            CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
            cardDisplay.card = card;
            cardDisplay.SetFlipped(HideCards);
            cardObjects.Add(cardObject);
            count++;
        }
    }


    private void Clean()
    {
        if (cardObjects != null)
        {
            foreach (GameObject go in cardObjects)
            {
                Destroy(go);
            }
        }
        cardObjects = new List<GameObject>();
    }
}