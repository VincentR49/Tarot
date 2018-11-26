using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dog")]
public class Dog : RunTimeCardList
{
	public static bool IsCardAllowedInDog(Card card, CardList hand)
	{
		// TODO A pr�ciser (d�pend de la main �galement)
		return !(card.IsOudler() || card.rank == CardRank.Roi || card.type == CardType.Trump);
	}
}