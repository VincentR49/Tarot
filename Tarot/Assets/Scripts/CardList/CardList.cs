using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class CardList : List<Card>
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
}