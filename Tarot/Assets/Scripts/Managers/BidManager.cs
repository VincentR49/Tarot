using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gère le système d'enchère
public class BidManager : ProcessManager 
{
	public PlayerList players;
	private Bid maxBid = Bid.None;
	private Player bidder = null;

	
	private void Update()
	{
		if (status == ProcessState.Running)
		{
			if (bidder is CpuPlayer)
			{
				CpuPlayer cpu = (CpuPlayer) bidder;
				cpu.MakeABid(maxBid);
			}	
			else
			{
				bidder.SetBid(Bid.Garde); // temporaire
			}
			Bid currentBid = bidder.CurrentBid;
			if (currentBid != Bid.None)
			{	
				if (currentBid > maxBid)
				{
					maxBid = currentBid;
				}
				if (IsBiddingFinished())
				{
					FinishProcess();
				}
				else
				{
					bidder = players.GetNext(bidder); 
				}
			}
		}
	}
	
	
	public override void StartProcess()
	{
		base.StartProcess();
		ResetBids();
		maxBid = Bid.None;	
		Player dealer = players.GetDealer();
		if (dealer == null)
		{
			Debug.Log("No dealer has been choosen. Bidding canceled");
			return;
		}	
		bidder = players.GetNext(dealer);
	}
	
	
	public override void FinishProcess()
	{
		if (maxBid >= Bid.Prise)
		{
			bidder.IsTaker = true;
		}
		base.FinishProcess();
	}
	
	
	private void ResetBids()
	{
		foreach (Player p in players.Items)
		{
			p.SetBid(Bid.None);
			p.IsTaker = false;
		}
	}

	
	private bool IsBiddingFinished()
	{
		if (maxBid == Bid.GardeContre) return true;
		foreach (Player p in players.Items)
		{
			if (p.CurrentBid == Bid.None) return false;
		}
		return true;
	}
}
