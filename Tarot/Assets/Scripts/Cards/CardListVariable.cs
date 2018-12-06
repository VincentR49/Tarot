using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Runtime Card List")]
public class CardListVariable : ScriptableObject
{
    public CardList Value = new CardList();
	public int Count => Value.Count;
	public bool Contains(Card card) => Value.Contains(card);
	public Card GetBestCard() => Value.GetBestCard();
	public int GetBestCardIndex() => Value.GetBestCardIndex();
	public float GetPoints() => Value.GetPoints();
	
	public void Add(Card card)
	{
		Value.Add(card);
	}
	
	public void Remove(Card card)
	{
		Value.Remove(card);
	}
	
	public void Clear()
	{
		Value.Clear();
	}
}