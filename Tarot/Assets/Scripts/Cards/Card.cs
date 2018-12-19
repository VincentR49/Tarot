using System;
using UnityEngine;
using System.ComponentModel; 

[CreateAssetMenu(menuName="Scriptable Objects/Card")]
// Represent a Card Element
public class Card : ScriptableObject, IComparable<Card>
{
	public Sprite sprite;
	public CardType type = CardType.Heart;
	public CardRank rank = CardRank.Dame;

    public float Value
	{
		get
		{
			if (IsOudler()) return 4.5f;
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
	
	
	public override String ToString() =>  rank + " " + type;
	public String ToSimpleString() => rank.GetDescription() + "" + type.GetDescription();
	public bool IsEqualTo(Card other) => rank == other.rank && type == other.type;
	
	
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