using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dog")]
public class Dog : RunTimeCardList
{
	public static bool IsCardAllowedInDog(Card card, CardList hand)
	{
		// TODO A préciser (dépend de la main également)
		return !(card.IsOudler() || card.rank == CardRank.Roi || card.type == CardType.Trump);
	}
}