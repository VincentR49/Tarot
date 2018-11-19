using System;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/Card")]
public class Card : ScriptableObject, IComparable<Card> 
{
	public Sprite sprite;
	public CardType type = CardType.Heart;
	public CardRank rank = CardRank.Queen;
	public float Value
	{
		get
		{
			if (IsOudler())
			{
				return 4.5f;
			}
			else
			{
				switch (rank)
				{
					case CardRank.King: return 4.5f;
					case CardRank.Queen: return 3.5f;
					case CardRank.Knight: return 2.5f;
					case CardRank.Jack: return 1.5f;
					default: return 0.5f;
				}
			}
		}
	}
	
	
	public override String ToString()
	{
		 return rank + " " + type;
	}
	
	
	public bool IsOudler()
	{
		return type == CardType.Excuse 
				|| ((type == CardType.Trump) 
					&& (rank == CardRank.One || rank == CardRank.TwentyOne));
	}
	
	
	public int CompareTo(Card other)
	{
		if (type != other.type)
		{
			return type.CompareTo(other.type);
		}
		return rank.CompareTo(other.rank);
	}
}