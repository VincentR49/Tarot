using System;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/Cpu Player")]
public class CpuPlayer : Player
{
	public BidAI bidAI;
	public DogAI dogAI;
	
	public void MakeABid(Bid minBid)
	{
		// Intelligence artificielle à développer
        /*
		System.Random rnd = new System.Random();
		Bid bid = (Bid) rnd.Next(2, (int)Bid.GardeContre);
		SetBid(bid, minBid);
        */
        SetBid(Bid.Pass);
	}
	
	public CardList ChooseCardsForDog(int nCard)
	{
		// AI à développer
		CardList cards = new CardList();
		for (int i = 0; i < nCard; i++)
		{
			cards.Add(Hand[i]);
		}
		return cards;
	}
}