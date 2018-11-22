using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe générique pour la gestion des process du jeu
public abstract class ProcessManager : MonoBehaviour 
{
	protected ProcessState status = ProcessState.NotStarted;
	public ProcessState GetStatus() => status;
	public virtual void StartProcess()
	{
		status = ProcessState.Running;
	}
	public virtual void FinishProcess()
	{
		status = ProcessState.Finished;
	}
	public virtual void InitProcess()
	{
		status = ProcessState.NotStarted;
	}
}