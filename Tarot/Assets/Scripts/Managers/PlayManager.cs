using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestion du système de jeu
public class PlayManager : ProcessManager 
{
	protected override string Name => "Play";
	public PlayerList players;
	public CardListVariable playedCard;
    public CardListVariable cardsSelected;
    public float delayBetweenTurnsSec = 2;

    private float waitForNewTurnTimer = 0;
	private Player Taker => players.GetTaker();
	private Player Dealer => players.GetDealer();
	private Player currentPlayer;
	private Player turnWinner;
	private int turn = 0;
	private int nTurnMax = 0;
    private int nCardPlayed = 0;
    private Dictionary<Card, Player> playedCardDict;

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
				if (playedCard.Count == players.Count) // toutes les cartes ont été jouées
				{
                    waitForNewTurnTimer += Time.deltaTime;
                    if (waitForNewTurnTimer > delayBetweenTurnsSec)
                    {
                        FinishCurrentTurn();
                        NewTurn(turnWinner);
                    } 
				}
				else
                {
                    if (currentPlayer is CpuPlayer)
                    {
                        CpuPlayer cpuPlayer = (CpuPlayer) currentPlayer;
                        Card card = cpuPlayer.SelectCardToPlay(playedCard.Value);
                        PlayCard(currentPlayer, card);
                    }
                    else
                    {
                        if (cardsSelected.Count == 1)
                        {
                            Card card = cardsSelected.Value[0];
                            PlayCard(currentPlayer, card);
                            cardsSelected.Value.Remove(card);
                        }
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
		ChangeCurrentPlayer(null);
		base.FinishProcess();
	}
	
	
	private void InitPlay()
	{
        Debug.Log("Init play");
        playedCardDict = new Dictionary<Card, Player>();
        turn = -1;
        turnWinner = null;
        nTurnMax = players.Items[0].Hand.Count - 1;
        NewTurn(players.GetNext(Dealer));
    }

	
	private void NewTurn(Player startPlayer)
	{
        turn += 1;
        nCardPlayed = 0;
        waitForNewTurnTimer = 0;
        playedCard.Clear();
        cardsSelected.Clear();
        ChangeCurrentPlayer (startPlayer);
        Debug.Log("Started Turn " + turn);
    }
	
	
	private void FinishCurrentTurn()
	{
        Card bestCard = playedCard.GetBestCard();
        turnWinner = playedCardDict[bestCard];
        PutBoardCardsInWinnerScoringPile();
        Debug.Log("Finished turn " + turn + ". Winner: " + turnWinner.name);
	}
	


	
	private void PlayCard(Player player, Card card)
	{
		if (player.CanPlayCard(card, playedCard.Value))
		{	
			Debug.Log(player.name + " played " + card);
			player.Hand.Remove(card);
			playedCard.Add(card);
            playedCardDict.Add(card, player);
            ChangeCurrentPlayer (players.GetNext(player));
            nCardPlayed++;
        }
        else
		{
			Debug.LogError(player.name + " cannot play " + card);
		}
    }
	
	
	private void PutBoardCardsInWinnerScoringPile()
	{
		// TODO: gérer l'excuse
		foreach (Card card in playedCard.Value)
		{
            turnWinner.ScoringPile.Add(card);
		}
		playedCard.Clear();
	}
	
	
	public void ChangeCurrentPlayer(Player currentPlayer)
	{
		if (this.currentPlayer != null)
		{
			this.currentPlayer.HasToDoSomething = false;
		}
		this.currentPlayer = currentPlayer;
		if (this.currentPlayer != null)
		{
			this.currentPlayer.HasToDoSomething = true;
		}
	}


    private void SetPlayersTeam()
    {

    }
}
