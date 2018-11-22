using System;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/Cpu Player")]
public class CpuPlayer : Player
{
	public BidAI bidAI;

	public void MakeABid(Bid minBid)
	{
		// � d�velopper en fonction de l'IA
		SetBid(Bid.Pass); 
	}
}