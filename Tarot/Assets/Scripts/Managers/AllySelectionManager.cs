using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// S�lection du joueur alli� au taker
// Pour le jeu � 5 uniquement
public class AllySelectionManager : ProcessManager 
{
    protected override string Name => "Ally Selection";
	public Card calledCard;
	
	
	private void Update()
	{
		if (status == ProcessState.Running)
		{
			
		}
	}
	
	
	public override void StartProcess()
	{
		base.StartProcess();
	}

	
	public override void FinishProcess()
	{
        base.FinishProcess();
	}
}
