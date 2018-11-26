using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gère le système d'enchère
// A simplifier ...
public class BidManager : ProcessManager 
{
    protected override string Name => "Bidding";
	public PlayerList players;
    [Tooltip("Event raised when the human player has to bid")]
	public GameEvent humanPlayerReadyToBid;
	[Tooltip("Event raised when the human player cannot bid anymore")]
	public GameEvent humanPlayerBidAborted;
    public float limitAnswerTimeSec = 10;

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
            else // human player
            {
                timeSinceBidBegin += Time.deltaTime;
                if (timeSinceBidBegin > limitAnswerTimeSec)
                {
                    bidder.SetBid (Bid.Pass);
					humanPlayerBidAborted.Raise();
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
		if (bidder is HumanPlayer)
		{
			humanPlayerReadyToBid.Raise();
		}
    }
}
