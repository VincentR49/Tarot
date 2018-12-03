using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dog")]
public class Dog : CardListVariable
{
    public GameEvent dogEditedEvent;

    public new void Add(Card card)
    {
        base.Add(card);
        dogEditedEvent.Raise();
    }

    public new void Remove(Card card)
    {
        base.Remove(card);
        dogEditedEvent.Raise();
    }

    public new void Clear()
    {
        base.Clear();
        dogEditedEvent.Raise();
    }
	

	public static int GetNumberOfCards(int nPlayer) => (nPlayer <= 4) ? 6 : 3;
}