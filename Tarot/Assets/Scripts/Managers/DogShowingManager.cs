using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Génère des events pour afficher / masquer le chien
public class DogShowingManager : ProcessManager 
{
	protected override string Name => "DogShowing";
	public PlayerList players;
	public GameEvent showDogEvent;
	public GameEvent hideDogEvent;
	public float showDogDurationSec = 5f;


	public override void StartProcess()
	{
		base.StartProcess();
		if (players.GetTaker().CurrentBid >= Bid.GardeSans)
		{
			FinishProcess();
		}
		else
		{
            StartCoroutine(ShowAndHideDog(showDogDurationSec));
		}
	}
	
	
	public override void FinishProcess()
	{
		base.FinishProcess();
		hideDogEvent.Raise();
	}


    private IEnumerator ShowAndHideDog(float waitTime)
    {
        showDogEvent.Raise();
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            FinishProcess();
            break;
        }
    }
}
