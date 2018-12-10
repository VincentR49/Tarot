using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RulesConstants;

// Ask each player if they want to show a poignee or not
// The showing poignee process is done in another manager (PoigneeShowingManager)
public class PoigneeDecidingManager : ProcessManager 
{
    protected override string Name => "Decide Poignee";
	public ScoringData scoringData;
	public PlayerList players;
	public PoigneeVariable humanPlayerPoignee; // link to the view
	public float answerTimeLimit = 60f; // security
	
	private int NPlayer => players.Count;
	private float answerTimer = 0f;
	private int playerChecked = -1;
	private Player currentPlayer;
	private bool firstPoigneeShown = false;

	
	private void Update()
	{
		if (status == ProcessState.Running)
        {
			AskPoigneeOfCurrentPlayer();
		}
	}
	

	public override void StartProcess()
	{
		base.StartProcess();
        Init();
	}
	
	
	public override void FinishProcess()
	{
		if (currentPlayer != null)
		{
			currentPlayer.HasToDoSomething = false;
			currentPlayer = null;
		}
		base.FinishProcess();
	}
	
	
	private void Init()
	{
		humanPlayerPoignee.Value = Poignee.NotDecided;
		scoringData.poignee1 = Poignee.NotDecided;
        scoringData.poignee2 = Poignee.NotDecided;
		playerChecked = -1;
		currentPlayer = null;
		firstPoigneeShown = false;
		GoToNextPlayer();
	}

	
	private void GoToNextPlayer()
	{
		if (currentPlayer == null)
		{
			currentPlayer = players.Items[0];
		}
		else
		{
			currentPlayer.HasToDoSomething = false;
			currentPlayer = players.GetNext(currentPlayer);
		}
		currentPlayer.HasToDoSomething = true;
		currentPlayer.CurrentPoignee = Poignee.NotDecided;
		answerTimer = 0f;
		playerChecked += 1;
		if (playerChecked == NPlayer)
		{
			FinishProcess();
		}
	}
	

	private void AskPoigneeOfCurrentPlayer()
	{
		if (currentPlayer is CpuPlayer)
		{
			CpuPlayer cpuPlayer = (CpuPlayer) currentPlayer;
			Poignee poignee = cpuPlayer.DecidePoigneeToShow();
			SetPoigneeForCurrentPlayer (poignee);
		}
		else // human player
		{
			Poignee maxPoignee = GetMaxPoignee (currentPlayer.Hand, NPlayer);
			if (maxPoignee == Poignee.None)
			{
				SetPoigneeForCurrentPlayer (Poignee.None);
			}
			else
			{
				answerTimer += Time.deltaTime;
				if (answerTimer >= answerTimeLimit)
				{
					SetPoigneeForCurrentPlayer (Poignee.None);
				}
				if (humanPlayerPoignee.Value != Poignee.NotDecided)
				{
					SetPoigneeForCurrentPlayer (humanPlayerPoignee.Value);
				}
			}
		}
	}
	
	
	public void SetPoigneeForCurrentPlayer(Poignee poignee)
	{
		currentPlayer.CurrentPoignee = poignee;
		if (poignee >= Poignee.Single) // update scoring data
		{
			if (!firstPoigneeShown)
			{
				scoringData.poignee1 = poignee;
				firstPoigneeShown = true
			}
			else // seconde poignée (max)
			{
				scoringData.poignee2 = poignee;
			}
		}
		GotToNextPlayer();
	}
}
