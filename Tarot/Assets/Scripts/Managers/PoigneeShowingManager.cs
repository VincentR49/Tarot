using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RulesConstants;

// Show the current
public class PoigneeShowingManager : ProcessManager 
{
    protected override string Name => "Show Poignee";
	public CardListVariable selectedCards;
	public CardListVariable poigneeToShow;
	public PlayerList players;
	public float decidePoigneeLimitTime = 60f;
	public float showPoigneeDurationSec = 10f;
	
	private float showTimer = 0f;
	private float decideTimer = 0f;
	private Player currentPlayer;
	private bool showPoignee = false;
	private int NPlayer => players.Count;
    private int playerChecked = 0;
	
	private void Update()
	{
		if (status == ProcessState.Running)
        {
			if (showPoignee)
			{
				ShowCurrentPoignee();
			}
			else
			{
                ScanForPoigneeToShow();
			}
		}
	}
	

	public override void StartProcess()
	{
		base.StartProcess();
        currentPlayer = null;
		GoToNextPlayer();
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
		showTimer = 0f;
		decideTimer = 0f;
		poigneeToShow.Clear();
		selectedCards.Clear();
		showPoignee = false;
		playerChecked += 1;
		if (playerChecked == NPlayer)
		{
			FinishProcess();
		}
	}
	
	
	private void ShowCurrentPoignee()
	{
		showTimer += Time.deltaTime;
		if (showTimer >= showPoigneeDurationSec)
		{
			GoToNextPlayer();
		}
	}
	
	
	// Check for each player the cards of their hand to show
	private void ScanForPoigneeToShow()
	{
		if (currentPlayer.CurrentPoignee <= Poignee.None)
		{
			GoToNextPlayer();
		}
		else
		{
			if (currentPlayer is CpuPlayer)
			{
				CpuPlayer cpuPlayer = (CpuPlayer) currentPlayer;
				SetPoigneeToShow (cpuPlayer.SelectCardsOfPoigneeToShow (NPlayer));
			}
			else // human player
			{
				decideTimer += Time.deltaTime;
				int nCardToShow = GetNCardPoignee (currentPlayer.CurrentPoignee, NPlayer);
				if (decideTimer >= decidePoigneeLimitTime)
				{
					SetPoigneeToShow (CompletePoigneeWithRandomCards (selectedCards.Value, nCardToShow));
				}
				else if (selectedCards.Count == nCardToShow)
				{
					SetPoigneeToShow(selectedCards.Value);
				}
			}
		}
	}
	
	
	// Set the cards that have to be shown
	private void SetPoigneeToShow(CardList poignee)
	{
		poigneeToShow.Value = poignee;
		showPoignee = true;
        Debug.Log("Player " + currentPlayer + " is showing the following cards: " + poignee.ToString());
	}
	
	
	private CardList CompletePoigneeWithRandomCards(CardList unCompletePoignee, int nCards)
	{
		// TODO
		return unCompletePoignee;
	}
}
