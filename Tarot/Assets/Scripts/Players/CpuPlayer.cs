using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/Cpu Player")]
public class CpuPlayer : Player
{
	public BidAI bidAI;
	public DogAI dogAI;
	public PlayAI playAI;
	public ChooseAllyAI chooseAllAI;
	
	public void MakeABid(Bid minBid)
	{
		// Intelligence artificielle � d�velopper
        /*
		System.Random rnd = new System.Random();
		Bid bid = (Bid) rnd.Next(2, (int)Bid.GardeContre);
		SetBid(bid, minBid);
        */
		// TODO delegate to AI
        SetBid(Bid.Pass);
	}
	
	public CardList ChooseCardsForDog(int nCard)
	{
		// AI � d�velopper
		// TODO delegate to AI
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
		// TODO delegate to AI
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
	
	
	public Card SelectCalledCard(Deck standardDeck)
	{
		// TODO delegate to AI
		CardRank minRank = GetMinRankCallable();
		Card heart = Hand.GetCard(minRank, CardType.Heart);
		Card diamond = Hand.GetCard(minRank, CardType.Diamond);
		Card spade = Hand.GetCard(minRank, CardType.Spade);
		Card club = Hand.GetCard(minRank, CardType.Club);
		if (heart == null)
		{
			return standardDeck.GetCard(CardType.Heart, minRank);
		}
		else if (diamond != null)
		{
			return standardDeck.GetCard(CardType.Diamond, minRank);
		}
		else if (spade != null)
		{
			return standardDeck.GetCard(CardType.Spade, minRank);
		}
		else if (club != null)
		{
			return standardDeck.GetCard(CardType.Club, minRank);
		}
		else
		{
			return chooseAllyAI.defaultCard;
		}
	}
}