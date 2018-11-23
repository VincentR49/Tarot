using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gère le système du chien
// Permet d'échanger ses cartes avec les cartes du chien si le niveau de prise le permet
public class DogMakingManager : ProcessManager 
{
	protected override string Name => "DogMaking";
	public PlayerList players;
	public Dog dog;
	public float makeDogTimeLimitSec = 0; // security (à supprimer)
	
	private float startMakeDogTimer = 0f;
	private Player Taker => players.GetTaker();
	private CardList cardsToPutInDog;
	private int nDogCard;
	private CardList initDogCards; 
	
	private void Update()
	{
		if (status == ProcessState.Running)
		{
			if (Taker is CpuPlayer)
			{
				CpuPlayer cpuPlayer = (CpuPlayer) Taker;
				cardsToPutInDog = cpuPlayer.ChooseCardsForDog(nDogCard);
				PutCardsIntoDog();
			}	
			else // human player
			{
				startMakeDogTimer += Time.deltaTime;
				if (startMakeDogTimer > makeDogTimeLimitSec)
				{
					cardsToPutInDog = initDogCards;
					PutCardsIntoDog();
				}
			}
		}
	}
	
	
	public override void StartProcess()
	{
		base.StartProcess();
		cardsToPutInDog = new CardList();
		nDogCard = dog.Count;
		
		
		if (Taker.CurrentBid >= Bid.GardeSans)
		{
			FinishProcess();
		}
		else
		{
			PutDogIntoTakerHand();
		}
	}
	
	
	private void PutDogIntoTakerHand()
	{
		initDogCards = new CardList();
		foreach (Card card in dog.Items)
		{
			initDogCards.Add(card); // save dog cards
			Taker.Hand.Add(card);
			dog.Remove(card);
		}
		Taker.SortHand();
	}
	
	
	public void SelectCardToPutInDog(Card card)
	{
		if (cardsToPutInDog.Count >= dog.Count)
		{
			Debug.LogError("Cannot select more cards to put inside the dog.");
		}
		else
		{
			cardsToPutInDog.Add(card);
		}
	}
	
	
	public void UnSelectCardToPutInDog(Card card)
	{
		if (cardsToPutInDog.Count > 0)
		{
			cardsToPutInDog.Remove(card);
		}
	}
	
	
	public void PutCardsIntoDog()
	{
		if (cardsToPutInDog.Count == nDogCard)
		{
			// à implémenter
			foreach (Card card in cardsToPutInDog)
			{
				Taker.Hand.Remove(card);
				dog.Add(card);
			}
			FinishProcess();
		}
		else
		{
			Debug.Log("You should put " + nDogCard + " in the dog");
		}
	}
}
