using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Génère des events pour afficher / masquer le chien
public class DogShowingManager : ProcessManager 
{
	public override string Name => "DogShowing";
	public PlayerList players;
	public GameEvent showDogEvent;
	public GameEvent hideDogEvent;
	public float showDogDurationSec = 5f;
	private float showDogCounter = 0f;

	
	private void Update()
	{
		if (status == ProcessState.Running)
		{
			showDogCounter += Time.DeltaTime;
			if (showDogCounter > showDogDurationSec)
			{
				FinishProcess();
			}
		}
	}
	
	
	public override void StartProcess()
	{
		base.StartProcess();
		showDogCounter = 0f;
		if (players.GetTaker().CurrentBid >= Bid.GardeSans)
		{
			FinishProcess();
		}
		else
		{
			showDogEvent.Raise();
		}
	}
	
	
	public override void FinishProcess();
	{
		base.FinishProcess();
		hideDogEvent.Raise();
	}
}
