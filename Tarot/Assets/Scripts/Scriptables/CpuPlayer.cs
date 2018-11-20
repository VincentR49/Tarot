using System;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/Cpu Player")]
public class CpuPlayer : Player
{
	public override bool IsHuman() => false;
}