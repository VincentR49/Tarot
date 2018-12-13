using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;
// Test de l'AI pour les enchères
public class TestBidAI : MonoBehaviour 
{
	// Parameters
	public CpuPlayer player;
	public int nPlayer = 4;
	public int playerPosition = 0;
	public Deck standardDeck;
	
	private int nHandCards => (standardDeck.Count - Dog.GetNumberOfCards(nPlayer)) / nPlayer;

	
	private void Start()
	{
		StartTestRandomCards();
	}
	
	
	private void StartTestRandomCards()
	{
		Debug.Log("Start test bid AI with random hand");
		player.Hand.Clear();
		List<int> cardIndexes = GetRandomUniqueIndexes (nHandCards, 0, standardDeck.Count-1);
		foreach (int index in cardIndexes)
		{
			Card card = standardDeck.Items[index];
			AddCardToHand(card.type, card.rank);
		}
		player.MakeABid(nPlayer, playerPosition, Bid.Pass);
		Debug.Log("Player Bid: " + player.bid);
	}
	
	

	private void AddCardToHand(CardType type, CardRank rank) 
	{ 
		Card card = GetCard(type, rank);
		player.Hand.Add (card); 
		Debug.Log("Add card to hand: " + card);
	}
	
	private Card GetCard(CardType type, CardRank rank) => standardDeck.GetCard(type, rank);
}