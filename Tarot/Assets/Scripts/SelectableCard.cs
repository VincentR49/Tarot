using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Gère la sélection / déselection d'une carte
// Complète la liste liée
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CardDisplay))]
public class SelectableCard : MonoBehaviour, IPointerClickHandler
{
	public CardListVariable selectedCards;
    public CardListVariable playedCards;
	public GamePhaseVariable gamePhase;
	public PlayerList players;
    public Color selectableColor;
    public Color selectedColor;
	
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


    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsSelectable() || selected)
        {
            SelectCard();
        }
    }


    private void Update()
    {
        SetSelectableEffect(IsSelectable());
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
	private bool IsSelectable()
	{	
		Player humanPlayer = players.GetFirstHumanPlayer();
		if (humanPlayer == null || humanPlayer.Hand == null) return false;
		if (!humanPlayer.Hand.Contains(Card)) return false;
        if (!humanPlayer.HasToDoSomething) return false;
		if (gamePhase.Value == GamePhase.DogMaking)
		{
			if (humanPlayer.CanPutCardInDog(Card) 
					&& selectedCards.Count < Dog.GetNumberOfCards(players.Count)
					&& humanPlayer.HasToDoSomething)
			{
				return true;
			}
		}	
		if (gamePhase.Value == GamePhase.Play)
		{
            if (humanPlayer.CanPlayCard(Card, playedCards.Value) 
                && humanPlayer.HasToDoSomething)
            {
                return true;
            }
        }
		return false;
	} 

	private void SelectionAnimation(bool selected)
	{
        image.color = selected ? selectedColor : selectableColor;
    }


    private void SetSelectableEffect(bool selectable)
    {
        if (!selected)
        {
            image.color = selectable ? selectableColor : originalColor;
        }
    }
}
