using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Calcul des points à la fin d'une partie
public class ScoringManager : ProcessManager 
{
    protected override string Name => "Scoring Manager";
	public PlayerList players;
	public Dog dog;
	private Player Taker => players.GetTaker();
	private float takerPoints;
	private float defensePoints;
	
	
	public override void StartProcess()
	{
		base.StartProcess();
		takerPoints = GetTakerPoints();
		defensePoints = GetDefensePoints();
		Debug.Log("Taker made " + takerPoints + " points.");
		Debug.Log("Defense made " + defensePoints + " points.");
		ComputeScores();
		FinishProcess();
	}

	
	private float GetTakerPoints()
	{
		float points = 0;
		foreach (Player p in players.Items)
		{
			if (p.team != TAKER_TEAM_INDEX) continue;
			points += p.ScoringPile.GetPoints();
		}
		if (Taker.Bid < Bid.GardeContre)
		{
			points += dog.GetPoints();
		}
		return points;
	}

	
	private float GetDefensePoints()
	{
		float points = 0;
		foreach (Player p in players.Items)
		{
			if (p.team == TAKER_TEAM_INDEX) continue;
			points += p.ScoringPile.GetPoints();
		}
		if (Taker.Bid == Bid.GardeContre)
		{
			points += dog.GetPoints();
		}
		return points;
	}
	
	
	private void ComputeScores()
	{
		// TODO
	}
	

	public override void FinishProcess()
	{
        base.FinishProcess();
	}
}
