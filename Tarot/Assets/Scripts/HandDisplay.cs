using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// G�re l'affichage d'une main
public class HandDisplay : MonoBehaviour
{
	public Hand hand;
	private bool flipped;

	
	private void Awake()
	{
		// R�cup�ration des components
	}
	
	
	private void UpdateDisplay()
	{
		//
	}
	
	
	public void SetFlipped(bool flipped)
	{
		this.flipped = flipped;
		UpdateDisplay();
	}
}