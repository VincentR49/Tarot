using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deal with the annoucement of the Chelem
// Reminder: Appeler la méthode DecideChelemAnnouncement(bool) à partir de la vue dans le cas du joueur humain
public class ChelemAnnouncementManager : ProcessManager
{
    protected override string Name => "Chelem Announcement";
    public ScoringData scoringData;
	public PlayerList players;
	public float answerTimeLimit = 60f;
	
	private Player Taker => players.GetTaker();
	private float answerTimer;
	
	
	private void Update()
	{
		if (status == ProcessState.Running)
        {
			answerTimer += Time.deltaTime;
			if (answerTimer >= answerTimeLimit)
			{
				DecideChelemAnnouncement(false);
			}
		}
	}
	
	
    public override void StartProcess()
    {
        base.StartProcess();
        scoringData.chelemAnnounced = false;
		answerTimer = 0f;
		if (Taker is CpuPlayer)
		{
			CpuPlayer cpuPlayer = (CpuPlayer) Taker;
			DecideChelemAnnouncement (cpuPlayer.AnnounceChelem (players.Count));
		}
    }
	
	
	public void DecideChelemAnnouncement(bool announceChelem)
	{
		scoringData.chelemAnnounced = true;
		answerTimer = 0f;
		FinishProcess();
	}
}