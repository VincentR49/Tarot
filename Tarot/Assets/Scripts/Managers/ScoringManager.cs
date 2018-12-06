using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Calcul des points à la fin d'une partie
// TODO: GESTION du chelem à faire
// A réécrire en prenant en compte ScoringData...
public class ScoringManager : ProcessManager 
{
    protected override string Name => "Scoring Manager";
	public PlayerList players;
	public Dog dog;
	public ScoringData scoringData;
	private Player Taker => players.GetTaker();
	
	
	public override void StartProcess()
	{
		base.StartProcess();
		// Init
		scoringData.nOudlerTaker = 0;
		scoringData.nPointTaker = 0;
		scoringData.petitWithTaker = false;
		ScanScoringPiles();
		ScanDog();
		// TODO
		ComputeScores();
		FinishProcess();
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
		ScanCardList(dog.Value, Taker.Bid < Bid.GardeContre);
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
	
	
	private void ComputeScores()
	{
		float winerBasePoints = scoringData.GetWinnerBasePoints();
	
	}
	public override void FinishProcess()
	{
        base.FinishProcess();
	}
}
