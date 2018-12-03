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
	public CardListVariable selectedCards;
	public float makeDogTimeLimitSec = 0; // security (à supprimer)
	private Player Taker => players.GetTaker();
    private int nDog;
    private float waitTimer = 0f;

    private void Update()
    {
        if (status == ProcessState.Running)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > makeDogTimeLimitSec)
            {
                PutCardsRandomlyInDog();
            }
        }
    }


    public override void StartProcess()
	{
		base.StartProcess();
		selectedCards.Clear();
        waitTimer = 0f;
        nDog = dog.Count;
        if (Taker.CurrentBid >= Bid.GardeSans)
		{
			FinishProcess();
		}
		else
		{
			PutDogInHand();
            Taker.SortHand();
            MakeDog();
        }
	}
	
	
	public override void FinishProcess()
	{
		selectedCards.Clear();
        Taker.HasToDoSomething = false;
		base.FinishProcess();
	}
	
	
	private void PutDogInHand()
	{
        foreach (Card card in dog.Value)
		{
			Taker.Hand.Add(card);
		}	
        dog.Clear();
    }
	
	
    private void MakeDog()
    {
        Taker.HasToDoSomething = true;
        if (Taker is CpuPlayer)
        {
            MakeCpuDog();
        }
        else
        {
            // human, ne rien faire
        }
    }


    private void MakeCpuDog()
    {
        CpuPlayer cpuPlayer = (CpuPlayer)Taker;
        foreach (Card card in cpuPlayer.ChooseCardsForDog(nDog))
        {
            selectedCards.Add(card);
        }
        PutCardsInDog();
    }


	public void PutCardsInDog()
	{
		if (selectedCards.Count == nDog)
		{
			foreach (Card card in selectedCards.Value)
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
		int nCardToPut = nDog - selectedCards.Count;
		if (nCardToPut > 0)
		{
			// Choose randomly the rest of the cards to put in the dog
			Debug.Log(nCardToPut + " cards will be put in dog randomly.");
			CardList possibleCards = new CardList();
			foreach (Card card in Taker.Hand)
			{
				if (!selectedCards.Contains(card) && Taker.CanPutCardInDog(card))
				{
					possibleCards.Add(card);
				}
			}
			List<int> indexes = Utils.GetRandomUniqueIndexes(nCardToPut, 0, possibleCards.Count-1);
			foreach (int index in indexes)
			{
				selectedCards.Add(possibleCards[index]);
			}
		}
		PutCardsInDog();
	}
}
