using System;
using UnityEngine;

[CreateAssetMenu(menuName="AIs/Bid AI")]
public class BidAI : ScriptableObject
{
	[Tooltip("0: very secure, 1: willing to take")]
	[Range(0f,1f)]
	public float riskFactor;
	
	private Dictionary<Bid,float> bidTresholds = new Dictionary<Bid,float> // TODO valeurs à fixer...
	{
		{ Bid.Pass, 0f},
		{ Bid.Prise, 1f},
		{ Bid.Garde, 2f},
		{ Bid.GardeSans, 3f},
		{ Bid.GardeContre, 4f}
	};
	
	
	public Bid DecideBid(CardList Hand, int nPlayer, int playerPosition, Bid minBid)
	{
		float handValue = EvaluateHand (Hand, nPlayer);
		float score = (1 + riskFactor) * handValue; // TODO formule à déterminer
		Bid bid = Bid.GardeContre;
		while (bid > minBid)
		{
			if (score >= bidThresholds[bid]) return bid;
			bid--;
		}
		return Bid.Pass;
	}
	
	
	private float EvaluateHand(CardList Hand, int nPlayer)
	{
		if (nPlayer == 3) return EvaluateHand3P (Hand);
		if (nPlayer == 4) return EvaluateHand4P (Hand);
		if (nPlayer == 5) return EvaluateHand5P (Hand);
		return 0f;
	}
	
	
	private float EvaluateHand3P(CardList Hand)
	{
		// TODO
		return 0f;
	}
	
	
	private float EvaluateHand4P(CardList Hand)
	{
		// TODO
		return 0f;
	}
	
	
	private float EvaluateHand5P(CardList Hand)
	{
		// TODO
		return 0f;
	}
}