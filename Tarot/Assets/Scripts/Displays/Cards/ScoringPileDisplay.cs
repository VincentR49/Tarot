using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Gère l'affichage de la pile de cartes gagnées par un joueur
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Text))]
public class ScoringPileDisplay : MonoBehaviour
{
    public Player player;
	public SpriteVariable cardBackSprite;
   
	private Image image;
	private Text text;
	
	
    private void Awake()
    {
       text = GetComponent<Text>();
	   image = GetComponent<Image>();
    }


    private void Update()
    {
		if (player.ScoringPile.Count == 0)
		{
			Hide();
		}
		else
		{
			Show();
		}
    }
		
	public void Hide()
	{
		image.enabled = false;
		text.text = "";
	}
	
	
	public void Show()
	{
		image.enabled = true;
		image.sprite = cardBackSprite.Value;
		text.text = player.ScoringPile.Count;
	}
}