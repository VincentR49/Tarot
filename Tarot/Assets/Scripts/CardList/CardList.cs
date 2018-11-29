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


    public CardList Clone()
    {
        CardList cardList = new CardList();
        foreach (Card card in this)
        {
            cardList.Add(card.Clone());
        }
        return cardList;
    }

    public new bool Contains(Card card)
    {
        foreach (Card c in this)
        {
            if (c.IsEqualTo(card)) return true;
        }
        return false;
    }
}