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

	
	public Card GetBestCard() => this[GetBestCardIndex()];

	// Get the best card, following tarot rule, starting by the first card of the list
	public int GetBestCardIndex()
	{
		int bestIndex = 0;
		for (int i = 1; i < Count; i++)
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
}