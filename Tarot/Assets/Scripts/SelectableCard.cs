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
	public CardListVariable selectedCards;
	public GamePhaseVariable gamePhase;
	public PlayerList players;
	public float shiftPixelYSelection = 10;
	
	private bool selected;
	private Image image;
	private Color originalColor;
    private Card Card => cardDisplay.card;
    private CardDisplay cardDisplay;
	private Vector3 positionNotSelected;
	
	void Awake()
	{
		image = GetComponent<Image>();
        cardDisplay = GetComponent<CardDisplay>();
		originalColor = image.color;
	}

	void Start()
	{
		positionNotSelected = transform.position;
	}
	

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsSelectable())
        {
            SelectionAnimation(!selected);
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsSelectable())
        {
            SelectCard();
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsSelectable())
        {
            SelectionAnimation(selected);
        }
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
		SelectionAnimation(selected);
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

	private void SelectionAnimation(bool selected)
	{
		if (selected)
		{
			transform.position = positionNotSelected + Vector3.up * shiftPixelYSelection;
		}
		else
		{
			transform.position = positionNotSelected;
		}
	}
}
