using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Player Generic")]
public class Player : ScriptableObject
{
	public new String name = "Player";
	public int score = 0;
	
	// Properties
	public bool IsDealer { get; set; }
	public bool IsTaker { get; get; }
	public CardList Hand { get; }
	public CardList ScoringPile { get; }
	public Bid CurrentBid => bid;
	private Bid bid = Bid.None;
	
	public void PrepareForNewGame()
	{
		score = 0;
		PrepareForNewHand();
	}
	
	
	public void PrepareForNewHand()
	{
		hand = new CardList();
		scoringPile = new CardList();
		IsDealer = false;
		IsTaker = false;
	}
	
	// can save other stats
	// game won / lost etc / nombre de prise, niveau des prises...
	public void SortHand()
	{
		Hand.Sort((a,b) => -1 * a.CompareTo(b));
	}
	
	public void SetBid (Bid bid)
	{
		this.bid = bid;
	}
}