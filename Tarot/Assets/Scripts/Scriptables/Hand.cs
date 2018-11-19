using System;
using System.Collections;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/Hand")]
public class Hand : RuntimeList<Card>
{
	public void Sort()
	{
		// sort in revserse order (strongest card on the left)
		Items.Sort((a,b) => -1 * a.CompareTo(b));
	}
	
	
	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		foreach (Card card in Items)
		{
			sb.AppendLine(card.ToString());
		}
		return sb.ToString();
	}
}