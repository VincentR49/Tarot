using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

// Gestion du syst�me de jeu
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
				if (playedCard.Count == players.Count) // toutes les cartes ont �t� jou�es
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
                        if (cardsSelected.Count == 1) // si le joueur humain a s�lectionn� sa carte � jouer
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
        NewTurn(players.GetNext(Dealer));
    }

	
	private void NewTurn(Player startPlayer)
	{
        turn += 1;
		turnWinner = null;
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
		cardReceiver = GetScoringPilePlayer(turnWinner.team);
		foreach (Card card in playedCard.Value)
		{   
			cardReceiver.ScoringPile.Add(card);
			if (card.type == CardType.Excuse)
			{
				excuseWinner = turnWinner;
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
		foreach (Player p in players.Items)
		{
			p.team = (p.IsTaker || p == calledPlayer) ? TAKER_TEAM_INDEX : DEFENDER_TEAM_INDEX;
		}
		JoinPlayersScoringPilesBasedOnTeam();
	}
	
	
	private void JoinPlayersScoringPilesBasedOnTeam()
	{
		Player takerReceiver = GetScoringPilePlayer (TAKER_TEAM_INDEX);
		Player defenderReceiver = GetScoringPilePlayer (DEFENDER_TEAM_INDEX);
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
			CardList excuseWinnerPile = GetScoringPilePlayer (excuseWinner.team).ScoringPile;
			CardList excusePlayerPile = GetScoringPilePlayer ((excuseWinner.team + 1) % 2).ScoringPile;
			Card excuse = excuseWinnerPile.GetCard(CardType.Excuse, CardRank.None);
			Card exchangeCard = excusePlayerPile.GetFirstCardByValue(0.5f);
			if (exchangeCard == null)
			{
				Debug.Log("Chelem non g�r�");
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
	
	
	private Player GetScoringPilePlayer(int team)
	{
		// TODO � complexifier (joueur en face?)
		return players.GetFirstPlayerByTeam (team);
	}
}
