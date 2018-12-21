using System;
using UnityEngine;
using static RulesConstants;

[CreateAssetMenu(menuName="AIs/Choose Ally AI")]
// Determine the ally card called in 5 players game
public class ChooseAllyAI : ScriptableObject
{
	public bool canCallHimSelf = true; // dans le cas d'un très bon jeu
	public CardType defaultType = CardType.Heart;
	
	// TODO à développer
	public Card ChooseAllyCard(CardList hand, Deck standardDeck)
	{
		CardRank minRank = GetMinRankCallable (rank);
		Card heart = hand.GetCard (CardType.Heart, minRank);
		Card diamond = hand.GetCard (CardType.Diamond, minRank);
        Card spade = hand.GetCard (CardType.Spade, minRank);
		Card club = hand.GetCard (CardType.Club, minRank);
		if (heart == null)
		{
			return standardDeck.GetCard(CardType.Heart, minRank);
		}
		else if (diamond == null)
		{
			return standardDeck.GetCard(CardType.Diamond, minRank);
		}
		else if (spade == null)
		{
			return standardDeck.GetCard(CardType.Spade, minRank);
		}
		else if (club == null)
		{
			return standardDeck.GetCard(CardType.Club, minRank);
		}
		else
		{
			return standardDeck.GetCard(defaultType, minRank);
		}
	}	
}