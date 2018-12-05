using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Gère l'affichage de la pile de cartes gagnées par un joueur
public class ScoringPileDisplay : MonoBehaviour
{
	public SpriteVariable cardBackSprite;
	public Image image;
	public Text text;
    private Player player;

    private void Update()
    {
        if (player != null)
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
        text.text = player.ScoringPile.Count.ToString();
	}


    public void SetPlayer(Player player)
    {
        this.player = player;
    }
}