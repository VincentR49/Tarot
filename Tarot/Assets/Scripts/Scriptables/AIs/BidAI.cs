using System;
using UnityEngine;

[CreateAssetMenu(menuName="AIs/Bid AI")]
// AI to decide which bid to Make
// Based on Noël Chavel algorithm
// Evalutation for 4 players
// Adjustements have to be made for 3 and 5 players games

// Addition of risk factor to add some personalities to AI
public class BidAI : ScriptableObject
{
	[Tooltip("0: very secure, 0.5: normal, 1: willing to take")]
	[Range(0f,1f)]
	public float riskFactor 0.5f;

	// Add more parameters?
	
	private static Dictionary<Bid,float> bidThresholds = new Dictionary<Bid,float> 
	{
		{ Bid.Pass, 0f },
		{ Bid.Prise, 40f }, 
		{ Bid.Garde, 56f }, 
		{ Bid.GardeSans, 71f },
		{ Bid.GardeContre, 80f }
	};
	
	// Points bonus
	private static Dictionary<CardRank, float> rankBonus = new Dictionary<CardRank,float>
	{
		{ CardRank.TwentyOne, 10 },
		{ CardRank.None, 9 + 2 }, // excuse, 9 + bonus trump 2 
		{ CardRank.Roi, 6 },
		{ CardRank.Dame, 3 },
		{ CardRank.Cavalier, 2 },
		{ CardRank.Valet, 1}
	};

	// Bonus for Petit (petit included)
	private static Dictionary<int, float> petitBonusByTrumps = new Dictionary<int, float>
	{
		{ 0, 0 },
		{ 1, 0 },
		{ 2, 0 },
		{ 3, 0 },
		{ 4, 0 }
		{ 5, 5 },
		{ 6, 7 },
		{ 7, 9 }
	};
		
	// Trumps bonus
	private const float trumpBonusMoreThan4 = 2;
	private const float majorTrump = 2; // 16 to 21
	private const float majorTrumpSequence = 1; // ex: 16,17,18 = 3 points, 20,21 = 2 points
	private const float trumpBonus = 2;
	
	// Other Bonuses
	private const float longue5 = 6;
	private const float longue6 = 7;
	private const float longue7 = 9;
	private const float wedding = 1;
	
	// Bonus garde contre / sans only
	private const float coupeBonus = 6; 
	private	const float singletonBonus = 3;
	
	
	public Bid DecideBid(CardList hand, int nPlayer, int playerPosition)
	{
		float handValue = EvaluateHand (hand, nPlayer);
		float score = (1 + riskFactor * riskFactorWeight) * handValue + bonusPosition; // TODO formule à déterminer
		float coupeSingletonBonus = GetCoupeSingletonBonus (hand);
		Debug.Log("hand value: " + handValue + " / Score: " + score);
		Bid bid = Bid.GardeContre;
		while (bid > Bid.Pass)
		{
			float actualScore = score;
			if (bid >= Bid.GardeSans)
			{
				actualScore += coupeSingletonBonus;
			}
			if (actualScore >= bidThresholds[bid])
			{
				return bid;
			}
			bid--;
		}
		return Bid.Pass;
	}
	
	
	private float EvaluateHand(CardList hand)
	{
		CardList cards = hand.ShallowCopy();
		cards.Sort((a,b) => -1 * a.CompareTo(b));
		float total = 0f;
		int nTrump = hand.GetNCardOfType (CardType.Trump);
		bool hasRoi = false;
		CardType currentType = null;
		CardRank previousCard = null;
		foreach (Card card in cards)
		{
			if (currentType != card.type)
			{
				hasRoi = false;
				currentType = card.type;
			}
			if (rankBonus.TryGetValue(card.rank, out float bonusRankValue))
			{
				total += bonusRankValue;
			}		
			if (card.rank == CardRank.Roi)
			{
				hasRoi = true;
			}
			if (hasRoi && card.rank == CardRank.Dame)
			{
				total += wedding;
			}
			if (card.type == CardType.Trump) 
			{
				total += GetTrumpBonus (card.rank, previousCard, nTrump);
			}
			if (card.type == CardType.Trump && card.rank == CardRank.One)
			{
				total += GetPetitBonus (nTrump);
			}	
			previousCard = card;
		}
		total += GetLongueBonus(cards);
		return total;
	}
	
	
	private float GetPetitBonus(nTrump)
	{
		if (petitBonusByTrumps.TryGetValue(nTrump, out float bonusPetit))
		{
			return bonusPetit;
		}
		else // bonus max
		{
			return petitBonusByTrumps[6];
		}
	}
	
	
	private float GetTrumpBonus(CardRank rank, Card previousCard, int nTrump)
	{
		float bonus = 0f;
		if (nTrump >= 4)
		{
			bonus += trumpBonus;
		}
		if (rank >= CardRank.Sixteen)
		{
			bonus += majorTrump;
			if (previousCard != null 
					&& previousCard.type == CardType.Trump 
					&& (previousCard.rank - card.rank == 1))
			{
				bonus += majorTrumpSequence;
			}
		}
		return bonus;
	}
	

	private float GetLongueBonus(CardList cards)
	{
		float total = 0f;
		total += GetLongueBonus (CardType.Heart, cards);
		total += GetLongueBonus (CardType.Club, cards);
		total += GetLongueBonus (CardType.Spade, cards);
		total += GetLongueBonus (CardType.Diamond, cards);
		return total;
	}
	
	
	private float GetLongueBonus(CardType type, CardList cards)
	{
		int nCards = cards.GetNCardOfType (type);
		if (nCards >= 7) return longue7;
		if (nCards == 6)  return longue6;
		if (nCards == 5) return longue5;
		return 0f;
	}
	
	
	private float GetCoupeSingletonBonus(CardList cards)
	{
		float total = 0f;
		total += GetCoupeSingletonBonus (CardType.Heart, cards);
		total += GetCoupeSingletonBonus (CardType.Club, cards);
		total += GetCoupeSingletonBonus (CardType.Spade, cards);
		total += GetCoupeSingletonBonus (CardType.Diamond, cards);
		return total;
	}
	
	
	private float GetCoupeSingletonBonus(CardType type, CardList cards)
	{
		int n = cards.GetNCardOfType (type);
		if (n == 0) return coupeBonus;
		if (n == 1) return singletonBonus;
		return 0f;
	}
}