using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Classe gérant le choix du dealer et la distribution des cartes à chaque manche
public class DealManager : MonoBehaviour
{
	[Tooltip("Reference to the full standard deck")]
	public Deck standardDeck;
    [Tooltip("Reference to the full standard deck")]
    public PlayerList players;
	[Tooltip("Reference to the global dog")]
	public Dog dog;
	private Deck deck;
	private Player dealer;
	
	private void PrepareDeck()
	{
		Debug.Log("Deck preparation");
		deck = Instantiate(standardDeck);
		deck.Shuffle();
        dog.Clear();
	}
	
	
	public void DealCards()
	{
		PrepareDeck();
		int nPlayer = players.Count;
		int nCard = GetNumberOfCardsToDeal (nPlayer);
		int nDog = GetNumberOfCardsInDog (nPlayer);
		List<int> dogIndexes = GetPutInDogIndexes (nDog, nCard, deck.Count);
		Player receiver = players.GetNext (dealer);
		int index = 0;
        int count = 0;
		while (deck.NCard > 0)
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
            count++;
            if (count > 100)
            {
                Debug.Log("An error occured during card dealing. Please make sure the number of card in the deck is correct.");
            }
		}
		Debug.Log("Cards have been dealed successfully");
	}
	
	
	public void SelectRandomDealer()
	{
		System.Random rnd = new System.Random();
		int dealerIndex = rnd.Next(0, players.Count-1);
		SelectNewDealer (players.Items[dealerIndex]);
		Debug.Log("The dealer was chosen randomly: " + dealer.name);
	}
	
	
	// We get the next player in the list
	public void SelectNextDealer()
	{
		SelectNewDealer (players.GetNext(dealer));
		Debug.Log("The next dealer is: " + dealer.name);
	}
	
	
	private void PutCardInDog()
	{
		Card card = deck.Take();
		if (card != null)
		{
			Debug.Log("Put " + card.ToString() + " to dog");
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
			player.Hand.Add(card);
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
	
	
	private static int GetNumberOfCardsInDog(int nPlayer)
	{
		return (nPlayer <= 4) ? 6 : 3;
	}
	
	
	// Define when to put cards in Dog
	private static List<int> GetPutInDogIndexes(int nDog, int nCard, int nDeck)
	{
		int nDeal = (nDeck - nDog) / nCard;
		return Utils.GetRandomUniqueIndexes(nDog, 2, nDeal-2);
	}
	
	
	private void SelectNewDealer(Player player)
	{
		if (dealer != null)
		{
			dealer.IsDealer = false;
		}
		player.IsDealer = true;
	}
}