using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

// Calcul des points à la fin d'une partie
// TODO: GESTION du chelem à faire
public class ScoringManager : ProcessManager 
{
    protected override string Name => "Scoring Manager";
	public PlayerList players;
	public Dog dog;
	public ScoringData scoringData;
	
	private Player Taker => players.GetTaker();
	private Bid bid => Taker.CurrentBid;
	private List<float> playerScores;
	
	public override void StartProcess()
	{
		base.StartProcess();
        InitScoringData();
		ScanScoringPiles();
		ScanDog();
        ComputeScores();
		DistributeScores();
		FinishProcess();
	}
	
	
    private void InitScoringData()
    {
        scoringData.nOudlerTaker = 0;
        scoringData.takerPoints = 0;
        scoringData.petitWithTaker = false;
        scoringData.bid = bid;
    }


	private void ScanScoringPiles()
	{
		foreach (Player p in players.Items)
		{
			ScanCardList(p.ScoringPile, p.team == TAKER_TEAM_INDEX);
		}
	}
	
	
	private void ScanDog()
	{
		ScanCardList(dog.Value, bid < Bid.GardeContre);
	}
	
	
	private void ScanCardList(CardList cardList, bool belongToTaker)
	{
		foreach (Card card in cardList)
		{
			if (belongToTaker)
			{
				scoringData.takerPoints += card.Value;
				scoringData.nOudlerTaker += card.IsOudler() ? 1 : 0;
			}
			if (card.type == CardType.Trump && card.rank == CardRank.One)
			{
				scoringData.petitWithTaker = belongToTaker;
			}
		}
	}
	
	
	private int GetNDefenders()
	{
		int n = 0;
		foreach (Player p in players.Items)
		{
			if (p.team == DEFENDER_TEAM_INDEX) n++;
		}
		return n;
	}
	
	// Fill the list playerScores
	private void ComputeScores()
	{
		float defendersBasePoint = scoringData.GetDefendersBasePoint();
		scoringData.PrintSummary();
        Debug.Log("TakerBase points of " + takerBasePoint);
		int nPlayer = players.Count;
		int nDefenders = GetNDefenders();
		List<float> playerScores = new List<float>();
		foreach (Player p in players.Items)
		{
			float score = 0;
			if (p.team == DEFENDER_TEAM_INDEX)
			{
				score = defendersBasePoint;
			}
			else // taker team
			{
				if (nPlayer == 5 && nDefenders == 3) // in the case the main player has an ally
				{
					score =  (p == Taker) ? - 2 * defendersBasePoint : - defendersBasePoint;
				}
				else
				{
					score = - nDefenders * defendersBasePoint;
				}
			}
			playerScores.Add (score);
		}
		
		// Check the total points (should be 0)
		float total = 0;
		foreach (float s in playerScore) 
			total += s;
		Debug.Log("Total score: " + total + " (should be 0)");
	}
	
	// Add the game score the current score of the player
	private void DistributeScores()
	{
		for (int i = 0; i < players.Count; i++)
		{
			Player player = players.Items[i];
			player.score += playerScores[i];
			Debug.Log("Player " + player.name + " received " + playerScores[i] + " points.");
		}
	}
}
