using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

// Gestion du système de jeu
public class PlayManager : ProcessManager 
{
	protected override string Name => "Play";
	public PlayerList players;
	public Card calledCard; // for 5 players game
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
	private Player excusePlayer;
	private Player excuseWinner;
	
	
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
                        if (cardsSelected.Count == 1) // si le joueur humain a sélectionné sa carte à jouer
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
		ExchangeExcuse();
		ChangeCurrentPlayer(null);
		base.FinishProcess();
	}
	
	
	private void InitPlay()
	{
        Debug.Log("Init play");
        playedCardDict = new Dictionary<Card, Player>();
		excusePlayer = null;
		excuseWinner = null;
        turn = -1;
        nTurnMax = players.Items[0].Hand.Count - 1;
        SetPlayersTeam();
        NewTurn(players.GetNext(Dealer));
    }

	
	private void NewTurn(Player startPlayer)
	{
        turn += 1;
		turnWinner = null;
        nCardPlayed = 0;
        waitForNewTurnTimer = 0;
		foreach (Player p in players.Items)
		{
			p.PrepareForNewTurn();
		}
        playedCard.Clear();
        cardsSelected.Clear();
		startPlayer.IsFirstThisTurn = true;
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
			if (card.type == CardType.Excuse)
			{
				excusePlayer = player;
			}
			if (players.Count == 5 && card == calledCard)
			{
				SetPlayersTeam5Players(player);
			}
            nCardPlayed++;
        }
        else
		{
			Debug.LogError(player.name + " cannot play " + card);
		}
    }
	
	
	private void PutBoardCardsInWinnerScoringPile()
	{
		Player cardReceiver = GetTeamScoringPilePlayer(turnWinner.team);
		foreach (Card card in playedCard.Value)
		{   
			cardReceiver.ScoringPile.Add(card);
			if (card.type == CardType.Excuse)
			{
				excuseWinner = cardReceiver;
			}	
		}
		playedCard.Clear();
	}
	
	
	public void ChangeCurrentPlayer(Player player)
	{
		if (currentPlayer != null)
		{
			currentPlayer.HasToDoSomething = false;
		}
		currentPlayer = player;
		if (currentPlayer != null)
		{
			currentPlayer.HasToDoSomething = true;
		}
	}


    private void SetPlayersTeam()
    {
		int nPlayers = players.Count;
        Debug.Log("Set players teams");
		if (nPlayers != 5)
		{
			foreach (Player p in players.Items)
			{
				p.team = p.IsTaker ? TAKER_TEAM_INDEX : DEFENDER_TEAM_INDEX;
			}
		}
		else
		{
			for (int i = 0; i < nPlayers; i++)
			{
				players.Items[i].team = i;
			}
		}
    }
	
	
	private void SetPlayersTeam5Players(Player calledPlayer)
	{
        Debug.Log("Set 5 players teams");
        foreach (Player p in players.Items)
		{
			p.team = (p.IsTaker || p == calledPlayer) ? TAKER_TEAM_INDEX : DEFENDER_TEAM_INDEX;
		}
		JoinPlayersScoringPilesBasedOnTeam();
	}
	
	
	private void JoinPlayersScoringPilesBasedOnTeam()
	{
		Player takerReceiver = GetTeamScoringPilePlayer (TAKER_TEAM_INDEX);
		Player defenderReceiver = GetTeamScoringPilePlayer (DEFENDER_TEAM_INDEX);
		foreach (Player p in players.Items)
		{
			if (p.team == TAKER_TEAM_INDEX && p != takerReceiver)
			{
				foreach (Card card in p.ScoringPile)
				{
					takerReceiver.ScoringPile.Add(card);
				}
				p.ScoringPile.Clear();
			}
			else if (p.team == DEFENDER_TEAM_INDEX && p != defenderReceiver)
			{
				foreach (Card card in p.ScoringPile)
				{
					defenderReceiver.ScoringPile.Add(card);
				}
				p.ScoringPile.Clear();
			}
		}
	}
	
	
	// Echange l'excuse avec une carte de valeur 0.5 dans les piles
	private void ExchangeExcuse()
	{
		if (excuseWinner != null && excusePlayer != null)
		{
			CardList excuseWinnerPile = GetTeamScoringPilePlayer (excuseWinner.team).ScoringPile;
			CardList excusePlayerPile = GetTeamScoringPilePlayer ((excuseWinner.team + 1) % 2).ScoringPile;
			Card excuse = excuseWinnerPile.GetCard(CardType.Excuse, CardRank.None);
			Card exchangeCard = excusePlayerPile.GetFirstCardByValue(0.5f);
			if (exchangeCard == null)
			{
				Debug.Log("Chelem non géré");
			}
			else
			{
				// Echange excuse / 0.5 card
				excuseWinnerPile.Remove(excuse);
				excuseWinnerPile.Add(exchangeCard);
				excusePlayerPile.Remove(exchangeCard);	
				excusePlayerPile.Add(excuse);
			}
		}
		else
		{
			Debug.LogError("No excuse player and excuse winner");
		}
	}
	
	
	private Player GetTeamScoringPilePlayer(int team)
	{
		if (team == TAKER_TEAM_INDEX)
		{
			return Taker;
		}
		else
		{
			// TODO à complexifier (joueur en face?)
			return players.GetFirstPlayerOfTeam (team);
		}
	}
}
