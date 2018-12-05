using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Gère l'affichage du choix de l'allié pour le jeu à 5 joueurs
public class SelectAllyDisplay : MonoBehaviour
{
    public PlayerList players;
	public GameObject cardButtonPrefab;
	public GamePhaseVariable gamePhase;

	private Player Taker => players.GetTaker();
	private List<GameObject> cards;
	private bool buttonsShown;
	
	
    private void Awake()
    {
	   cards = new List<GameObject>();
    }


    private void Update()
    {
		if (gamePhase.Value == GamePhase.AllySelection)
		{
			if (cards.Count == 0)
			{
				GenerateCards();
			}	
		}
		else if (cards.Count > 0)
		{
			Clean();
		}
    }
		
	public void GenerateCards()
	{	
		// TODO
	}
	
		
	void Clean()
	{
		if (cards != null)
        {
            foreach (GameObject go in cards)
            {
                Destroy(go);
            }
        }   
        cards = new List<GameObject>();
	}
}