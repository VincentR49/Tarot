using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class Hand : CardList
{
	public new void Sort()
	{
		// sort in revserse order (strongest card on the left)
		Sort((a,b) => -1 * a.CompareTo(b));
	}
}