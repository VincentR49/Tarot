using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// G�re le syst�me du chien
// Permet d'�changer ses cartes avec les cartes du chien si le niveau de prise le permet
public class DogMakingManager : ProcessManager 
{
	protected override string Name => "DogMaking";
	public PlayerList players;
	public Dog dog;
	public RunTimeCardList cardsToPutInDog;
	public float makeDogTimeLimitSec = 0; // security (� supprimer)
	private float startTimer = 0f;
	private Player Taker => players.GetTaker();
    private int nDog;
    
	
	private void Update()
	{
		if (status == ProcessState.Running)
		{
			if (Taker is CpuPlayer)
			{
				CpuPlayer cpuPlayer = (CpuPlayer) Taker;
				foreach (Card card in  cpuPlayer.ChooseCardsForDog(nDog))
				{
					cardsToPutInDog.Add(card);
				}
				PutCardsInDog();
			}	
			else // human player
			{
				startTimer += Time.deltaTime;
				if (startTimer > makeDogTimeLimitSec)
				{
					PutCardsRandomlyInDog();
				}
			}
		}
	}
	
	
	public override void StartProcess()
	{
		base.StartProcess();
		cardsToPutInDog.Clear();
        startTimer = 0f;
		nDog = dog.Items.Count;
        if (Taker.CurrentBid >= Bid.GardeSans)
		{
			FinishProcess();
		}
		else
		{
			TakeDog();
		}
	}
	
	
	public override void FinishProcess()
	{
		startTimer = 0f;
		cardsToPutInDog.Clear();
		base.FinishProcess();
	}
	
	
	private void TakeDog()
	{
        foreach (Card card in dog.Items)
		{
			Taker.Hand.Add(card);
		}
		Taker.SortHand();
        dog.Clear();
    }
	
	
	public void SelectCardToPutInDog(Card card)
	{
		if (cardsToPutInDog.Count >= dog.Count)
		{
			Debug.LogError("Cannot select more cards to put inside the dog.");
		}
		else if (Dog.IsCardAllowedInDog(card, Taker.Hand))
		{
			cardsToPutInDog.Add(card);
		}
		else
		{
			Debug.Log("Cannot put card " + card + "into dog");
		}
	}
	
	
	public void UnSelectCardToPutInDog(Card card)
	{
		if (cardsToPutInDog.Count > 0)
		{
			cardsToPutInDog.Remove(card);
		}
	}
	
	
	public void PutCardsInDog()
	{
		if (cardsToPutInDog.Count == nDog)
		{
			foreach (Card card in cardsToPutInDog.Items)
			{
				Taker.Hand.Remove(card);
				dog.Add(card);
				Debug.Log("Added " + card + " in the dog");
			}
			FinishProcess();
		}
		else
		{
			Debug.Log("You should put " + nDog + " cards in the dog");
		}
	}
	
	
	private void PutCardsRandomlyInDog()
	{
		int nCardToPut = nDog - cardsToPutInDog.Count;
		if (nCardToPut > 0)
		{
			// Choose randomly the rest of the cards to put in the dog
			Debug.Log(nCardToPut + " cards will be put in dog randomly.");
			CardList possibleCards = new CardList();
			foreach (Card card in Taker.Hand)
			{
				if (!cardsToPutInDog.Items.Contains(card) && Dog.IsCardAllowedInDog(card, Taker.Hand))
				{
					possibleCards.Add(card);
				}
			}
			List<int> indexes = Utils.GetRandomUniqueIndexes(nCardToPut, 0, possibleCards.Count-1);
			foreach (int index in indexes)
			{
				cardsToPutInDog.Add(possibleCards[index]);
			}
		}
		PutCardsInDog();
	}
}
