using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class Hand : CardList
{
	public void Sort()
	{
		// sort in revserse order (strongest card on the left)
		Items.Sort((a,b) => -1 * a.CompareTo(b));
	}
}