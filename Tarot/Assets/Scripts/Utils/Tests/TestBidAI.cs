using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;
// Test de l'AI pour les ench�res
public class TestBidAI : MonoBehaviour 
{
	// Parameters
	public CpuPlayer player;
	public int nPlayer = 4;
	public int playerPosition = 0;
	public int nRandomTests = 10;
	public Deck standardDeck;
	
	private int nHandCards => (standardDeck.Count - Dog.GetNumberOfCards(nPlayer)) / nPlayer;

	
	private void Start()
	{
		for (int = 0; i < nRandomTests; i++)
		{
			StartTestRandomCards();
		}
	}
	
	
	private void StartTestRandomCards()
	{
		Debug.Log("Start test bid AI with random hand");
		Debug.Log("-----------------------------------------------");
		player.Hand.Clear();
		List<int> cardIndexes = GetRandomUniqueIndexes (nHandCards, 0, standardDeck.Count-1);
		foreach (int index in cardIndexes)
		{
			AddCardToHand (standardDeck.Items[index]);
		}
		player.MakeABid (nPlayer, playerPosition, Bid.Pass);
		Debug.Log("Player Bid: " + player.bid);
		Debug.Log("-----------------------------------------------");
		Debug.Log("");
	}
	

	private void AddCardToHand(Card card)
	{
		player.Hand.Add (card); 
		Debug.Log("Add card to hand: " + card);
	}
	
	
	private void AddCardToHand(CardType type, CardRank rank) 
	{ 
		AddCardToHand (GetCard (type, rank));
	}
	
	
	private Card GetCard(CardType type, CardRank rank) => standardDeck.GetCard(type, rank);
}