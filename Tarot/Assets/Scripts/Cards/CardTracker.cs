using System;
using UnityEngine;

// Store the card information
public class CardTracker
{
	public int turn;
	public Card card;
	public Player player;

	public CardTracker(int turn, Card card, Player player)
	{
		this.turn = turn;
		this.card = card;
		this.player = player;
	}
}