using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Gère l'affichage du bouton de validation du chien
public class ValidDogButtonDisplay : CanvasGroupDisplay {

    public Button validDogButton;
	public PlayerList players;
	public GamePhaseVariable gamePhase;
	public RuntimeCardList selectedCards;
	
	private new void Awake()
    {
        base.Awake();
        Hide();
    }
	
	void Update()
	{
		if (gamePhase.Value == GamePhase.DogMaking)
		{
			Player humanPlayer = players.GetFirstHumanPlayer();
			// Ne s'affiche que si le preneur est humain et qu'il a sélectionné suffisament de carte pour faire le chien
			if (humanPlayer != null 
				&& humanPlayer.IsTaker 
				&& selectedCards.Count == Dog.GetNumberOfCards(players.Count))
			{
				if (!visible) Show();
			}
			else
			{
				if (visible) Hide();
			}
		}
		else
		{
			if (visible) Hide();
		}
	}
}
