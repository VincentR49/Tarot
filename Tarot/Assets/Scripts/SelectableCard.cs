using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Gère la sélection / déselection d'une carte
// Complète la liste liée
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CardDisplay)]
public class SelectableCard : MonoBehaviour 
{
	public RunTimeCardList selectedCards;
	public GamePhaseVariable gamePhase;
	public PlayerList players;

	private bool selected;
	private Image image;
	private Color originalColor;
	private Card card;
	
	
	void Awake()
	{
		image = GetComponent<Image>();
		originalColor = image.color;
		card = GetComponent<CardDisplay>().card;
	}
	
	
	void OnMouseOver()
	{
		if (IsSelectable())
		{
			image.color = cyan;
			Debug.Log("On mouse over");
		}
	}

	
	void OnMouseExit()
	{
		if (IsSelectable())
		{
			image.color = originalColor;
			Debug.Log("On mouse exit");
		}
	}

	
	void OnMouseDown()
	{
		if (IsSelectable())
		{
			SelectCard();
		}
	}
	
	
	private void SelectCard()
	{
		selected = !selected;
		if (selected)
		{
			selectedCards.Add(card);
		}
		else
		{
			selectedCards.Remove(card);
		}
	}
	
	// Définit si la carte peut être sélectionnée
	// Dépend de divers paramètres (mode de jeu etc.)
	private bool IsSelectable()
	{	
		Player humanPlayer = players.GetFirstHumanPlayer();
		if (humanPlayer == null) return false;
		if (!humanPlayer.Hand.Contains(card)) return false;
		
		if (gamePhase.Value == GamePhase.DogMaking)
		{
			if (selectedCards.Count >= dog.Count)
			{
				return false;
			}
			else if (Dog.IsCardAllowedInDog(card, humanPlayer.Hand))
			{
				return true;
			}
			return false;
		}	
		
		if (gamePhase.Value == GamePhase.Play)
		{
			// à définir
		}
		return false;
	}
}
