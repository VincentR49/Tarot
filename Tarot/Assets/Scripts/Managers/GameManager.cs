using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayersGenerator))]
[RequireComponent(typeof(DealManager))]
[RequireComponent(typeof(BidManager))]
public class GameManager : MonoBehaviour
{
	public IntVariable nPlayer;
	public PlayerList players;
	public GamePhaseVariable gamePhase;
	private Dictionary<GamePhase,ProcessManager> gameProcesses;
	private ProcessManager currentProcess;
	
	
    private void Awake()
    {
        ProcessManager playersGenerator = GetComponent<PlayersGenerator>();
		ProcessManager dealManager = GetComponent<DealManager>();
		ProcessManager bidManager = GetComponent<BidManager>();
		
		// Dictionary of process
		gameProcesses = new Dictionary<GamePhase,Process>();
		gameProcesses.Add (GamePhase.PlayerPreparation, playersGenerator);
		gameProcesses.Add (GamePhase.Dealing, dealManager);
		gameProcesses.Add (GamePhase.Bidding, bidManager);
    }

	
    private void Start()
    {
        //NewGame();
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
		Debug.Log("Change Game Phase: " + newPhase);
		gamePhase.Value = newPhase;
		currentProcess = gamePhaseProcess[newPhase];
		if (currentProcess != null)
		{
			currentProcess.StartProcess();
		}
	}
	
	
	private GamePhase GetNextGamePhase()
	{
		switch (gamePhase.Value)
		{
			// à simplifier (créer une classe gérant les ProcessManager)
			case GamePhase.PlayerPreparation: return GamePhase.Dealing;
			case GamePhase.Dealing: return GamePhase.Bidding;
			case GamePhase.Bidding:
				if (players.IsTaker() == null)
				{
					return GamePhase.Dealing;
				}
				else
				{
					return GamePhase.DogMaking;
				}
			default: return GamePhase.None;
		}
	}
}