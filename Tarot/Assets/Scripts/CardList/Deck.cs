using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/Deck")]
public class Deck : RuntimeStack<Card> 
{
    public int NCard => Items.Count;

	public void Shuffle()
	{
		System.Random rnd = new System.Random();
		List<Card> tempCards = new List<Card>();
		while (Items.Count != 0)
		{
            tempCards.Add(Pop());
		}
		int n = tempCards.Count;
		while (n != 0)
		{
			Card card = tempCards[rnd.Next(0,n-1)];
			Push(card);
			tempCards.Remove(card);
			n--;
		}
	}

    public Card Take()
	{
		if (Items.Count == 0) return null;
		return Pop();
	}
}