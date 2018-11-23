using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe générique pour la gestion des process du jeu
public abstract class ProcessManager : MonoBehaviour 
{
	public abstract string Name;
	protected ProcessState status = ProcessState.NotStarted;
	public ProcessState GetStatus() => status;
	
	public virtual void StartProcess()
	{
		Debug.Log("Start process: " + Name);
		status = ProcessState.Running;
	}
	
	public virtual void FinishProcess()
	{
		Debug.Log("Finish process: " + Name);
		status = ProcessState.Finished;
	}
	
	public virtual void ResetProcess()
	{
		Debug.Log("ReinitProcess process: " + Name);
		status = ProcessState.NotStarted;
	}
}