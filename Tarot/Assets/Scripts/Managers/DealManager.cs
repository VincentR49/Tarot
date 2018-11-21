using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe gérant le choix du dealer et la distribution des cartes à chaque manche
public class DealManager : MonoBehaviour
{
	[Tooltip("Reference to the full standard deck")]
	public Deck standardDeck;
	[Tooltip("Reference to the full standard deck")]
	public PlayerList players:
	[Tooltip("Reference to the global dealer")]
	public Player dealer
	[Tooltip("Reference to the global dog")]
	public Dog dog;
	private Deck deck;
	
	
	private void PrepareDeck()
	{
		Debug.Log("Deck preparation");
		deck = Instantiate(standardDeck);
		deck.Shuffle();
	}
	
	
	public void DealCards()
	{
		PrepareDeck();
		int nPlayer = players.Count;
		int nCard = GetNumberOfCardsToDeal (nPlayer);
		int nDog = GetNumberOfCardsInDog (nPlayer);
		List<int> dogIndexes = GetPutInDogIndexes (nDog, nCard, deck.Count);
		Debug.Log ("Dog indexes " + dogIndexes);
		Player receiver = players.GetNext (dealer);
		int index = 0;
		while (deck.NCard >= 0)
		{
			if (dogIndexes.Contains(index))
			{
				PutCardInDog();
				dogIndexes.Remove(index);
			}
			else
			{
				GiveCardsTo(receiver, nCard);
				receiver = players.GetNext(receiver);
				index ++;
			}
		}
		Debug.Log("Cards have been dealed successfully");
	}
	
	
	public void SelectRandomDealer()
	{
		System.Random rnd = new System.Random();
		int dealerIndex = rnd.Next(0, players.Count-1);
		dealer = players[dealerInder];
		Debug.Log("The dealer was chosen randomly: " + dealer.name);
	}
	
	
	// We get the next player in the list
	public void SelectNextDealer()
	{
		dealer = players.GetNext(dealer);
		Debug.Log("The next dealer is: " + dealer.name);
	}
	
	
	private void PutCardInDog()
	{
		Card card = deck.Take();
		if (card != null)
		{
			Debug.Log("Gave " + card.ToString() + " to " + player.name);
			dog.Add(card);
		}
		else
		{
			Debug.Log("No card left to add in the dog");
		}
	}
	
	
	private bool GiveCardTo(Player player)
	{
		Card card = deck.Take();
		if (card == null)
		{
			Debug.Log("No card to give to " + player.name);
			return false;
		}
		else
		{
			player.GetHand().Add(card);
			return true;
		}
	}
	
	
	private bool GiveCardsTo(Player player, int nCard)
	{
		for (int i = 0; i < nCard; i++)
		{
			if (!GiveCardTo(player))
			{
				return false;
			}
		}
		return true;
	}
	
	
	private static int GetNumberOfCardsToDeal(int nPlayer)
	{
		return (nPlayer <= 3) ? 4 : 3;
	}
	
	
	private static GetNumberOfCardsInDog(int nPlayer)
	{
		return (nPlayer <= 4) ? 6 : 3;
	}
	
	
	// Define when to put cards in Dog
	private static List<int> GetPutInDogIndexes(int nDog, int nCard, int nDeck)
	{
		System.Random rnd = new System.Random();
		List<int> indexes = new List<int>();
		int nDeal = (nDeck - nDog) / nCard;
		// generate the liste of possible moment to put a card inside the dog
		// We dont put the dog at first iteration, and at the last iteration
		List<int> possibleIndexes = Enumerable.Range(1, nDeal-2).ToList(); 
		for (int i = 0; i < nDog; i++)
		{
			int index = possibleIndexes [rnd.Next(0, possibleIndexes.Count-1)];
			indexes.Add (index);
			possibleIndexes.Remove (index); // to have unique indexes
		}
		return indexes;
	}
}