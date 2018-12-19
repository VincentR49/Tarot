using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CardList : List<Card>
{
    public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		foreach (Card card in this)
		{
            sb.AppendLine(card.ToString());
		}
		return sb.ToString();
	}

	
	public string ToSimpleString()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append (Count + "C: ");
		foreach (Card card in this)
		{
			sb.Append (card.ToSimpleString());
			sb.Append (" ");
		}
		return sb.ToString();
	}
	

    public CardList ShallowCopy()
    {
        CardList copy = new CardList();
        foreach (Card c in this)
            copy.Add(c);
        return copy;
    }
	

	public float GetPoints()
	{
		float points = 0f;
		foreach (Card card in this)
		{
			points += card.Value;
		}
		return points;
	}


    public bool IsEqualTo(CardList other)
    {
        if (other == null) return false;
        if (Count != other.Count) return false;
        for (int i = 0; i < Count; i++)
        {
            if (!this[i].IsEqualTo(other[i]))
            {
                return false;
            }
        }
        return true;
    }

	
    public new bool Contains(Card card)
    {
        foreach (Card c in this)
        {
            if (c.IsEqualTo(card)) return true;
        }
        return false;
    }


    public bool ContainsType(CardType cardType)
    {
        foreach (Card c in this)
        {
            if (c.type == cardType) return true;
        }
        return false;
    }


    public bool ContainsStrongerTrump(CardRank trumpRank)
    {
        foreach (Card c in this)
        {
            if (c.type == CardType.Trump && c.rank > trumpRank) return true;
        }
        return false;
    }


	public Card GetBestCard() => this[GetBestCardIndex()];

	
	// Get the best card, following tarot rule, starting by the first card of the list
	public int GetBestCardIndex()
	{
		int bestIndex = 0;
		// Excuse en premier
		if (this[0].type == CardType.Excuse)
		{
            bestIndex = 1;
		}
		for (int i = bestIndex + 1; i < Count; i++)
		{
			if (this[i].type == this[bestIndex].type)
			{
				if (this[i].rank > this[bestIndex].rank) 
					bestIndex = i;
			}
			else if (this[i].type == CardType.Trump)
				bestIndex = i;
		}
		return bestIndex;
	}
	
	
	public Card GetCard(CardType type, CardRank rank)
	{
		foreach (Card c in this)
        {
            if (c.type == type && c.rank == rank) return c;
        }
		return null;
	}
	
	
	public bool Contains(CardType type, CardRank rank) => GetCard(type, rank) != null;
	
	
	public Card GetFirstCardByValue(float value)
	{
		foreach (Card c in this)
        {
            if (c.Value == value) return c;
        }
		return null;
	}
	
	
	public int GetNOudler()
	{
		int n = 0;
		foreach (Card c in this)
        {
            if (c.IsOudler()) n++;
        }
		return n;
	}
	
	
	public int GetNCardOfType(CardType type)
	{
		int n = 0;
		foreach (Card c in this)
        {
            if (c.type == type) n++;
        }
		return n;
	}
}