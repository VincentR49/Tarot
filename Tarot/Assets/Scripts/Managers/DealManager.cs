using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Classe gérant le choix du dealer et la distribution des cartes à chaque manche
// Séparer en dealingManager et dealerManager ?
public class DealManager : ProcessManager
{
	protected override string Name => "Dealing";
	[Tooltip("Reference to the full standard deck")]
	public Deck standardDeck;
    [Tooltip("Reference to the full standard deck")]
    public PlayerList players;
	[Tooltip("Reference to the global dog")]
	public Dog dog;
	private Deck deck;

	private static int GetNumberOfCardsToDeal(int nPlayer) => (nPlayer <= 3) ? 4 : 3;

	public override void StartProcess()
	{
		base.StartProcess();
		dog.Clear(); 
        CleanPlayersHand();
        SelectDealer();
		PrepareDeck();
		DealCards();
		SortPlayersHand();
		FinishProcess();
	}
	
	
    private void CleanPlayersHand()
    {
        foreach (Player player in players.Items)
        {
            player.PrepareForNewHand();
        }
    }

	private void SelectDealer()
	{
		Player lastDealer = players.GetDealer();
		Player newDealer;
		if (lastDealer == null)
		{
			// We select randomly the new dealer
			System.Random rnd = new System.Random();
			int dealerIndex = rnd.Next(0, players.Count-1);
			newDealer = players.Items[dealerIndex];
		}
		else
		{
			lastDealer.IsDealer = false;
			newDealer = players.GetNext(lastDealer);
		}
		newDealer.IsDealer = true;
		Debug.Log("Dealer selection: " + newDealer.name);
	}
	
	
	private void PrepareDeck()
	{
		Debug.Log("Deck preparation");
		deck = Instantiate(standardDeck);
		deck.Shuffle();
	}

	
	private void DealCards()
	{
		int nPlayer = players.Count;
		int nCard = GetNumberOfCardsToDeal (nPlayer);
		int nDog = Dog.GetNumberOfCards (nPlayer);
		List<int> dogIndexes = GetPutInDogIndexes (nDog, nCard, deck.Count);
		Player receiver = players.GetNext (players.GetDealer());
		int index = 0;
        int securityCount = 0;
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
            securityCount++;
            if (securityCount > 100)
            {
                Debug.LogError("Please make sure the number of card in the deck is correct.");
				return;
            }
		}
		Debug.Log("Cards have been dealed successfully");	
	}
	
	
	private void SortPlayersHand()
	{
		foreach (Player p in players.Items)
		{
			p.SortHand();
		}
		Debug.Log("Player's hands have been sorted");
	}
	
	
	private void PutCardInDog()
	{
		Card card = deck.Take();
		if (card != null)
		{
			dog.Add(card);
		}
	}
	
	
	private void GiveCardTo(Player player)
	{
		Card card = deck.Take();
		if (card != null)
		{
			player.Hand.Add(card);
		}
		else
		{
			Debug.LogError("No card left to give to the player");
		}
	}
	
	
	private void GiveCardsTo(Player player, int nCard)
	{
		for (int i = 0; i < nCard; i++)
		{
			GiveCardTo(player);
		}
	}

	
	// Define when to put cards in Dog
	private static List<int> GetPutInDogIndexes(int nDog, int nCard, int nDeck)
	{
		int nDeal = (nDeck - nDog) / nCard;
		return Utils.GetRandomUniqueIndexes(nDog, 2, nDeal-2);
	}
}