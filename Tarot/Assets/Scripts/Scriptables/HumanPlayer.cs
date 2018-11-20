using System;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/Human Player")]
public class HumanPlayer : Player
{
	public override bool IsHuman() => true;
}