using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestion du système de jeu
public class PlayManager : ProcessManager 
{
	protected override string Name => "Play";
	public PlayerList players;
	public CardListVariable selectedCards;
	
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
			if (turn > nTurnMax)
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
				else
                {
                    if (currentPlayer is CpuPlayer)
                    {
                        CpuPlayer cpuPlayer = (CpuPlayer)currentPlayer;
                        Card card = cpuPlayer.SelectCardToPlay(selectedCards.Value);
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
	}
	
	
	public override void StartProcess()
	{
		base.StartProcess();
		InitPlay();
	}	
	
	
	public override void FinishProcess()
	{
		// TODO
		base.FinishProcess();
	}
	
	
	private void InitPlay()
	{
        Debug.Log("Init play");
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
        Debug.Log("Started Turn " + turn);
    }
	
	
	private void FinishCurrentTurn()
	{
		int bestCardIndex = selectedCards.GetBestCardIndex();
		turnWinner = players.Items[bestCardIndex];
		PutBoardCardsInWinnerScoringPile();
        Debug.Log("Finished turn " + turn + ". Winner: " + turnWinner.name);
	}
	
	
	private void PlayCard(Player player, Card card)
	{
        Debug.Log(player.name + " played " + card);
		player.Hand.Remove(card);
		selectedCards.Add(card);
        currentPlayer = players.GetNext(player);
    }
	
	
	private void PutBoardCardsInWinnerScoringPile()
	{
		// TODO: gérer l'excuse
		foreach (Card card in selectedCards.Value)
		{
            turnWinner.ScoringPile.Add(card);
		}
		selectedCards.Clear();
	}
}
