using System;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/Cpu Player")]
public class CpuPlayer : Player
{
	public BidAI bidAI;

	public void MakeABid(Bid minBid)
	{
        // à développer en fonction de l'IA
        if (minBid == Bid.GardeContre)
        {
            SetBid(Bid.None);
        }
        else
        {
            System.Random rnd = new System.Random();
            Bid bid = (Bid) rnd.Next(2, (int)Bid.GardeContre);
            SetBid(bid > minBid ? bid : Bid.Pass);
        } 
	}
}