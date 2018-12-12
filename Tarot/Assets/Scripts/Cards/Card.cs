using System;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/Card")]
public class Card : ScriptableObject, IComparable<Card>
{
	public Sprite sprite;
	public CardType type = CardType.Heart;
	public CardRank rank = CardRank.Dame;

    //public int Id => ((int)rank + 1) * 100 + ((int)type);
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
					case CardRank.Roi: return 4.5f;
					case CardRank.Dame: return 3.5f;
					case CardRank.Cavalier: return 2.5f;
					case CardRank.Valet: return 1.5f;
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

    public bool IsEqualTo(Card other) => card.rank == other.rank && card.type == other.type;
}