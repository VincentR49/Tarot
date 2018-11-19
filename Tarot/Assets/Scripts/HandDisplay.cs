using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Gère l'affichage d'une main
public class HandDisplay : MonoBehaviour
{
	public Hand hand;
	private bool flipped;

	
	private void Awake()
	{
		// Récupération des components
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