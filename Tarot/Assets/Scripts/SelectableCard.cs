using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Gère la sélection / déselection d'une carte
// Complète la liste liée
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CardDisplay))]
public class SelectableCard : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
	public RunTimeCardList selectedCards;
	public GamePhaseVariable gamePhase;
	public PlayerList players;

	private bool selected;
	private Image image;
	private Color originalColor;
    private Card Card => cardDisplay.card;
    private CardDisplay cardDisplay;
	
	void Awake()
	{
		image = GetComponent<Image>();
        cardDisplay = GetComponent<CardDisplay>();
		originalColor = image.color;
	}


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsSelectable())
        {
            image.color = Color.cyan;
        }
        Debug.Log("On mouse over");
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsSelectable())
        {
            SelectCard();
        }
        Debug.Log("On mouse down");
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsSelectable())
        {
            image.color = originalColor;
        }
        Debug.Log("On mouse exit");
    }


    private void SelectCard()
	{
		selected = !selected;
		if (selected)
		{
            Debug.Log("Selected card " + Card);
			selectedCards.Add(Card);
		}
		else
		{
            Debug.Log("Unselected card " + Card);
            selectedCards.Remove(Card);
		}
	}
	
	// Définit si la carte peut être sélectionnée
	// Dépend de divers paramètres (mode de jeu etc.)
	private bool IsSelectable()
	{	
		Player humanPlayer = players.GetFirstHumanPlayer();
		if (humanPlayer == null) return false;
		if (!humanPlayer.Hand.Contains(Card)) return false;
		
		if (gamePhase.Value == GamePhase.DogMaking)
		{
			if (selectedCards.Count >= Dog.GetNumberOfCards(players.Count))
			{
				return false;
			}
			else if (Dog.IsCardAllowedInDog(Card, humanPlayer.Hand))
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
