using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayersGenerator))]
[RequireComponent(typeof(DealManager))]
[RequireComponent(typeof(BidManager))]
[RequireComponent(typeof(AllySelectionManager))]
[RequireComponent(typeof(DogMakingManager))]
[RequireComponent(typeof(DogShowingManager))]
[RequireComponent(typeof(ChelemAnnouncementManager))]
[RequireComponent(typeof(PoigneeDecidingManager))]
[RequireComponent(typeof(PoigneeShowingManager))]
[RequireComponent(typeof(PlayManager))]
[RequireComponent(typeof(ScoringManager))]
// General Game gestion
// Manage the game processes and their temporal succession
public class GameManager : MonoBehaviour
{
	public IntVariable nPlayer;
	public PlayerList players;
	public GamePhaseVariable gamePhase;
	private Dictionary<GamePhase,ProcessManager> gameProcesses;
	private ProcessManager currentProcess;

	
    private void Awake()
    {
        gameProcesses = new Dictionary<GamePhase, ProcessManager>
        {
            { GamePhase.PlayerPreparation, GetComponent<PlayersGenerator>() },
            { GamePhase.Dealing, GetComponent<DealManager>() },
            { GamePhase.Bidding, GetComponent<BidManager>() },
			{ GamePhase.AllySelection, GetComponent<AllySelectionManager>() },
			{ GamePhase.DogShowing, GetComponent<DogShowingManager>() },
			{ GamePhase.DogMaking, GetComponent<DogMakingManager>() },
			{ GamePhase.ChelemAnnouncement, GetComponent<ChelemAnnouncementManager>() },
			{ GamePhase.PoigneeDeciding, GetComponent<PoigneeDecidingManager>() },
			{ GamePhase.PoigneeShowing, GetComponent<PoigneeShowingManager>() },
			{ GamePhase.Play, GetComponent<PlayManager>() },
			{ GamePhase.Scoring, GetComponent<ScoringManager>() },
        };
    }

	
    private void Start()
    {
        InitGame();
    }

	
    private void InitGame()
    {
        ChangeGamePhase(GamePhase.None);
    }

	
	private void Update()
	{
		if (currentProcess != null 
				&& currentProcess.GetStatus() == ProcessState.Finished)
		{
			ChangeGamePhase (GetNextGamePhase());
		}
	}
	
	
    public void NewGame(int nPlayer)
    {
        this.nPlayer.Value = nPlayer;
        NewGame();
    }

	
    public void NewGame()
	{
        if (nPlayer.Value < 3 || nPlayer.Value > 5)
        {
            Debug.Log("Wrong Players number: should be 3, 4 or 5");
            return;
        }
		ChangeGamePhase(GamePhase.PlayerPreparation);
	}
	
	
	public void ChangeGamePhase(GamePhase newPhase)
	{
		gamePhase.Value = newPhase;
		if (currentProcess != null)
		{
			currentProcess.ResetProcess();
			currentProcess = null;
		}
		if (gameProcesses.TryGetValue(newPhase, out currentProcess))
		{
			currentProcess.StartProcess();
		}
	}
	
	
	private GamePhase GetNextGamePhase()
	{
		switch (gamePhase.Value)
		{
			case GamePhase.PlayerPreparation: return GamePhase.Dealing;
			case GamePhase.Dealing: return GamePhase.Bidding;
			case GamePhase.Bidding:
				if (players.GetTaker() == null)
				{
					return GamePhase.Dealing;
				}
				else
				{
					return nPlayer.Value == 5 ? GamePhase.AllySelection : GamePhase.DogShowing;
				}
			case GamePhase.AllySelection: return GamePhase.DogShowing;
			case GamePhase.DogShowing: return GamePhase.DogMaking;
			case GamePhase.DogMaking: return GamePhase.ChelemAnnouncement;
			case GamePhase.ChelemAnnouncement: return GamePhase.PoigneeDeciding;
			case GamePhase.PoigneeDeciding: return GamePhase.PoigneeShowing;
			case GamePhase.PoigneeShowing: return GamePhase.Play;
			case GamePhase.Play: return GamePhase.Scoring;
			//case GamePhase.Scoring: return GamePhase.Dealing;
			default: return GamePhase.None;
		}
	}
}