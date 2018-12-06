using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deal with showing the Poignee
public class PoigneeShowingManager : ProcessManager 
{
    protected override string Name => "Show Poignée";

	
	public override void StartProcess()
	{
		base.StartProcess();
		// TODO
		FinishProcess(); 
	}
	
	
	public override void FinishProcess()
	{
		base.FinishProcess();
	}
}
