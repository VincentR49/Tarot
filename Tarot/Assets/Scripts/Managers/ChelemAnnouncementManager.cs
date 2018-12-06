using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deal with the annoucement of the Chelem
public class ChelemAnnouncementManager : ProcessManager
{
    protected override string Name => "Chelem Announcement";
    public ScoringData scoringData;

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