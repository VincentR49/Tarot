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
}