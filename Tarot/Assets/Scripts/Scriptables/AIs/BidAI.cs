using System;
using UnityEngine;

[CreateAssetMenu(menuName="AIs/Bid AI")]
// AI to decide which bid to Make
public class BidAI : ScriptableObject
{
	[Tooltip("0: very secure, 0.5: normal, 1: willing to take")]
	[Range(0f,1f)]
	public float riskFactor 0.5f;
	
	// Valeurs fixées pour le jeu à 4, risque à 0.5
	// Ajustement pour jeu à 3 et 5 via des coeffs
	// A ajuster
	private static Dictionary<Bid,float> bidThresholds = new Dictionary<Bid,float> 
	{
		{ Bid.Pass, 0f },
		{ Bid.Prise, 2.4f }, // proche de la moyenne, jeu à 4, risque à 0.5
		{ Bid.Garde, 3.3f }, 
		{ Bid.GardeSans, 4.2f },
		{ Bid.GardeContre, 4.8f }
	};
	
	// à Ajuster
	private const float riskFactorWeight = 0.5f; 
	private const float coeff3Players = 1.15f; 
	private const float coeff5Players = 1.4f:
	private const float bonusPosition = 0f;
	private const float bonusLongue = 0f;
	private const float bonusMarriage = 0f;
	
	// OK à garder
	private const float trumpValue = 2f;
	private const float oudlerValue = 9.5f;
	private const float twoOudlersBonus = 5f;

	
	public Bid DecideBid(CardList hand, int nPlayer, int playerPosition)
	{
		float handValue = EvaluateHand (hand, nPlayer);
		float score = (1 + riskFactor * riskFactorWeight) * handValue + bonusPosition; // TODO formule à déterminer
		Debug.Log("hand value: " + handValue + " / Score: " + score);
		Bid bid = Bid.GardeContre;
		while (bid > Bid.Pass)
		{
			if (score >= bidThresholds[bid]) return bid;
			bid--;
		}
		return Bid.Pass;
	}
	
	
	private float EvaluateHand(CardList hand, int nPlayer)
	{
		float handValue = GetHandCardsValue(hand);
		handValue /= GetNHandCards(nPlayer); // normalize
		if (nPlayer == 5) handValue *= coeff5Players;
		if (nPlayer == 3) handValue *= coeff3Players;
		return handValue;
	}
	
	
	private float GetHandCardsValue(CardList hand)
	{
		float total = 0f;
		int nOudlers = 0;
		foreach (Card card in hand)
		{
			if (card.IsOudler())
			{
				total += oudlerValue;
				nOudlers++;
				if (nOudlers == 2)
				{
					total += twoOudlersBonus;
				}
			}
			else if (card.type == CardType.Trump)
			{
				total += trumpValue;
			}
			else
			{
				total += card.Value;
			}
		}
		return total;
	}

	private int GetNHandCards(int nPlayer) => (Deck.NCardsTarot - Dog.GetNumberOfCards(nPlayer)) / nPlayer;
}