using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Calcul des points à la fin d'une partie
public class ScoringManager : ProcessManager 
{
    protected override string Name => "Scoring Manager";
	
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
