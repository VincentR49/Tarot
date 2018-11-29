using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Player Generic")]
public class Player : ScriptableObject
{
	public new String name = "Player";
	public int score = 0;
	public int teamIndex;

	public bool IsDealer { get; set; }
	public bool IsTaker { get; set; }
	public CardList Hand { get; set; }
	public CardList ScoringPile { get; set; }
	public Bid CurrentBid => bid;
	
	private Bid bid = Bid.None;
	
	public void PrepareForNewGame()
	{
		score = 0;
		PrepareForNewHand();
	}
	
	
	public void PrepareForNewHand()
	{
		Hand = new CardList();
		ScoringPile = new CardList();
		IsDealer = false;
		IsTaker = false;
	}
	
	// can save other stats
	// game won / lost etc / nombre de prise, niveau des prises...
	public void SortHand()
	{
		Hand.Sort((a,b) => -1 * a.CompareTo(b));
	}
	
	
	public void SetBid(Bid bid)
	{
		this.bid = bid;
	}

	
	public void SetBid(Bid bid, Bid minBid)
	{
		if (bid <= minBid && bid > Bid.Pass)
		{
			Debug.Log("Cannot make bid inferior or egal to the min bid");
			SetBid(Bid.Pass);
		}
		else
		{
			SetBid(bid);
		}
	}
}