using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/Card Tracker Set")]
public class CardTrackerSet : RunTimeList<CardTracker>
{	
	public Player GetPlayer(Card card)
	{
		foreach (CardTracker tracker in Items)
		{
			if (tracker.card.IsEqualTo(card)) return tracker.player;
		}
		return null;
	}
	
	
	public int GetTurn(CardType type, CardRank rank)
	{
		foreach (CardTracker tracker in Items)
		{
			if (tracker.card.rank == rank && tracker.card.type == type) return card.turn;
		}
		return -1;
	}
}