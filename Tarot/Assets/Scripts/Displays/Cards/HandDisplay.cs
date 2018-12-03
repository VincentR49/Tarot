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
    private List<GameObject> cards;
    String currentHandSignature = "";
    CardList Hand => player.Hand;
	
	
    private void Start()
    {
        Clean();
    }


    private void Update()
    {
        if (player != null)
        {
            if (player.Hand.ToString() != currentHandSignature)
            {
				currentHandSignature = player.Hand.ToString();
                UpdateDisplay();
            }
        }
        else
        {
			currentHandSignature = "";
            Clean(); 
        }
    }


    private void UpdateDisplay()
    {
        // A optimiser
        Clean();   
        if (Hand == null) return;
        int startPixel = -Hand.Count * pixelBetweenCard / 2;
        int count = 0;
        foreach (Card card in Hand)
        {
            GameObject cardObject = Instantiate(cardPrefab, transform);
            cardObject.transform.localPosition = new Vector2(startPixel + count * pixelBetweenCard, 0);
            CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
            cardDisplay.card = card;
			cardDisplay.SetFlipped(HideCards);
            cards.Add(cardObject);
            count++;
        }
    }


    private void Clean()
    {
        if (cards != null)
        {
            foreach (GameObject go in cards)
            {
                Destroy(go);
            }
        }
        cards = new List<GameObject>();
    }
}