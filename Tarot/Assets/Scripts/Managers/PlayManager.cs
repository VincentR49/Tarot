using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

// Gestion du système de jeu
// TODO: Petit au bout dans le cas du chelem
public class PlayManager : ProcessManager 
{
	protected override string Name => "Play";
	public PlayerList players;
	public CardVariable allyCard; // for 5 players game
	public CardListVariable playedCard;
    public CardListVariable cardsSelected;
	public ScoringData scoringData;
	[Tooltip("Store data about the cards played in the game")]
	public CardTrackerSet playedCardsRecord;
    public float delayBetweenTurnsSec = 2;

    private float waitForNewTurnTimer = 0;
	private Player Taker => players.GetTaker();
	private Player Dealer => players.GetDealer();
	private Player currentPlayer;
	private Player turnWinner;
	private int turn = 0;
	private int lastTurn = 0;
    private int nCardPlayed = 0;
	private bool giveSmallCardForExcuse = false;
	
	
	void Update()
	{
		if (status == ProcessState.Running)
        {
			if (turn > lastTurn)
			{
				FinishProcess();
			}
			else
			{
				if (playedCard.Count == players.Count) // toutes les cartes ont été jouées
				{
                    ChangeCurrentPlayer(null);
                    waitForNewTurnTimer += Time.deltaTime;
                    if (waitForNewTurnTimer > delayBetweenTurnsSec)
                    {
                        FinishCurrentTurn();
                    } 
				}
				else
                {
                    if (currentPlayer is CpuPlayer)
                    {
                        CpuPlayer cpuPlayer = (CpuPlayer) currentPlayer;
                        Card card = cpuPlayer.SelectCardToPlay (playedCard.Value);
                        PlayCard (currentPlayer, card);
                    }
                    else
                    {
                        if (cardsSelected.Count == 1) // si le joueur humain a sélectionné sa carte à jouer
                        {
                            Card card = cardsSelected.Value[0];
                            PlayCard (currentPlayer, card);
                            cardsSelected.Value.Remove (card);
                        }
                    }
                } 
			}
		}
	}
	
	
	public override void StartProcess()
	{
		base.StartProcess();
		playedCardsRecord.Clear();
		excuseExchangedHasToBeDone = false;
        scoringData.petitAuBout = false;
        scoringData.chelemDone = false;
        SetPlayersTeam();
        lastTurn = players.Items[0].Hand.Count - 1; 
		Player firstPlayer = scoringData.chelemAnnounced ? Taker : players.GetNext(Dealer);
		NewTurn(firstPlayer, true);
	}	
	
	
	public override void FinishProcess()
	{
		bool chelemDone = CheckChelem();
		scoringData.chelemDone = chelemDone;
		scoringData.petitAuBout = CheckPetitAuBout(chelemDone);
		if (giveSmallCardForExcuse && !chelemDone)
		{
			GiveCardToReplaceExcuse();
		}
		base.FinishProcess();
	}

	
	private bool CheckPetitAuBout(bool chelemDone)
	{
		int petitTurn = playedCardsRecord.GetTurn (CardType.Trump, CardType.One);
		if (petitTurn == lastTurn)
		{
			return true;
		}
		if (chelemDone)
		{
			int excuseTurn = playedCardsRecord.GetTurn (CardType.Excuse, CardType.None);
			if (excuseTurn == lastTurn && petitTurn == lastTurn -1)
			{
				return true;
			}
		}
		return false;
	}
	
	
	private void NewTurn(Player startPlayer, bool firstTurn)
	{
        turn = firstTurn ? 0 : turn + 1;
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
        turnWinner = playedCardsRecord.GetPlayer (GetBestCardOfThisTurn());
        PutBoardCardsInWinnerScoringPile();
        Debug.Log("Finished turn " + turn + ". Winner: " + turnWinner.name);
		NewTurn (turnWinner, false);
	}
	
	
	private Card GetBestCardOfThisTurn()
	{
		Card bestCard = playedCard.GetBestCard();
		if (turn == lastTurn) // Gestion du chelem et de l'excuse
		{
			Card excuse = playedCard.GetCard (CardType.Excuse, CardRank.None);
			if (excuse != null)
			{
				Player excusePlayer = playedCardsRecord.GetPlayer (excuse);
				int nOpponentCardWon = GetNumberOfWonCards (GetOpponentTeamIndex (excusePlayer.team));
				if (nOpponentCardWon == 0) // CHELEM
				{
					bestCard = excuse;
				}
			}
		}
		return bestCard;
	}
	

