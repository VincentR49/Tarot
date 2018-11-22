using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Player Generic")]
public abstract class Player : ScriptableObject
{
	public new String name = "Player";
	public int score = 0;
	public bool IsDealer { get; set; )
	public Hand Hand { get; }
	private ScoringPile scoringPile;
	
	// think about some stats
	
	public void PrepareForNewGame()
	{
		score = 0;
		PrepareForNewHand();
	}
	
	
	public void PrepareForNewHand()
	{
		hand = new Hand();
		scoringPile = new ScoringPile();
		IsDealer = false;
	}
	
	// can save other stats
	// game won / lost etc / nombre de prise, niveau des prises...
}