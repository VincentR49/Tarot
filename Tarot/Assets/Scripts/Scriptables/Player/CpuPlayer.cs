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
	
	public Card SelectCardToPlay(List<Card> cardsOnBoard)
	{
		// A d�velopper
		// Dans un premier temps prendre une carte au hasard dans la main
		// Plus tard, dire quelles cartes sont autoris�es
		System.Random rnd = new System.Random();
		return Hand[rnd.Next(0, Hand.Count - 1)];
	}
}