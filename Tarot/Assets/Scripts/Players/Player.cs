using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Player Generic")]
public class Player : ScriptableObject
{
	public new String name = "Player";
	public int score = 0;
	public int teamIndex;

	public bool HasToDoSomething { get; set; }
	public bool IsDealer { get; set; }
	public bool IsTaker { get; set; }
	public CardList Hand { get; set; }
	public CardList ScoringPile { get; set; }
	public Bid CurrentBid => bid;
	
	private Bid bid = Bid.None;
	private List<int> pastScores = new List<int>();
	
	
	public void PrepareForNewGame()
	{
		score = 0;
		PrepareForNewHand();
	}
	
	public void PrepareForNewHand()
	{
		Hand = new CardList();
		ScoringPile = new CardList();
		IsDealer = false;
		IsTaker = false;
        HasToDoSomething = false;
    }

	// can save other stats
	// game won / lost etc / nombre de prise, niveau des prises...
	public void SortHand()
	{
		Hand.Sort((a,b) => -1 * a.CompareTo(b));
	}
	
	
	public void SetBid(Bid bid)
	{
		this.bid = bid;
	}

	
	public void SetBid(Bid bid, Bid minBid)
	{
		if (bid <= minBid && bid > Bid.Pass)
		{
			Debug.Log("Cannot make bid inferior or egal to the min bid");
			SetBid(Bid.Pass);
		}
		else
		{
			SetBid(bid);
		}
	}


    public bool CanPutCardInDog(Card card)
    {
        // A développer, dépend aussi de la main
        return !(card.IsOudler() || card.rank == CardRank.Roi || card.type == CardType.Trump);
    }


    public bool CanPlayCard(Card card, CardList cardsOnBoard) // à simplifier
    {
		if (!Hand.Contains(card)) return false;
        if (cardsOnBoard.Count == 0)  return true;
        CardType firstCardType = cardsOnBoard[0].type;
        if (firstCardType == CardType.Excuse || card.type == CardType.Excuse)
        {
            return true;
        }
        else if (firstCardType == card.type)
        {
            if (firstCardType == CardType.Trump)
            {
                Card bestTrump = cardsOnBoard.GetBestCard();
                if (card.rank > bestTrump.rank)
                {
                    return true;
                }
                else // only if the player cannot play higher card
                {
                    return !Hand.ContainsStrongerTrump(bestTrump.rank);
                }
            }
            return true;
        }
        else  // different type
        {
            if (Hand.ContainsType(firstCardType))
            {
                return false;
            }
            else // cannot play the same type
            {
                if (firstCardType == CardType.Trump)
                {
                    return true; // can play any card
                }
                else if (Hand.ContainsType(CardType.Trump))
                {
                    if (card.type != CardType.Trump)
                    {
                        return false;
                    }
                    else // trump card 
                    {
                        // La carte est un atout.
                        // La première carte n'est pas atout et on ne peut pas jouer cette couleur
                        // Notre main contient des atouts
                        // Si la carte joué est un atout inférieur à l'atout le plus fort de la liste
                        // S'assurer que le jouer ne possède pas d'atout plus fort dans son jeu
                        Card bestCard = cardsOnBoard.GetBestCard();
                        if (bestCard.type == CardType.Trump 
                            && card.rank < bestCard.rank
                            && Hand.ContainsStrongerTrump(bestCard.rank))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}