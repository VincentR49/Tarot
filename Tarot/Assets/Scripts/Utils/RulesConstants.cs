using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Other constants based on the game's rules
public static class RulesConstants
{
	// Poignées
    private static Dictionary<Poignee,int> limitPoignee3Players = new Dictionary<Poignee,int>
	{
		{ Poignee.Single, 13 },
		{ Poignee.Double, 15 },
		{ Poignee.Triple, 18 }
	};
	
	private static Dictionary<Poignee,int> limitPoignee4Players = new Dictionary<Poignee,int>
	{
		{ Poignee.Single, 10 },
		{ Poignee.Double, 13 },
		{ Poignee.Triple, 15 }
	};
	
	private static Dictionary<Poignee,int> limitPoignee5Players = new Dictionary<Poignee,int>
	{
		{ Poignee.Single, 8  },
		{ Poignee.Double, 10 },
		{ Poignee.Triple, 13 }
	};
	
	
	public static int GetNCardPoignee(Poignee poignee, int nPlayer)
	{
		Dictionary<Poignee, int> dict;
		if (nPlayer == 3)
			dict = limitPoignee3Players;
		else if (nPlayer == 4)
			dict = limitPoignee4Players;
		else if (nPlayer == 5)
			dict = limitPoignee5Players;
		else
		{
			Debug.LogError("Number of player incorrect: " + nPlayer);
            return -1;
		}
		return dict[poignee];
	}
	
	
	public static Poignee GetMaxPoignee(CardList hand, int nPlayer)
	{
		int nTrump = hand.GetNCardOfType(CardType.Trump) + hand.GetNCardOfType(CardType.Excuse);
		Poignee maxPoignee = Poignee.Triple;
		while (maxPoignee > Poignee.None)
		{
			if (nTrump >= GetNCardPoignee(maxPoignee, nPlayer))
				return maxPoignee;
            maxPoignee--;
		}
		return Poignee.None;
	}
	
	
	public static CardRank GetMinRankCallable(CardList hand)
	{
		CardRank rank = CardRank.Roi;
		while (rank >= CardRank.Valet)
		{
			Card heart = hand.GetCard (CardType.Heart, rank);
			Card diamond = hand.GetCard (CardType.Diamond, rank);
			Card spade = handGetCard (CardType.Spade, rank);
			Card club = hand.GetCard (CardType.Club, rank);
			if (heart == null || diamond == null || spade == null || club == null)
			{
				break;
			}		
			rank--;
		}
		return (CardRank) rank;
	}
}