using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Gère l'affichage d'une main
[RequireComponent(typeof(GridLayout))]
public class HandDisplay : MonoBehaviour
{
    public Player player;
    public GameObject cardPrefab;
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
        if (player != null && player.Hand != null)
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
        foreach (Card card in Hand)
        {
            GameObject cardObject = Instantiate(cardPrefab, transform);
            CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
            cardDisplay.card = card;
			cardDisplay.SetFlipped(HideCards);
            cards.Add(cardObject);
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