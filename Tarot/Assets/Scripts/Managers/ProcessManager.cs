using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe générique pour la gestion des process du jeu
public abstract class ProcessManager : MonoBehaviour 
{
	private ProcessState status = ProcessState.NotStarted;
	public ProcessState GetStatus() => status;
	public void StartProcess()
	{
		status = ProcessState.Running;
	}
	public void FinishProcess()
	{
		status = ProcessState.Finished;
	}
	public void InitProcess()
	{
		status = ProcessState.NotStarted;
	}
}