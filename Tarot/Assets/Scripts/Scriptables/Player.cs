using System;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/Player")]
public class Player : ScriptableObject
{
	public String name = "Player";
	public int score = 0;
	public Hand hand;
	
	// can save other stats
	// game won / lost etc / nombre de prise, niveau des prises...
}