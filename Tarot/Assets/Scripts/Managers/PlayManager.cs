using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestion du système de jeu
public class PlayManager : ProcessManager 
{
	protected override string Name => "Play";
	public PlayerList players;
	public Dog dog;
	public RunTimeCardList selectedCards;
	
	private Player Taker => players.GetTaker();
	private Player Dealer => players.GetDealer();
	private Player currentPlayer;
	private Player turnWinner;
	private int turn = 0;
	private int nTurnMax = 0;
	
	
	void Update()
	{
		if (status == ProcessState.Running)
        {
			if (turn >= nTurnMax)
			{
				FinishProcess();
			}
			else
			{
				if (selectedCards.Count == players.Count) // toutes les cartes ont été jouées
				{
					FinishCurrentTurn();
					NewTurn();
				}
				if (currentPlayer is CpuPlayer)
				{
					CpuPlayer cpuPlayer = (CpuPlayer) currentPlayer;
					Card card = cpuPlayer.SelectCardToPlay(selectedCards.Items);
					PlayCard(cpuPlayer, card);
				}
				else
				{
					// humanPlayer (ne rien faire pour le moment)
					// Carte au hasard
					PlayCard(currentPlayer, currentPlayer.Hand[0]);
				}
			}
		}
	}
	
	
	public override void StartProcess()
	{
		base.StartProcess();
		InitPlay();
	}	
	
	
	public override void FinishProcess()
	{
		// TODO
		// Gestion du chien ici
		base.FinishProcess();
	}
	
	
	private void InitPlay()
	{
		turn = 0;
		nTurnMax = players.Items[0].Hand.Count - 1;
		currentPlayer = players.GetNext(Dealer);
		selectedCards.Clear();
		turnWinner = null;		
	}

	
	private void NewTurn()
	{
		turn += 1;
		currentPlayer = turnWinner;
		selectedCards.Clear();
	}
	
	
	private void FinishCurrentTurn()
	{
		// Check all the cards and decice the winner
		turnWinner = null; // TODO à décider
	}
	
	
	private void PlayCard(Player player, Card card)
	{
		player.Hand.Remove(card);
		selectedCards.Add(card);
	}
}
