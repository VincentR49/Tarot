using System;
using UnityEngine;

public abstract class Player : ScriptableObject
{
	public String name = "Player";
	public int score = 0;
	private Hand hand;
	private ScoringPile scoringPile;
	
	public void PrepareForNewHand()
	{
		hand = new Hand();
		scoringPile = new ScoringPile();
	}

	public abstract bool IsHuman();
	// can save other stats
	// game won / lost etc / nombre de prise, niveau des prises...
}