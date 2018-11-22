using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gère le système d'enchère
public class BidManager : ProcessManager 
{
	public PlayerList players;
    [Tooltip("Event raised when the human player has to bid")]
    public float limitAnswerTimeSec = 5;
	private Bid maxBid = Bid.None;
	private Player bidder = null;
    private float timeSinceBidBegin = 0f;

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
                timeSinceBidBegin += Time.deltaTime;
                if (timeSinceBidBegin > limitAnswerTimeSec)
                {
                    bidder.SetBid (Bid.Pass);
                }
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
                    SelectBidder (players.GetNext(bidder));
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
        SelectBidder (players.GetNext(dealer));
	}
	
	
	public override void FinishProcess()
	{
		if (maxBid >= Bid.Prise)
		{
			foreach (Player p in players.Items)
            {
                if (p.CurrentBid == maxBid)
                {
                    p.IsTaker = true;
                    Debug.Log("Player " + p.name + " is taker.");
                    break;
                }
            }
		}
        Debug.Log("Bidding finished.");
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


    private void SelectBidder(Player newBidder)
    {
        bidder = newBidder;
        timeSinceBidBegin = 0f;
    }
}
