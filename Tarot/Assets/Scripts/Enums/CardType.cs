using System.Collections;
using System.Collections.Generic;
using System.ComponentModel; 
using UnityEngine;

// Liste des types de carte (par ordre croissant de valeur)
public enum CardType
{
	[Description("C")]
	Club,
	[Description("D")]
	Diamond,
	[Description("H")]
	Heart,
	[Description("S")]
	Spade,
	[Description("T")]
	Trump,
	[Description("E")]
	Excuse
}