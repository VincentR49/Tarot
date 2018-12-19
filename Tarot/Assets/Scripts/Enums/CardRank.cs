using System.Collections;
using System.Collections.Generic;
using System.ComponentModel; 
using UnityEngine;

// Liste des ranks de carte (par ordre croissant de valeur)
public enum CardRank
{
	[Description("")]
	None, // excuse
	[Description("1")]
	One,
	[Description("2")]
	Two,
	[Description("3")]
	Three,
	[Description("4")]
	Four,
	[Description("5")]
	Five,
	[Description("6")]
	Six,
	[Description("7")]
	Seven,
	[Description("8")]
	Eight,
	[Description("9")]
	Nine,
	[Description("10")]
	Ten, 
	[Description("11")]
	Eleven,
	[Description("12")]
	Twelve,
	[Description("13")]
	Thirtheen,
	[Description("14")]
	Fourteen,
	[Description("15")]
	Fifteen,
	[Description("16")]
	Sixteen,
	[Description("17")]
	Seventeen,
	[Description("18")]
	Eighteen,
	[Description("19")]
	Nineteen,
	[Description("20")]
	Twenty,
	[Description("21")]
	TwentyOne,
	[Description("V")]
	Valet,
	[Description("C")]
	Cavalier,
	[Description("D")]
	Dame,
	[Description("R")]
	Roi
}