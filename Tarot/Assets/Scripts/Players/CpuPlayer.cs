using System;
using System.Collections.Generic;
using UnityEngine;
using static RulesConstants;

[CreateAssetMenu(menuName="Scriptable Objects/Cpu Player")]
public class CpuPlayer : Player
{
	public BidAI bidAI;
	public ChooseAllyAI chooseAllyAI;
	public AnnounceChelemAI announceChelemAI;
	public DogAI dogAI;
	public PoigneeAI poigneeAI;
	public PlayAI playAI;
	
	
	public void MakeABid(int nPlayer, int playerPosition, Bid minBid)
	{
		Bid bid = bidAI.DecideBid(Hand, nPlayer, playerPosition, minBid);
        SetBid(bid, minBid);
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
		Card heart = Hand.GetCard (CardType.Heart, minRank);
		Card diamond = Hand.GetCard (CardType.Diamond, minRank);
        Card spade = Hand.GetCard (CardType.Spade, minRank);
		Card club = Hand.GetCard (CardType.Club, minRank);
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
	
	
	public Poignee DecidePoigneeToShow()
	{
		// TODO delegate to AI
		Poignee poignee = Poignee.None;
		int nPossibleCards = Hand.GetNCardOfType(CardType.Trump) + Hand.GetNCardOfType(CardType.Excuse);
		// TODO
		return poignee;
	}
	
	
	public CardList SelectCardsOfPoigneeToShow(int nPlayers)
	{
		// TODO delegate to AI
		// TODO AI de base � d�velopper
		CardList cardsToShow = new CardList();
		if (CurrentPoignee >= Poignee.Single)
		{
			int nCards = GetNCardPoignee(CurrentPoignee, nPlayers);
		}
		return cardsToShow;
	}
	
	
	public bool AnnounceChelem(int nPlayers)
	{
		// TODO delegate to AI and AI � d�velopper
		return false;
	}
}