	private void PlayCard(Player player, Card card)
	{
		if (player.CanPlayCard(card, playedCard.Value))
		{	
			Debug.Log(player.name + " played " + card);
			player.Hand.Remove(card);
			playedCard.Add(card);
            playedCardsRecord.Add (new CardTracker (turn, card, player));
            ChangeCurrentPlayer (players.GetNext(player));
			if (players.Count == 5 && card == allyCard.Value)
			{
                Debug.Log("Ally revealed: " + player.name + "!");
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
		CardList winerScoringPile = GetTeamScoringPile(turnWinner.team);
		foreach (Card card in playedCard.Value)
		{   
			if (card.type == CardType.Excuse) // Stays in the excuse possessor team
			{
				int excusePlayerTeam = playedCardsRecord.GetPlayer(card).team;
				GetTeamScoringPile(excusePlayerTeam).Add(card);
				if (excusePlayerTeam != turnWinner.team)
				{
					giveSmallCardForExcuse = true;
				}
			}	
			else
			{
				winerScoringPile.Add(card);
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
		else // 5 players only
		{
			for (int i = 0; i < nPlayers; i++)
			{
				players.Items[i].team = i;
			}
		}
    }
	
	// 5 players only
	private void SetPlayersTeam5Players(Player calledPlayer)
	{
        Debug.Log("Set 5 players teams");
        foreach (Player p in players.Items)
		{
			p.team = (p.IsTaker || p == calledPlayer) ? TAKER_TEAM_INDEX : DEFENDER_TEAM_INDEX;
		}
		JoinPlayersScoringPilesBasedOnTeam();
	}
	
	// 5 players only
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
	
	
	// Donne une carte provenant de l'équipe contenant l'excuse à l'équipe adverse
	// cette carte doit avoir une valeur de 0.5
	// A faire à la fin du jeu (pour être sûr que les équipes ait été révélées)
	private void GiveCardToReplaceExcuse()
	{
		int teamExcuse = GetTeamScoringPile(TAKER_TEAM_INDEX).Contains(CardType.Excuse, CardRank.None) != null ?
							TAKER_TEAM_INDEX : DEFENDER_TEAM_INDEX;
		Card exchangeCard = GetTeamScoringPile(teamExcuse).GetFirstCardByValue(0.5f);
		if (exchangeCard == null)
		{
			Debug.LogError("Cannot give a small card for excuse. Possible?");
		}
		else
		{
			// give the card
			GetTeamScoringPile(teamExcuse).Remove(exchangeCard);
			GetTeamScoringPile(GetOpponentTeamIndex(teamExcuse)).Add(exchangeCard);
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
            return players.GetFirstPlayerByTeam(team);
		}
	}

	
	private CardList GetTeamScoringPile(int team) => GetTeamScoringPilePlayer(team).ScoringPile;
	private int GetNumberOfWonCards(int team) => GetTeamScoringPilePlayer(team).ScoringPile.Count;
	private int GetOpponentTeamIndex(int team) => team == TAKER_TEAM_INDEX ? DEFENDER_TEAM_INDEX : TAKER_TEAM_INDEX;
	private bool CheckChelem() => GetNumberOfWonCards(TAKER_TEAM_INDEX) <= 1 || GetNumberOfWonCards(DEFENDER_TEAM_INDEX) <= 1;
}
