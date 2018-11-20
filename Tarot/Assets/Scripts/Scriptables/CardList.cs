using System;
using System.Collections;
using System.Text;
using UnityEngine;

public abstract class CardList : List<Card>
{
	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		foreach (Card card in Items)
		{
			sb.AppendLine(card.ToString());
		}
		return sb.ToString();
	}
	
	
	public float GetPoints()
	{
		float points = 0f;
		foreach (Card card in Items)
		{
			points += card.Value;
		}
		return points;
	}
}