using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/Cpu Player")]
public class CpuPlayer : Player
{
	public BidAI bidAI;
	public DogAI dogAI;
	public PlayAI playAI;
	
	public void MakeABid(Bid minBid)
	{
		// Intelligence artificielle � d�velopper
        /*
		System.Random rnd = new System.Random();
		Bid bid = (Bid) rnd.Next(2, (int)Bid.GardeContre);
		SetBid(bid, minBid);
        */
        SetBid(Bid.Pass);
	}
	
	public CardList ChooseCardsForDog(int nCard)
	{
		// AI � d�velopper
		CardList cards = new CardList();
		foreach (Card card in Hand)
		{
			if (CanPutCardInDog(card))
			{
				cards.Add(card);
				if (cards.Count == nCard)
				{
					break;
				}
			}
		}
		return cards;
	}
	
	public Card SelectCardToPlay(CardList cardsOnBoard)
	{
		// AI d�velopper
		// Pour l'instant joue la premi�re carte disponnible
		foreach (Card card in Hand)
		{
			if (CanPlayCard (card, cardsOnBoard))
			{
				return card;
			}
		}
		Debug.LogError("Aucune carte ne peut �tre jou�e par le cpu");
		return null; // ne doit pas arriver l�
	}
}