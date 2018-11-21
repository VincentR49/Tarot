using System;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/Player List")]
public class PlayerList : RuntimeList<Player>
{
	public HumanPlayer GetFirstHumanPlayer()
	{
		foreach (Player p in Items)
		{
			if (p is HumanPlayer) return p;
		}
		return null;
	}
	
	// Return the player index in the list
	// Return -1 if the player is not in the list
	public int GetPlayerIndex (Player p)
	{
		for (int i = 0; i < Count; i++)
		{
			if (p == Items[i]) return i;
		}
		return -1;
	}
	
	
	public Player GetNext(Player p)
	{
		int index = GetPlayerIndex(p);
		if (index == -1)
		{
			return null;
		}
		else
		{
			index += 1;
			return (index >= Count) ? Items[0] : Items[index];
		}
	}
}